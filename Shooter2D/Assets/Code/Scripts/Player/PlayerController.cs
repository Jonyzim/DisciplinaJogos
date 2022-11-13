using MWP.Misc;
using MWP.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Serialization;

namespace MWP
{
    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerCanvas")] public Canvas playerCanvas;
        public MultiplayerEventSystem PlayerEventSystem;


        [SerializeField] private GameObject _HUDPrefab;

        //TEMPORARY
        [SerializeField] private GameObject[] _characterPrefab;

        [SerializeField] private Character _pawn;
        private Camera _cam;
        private Vector2 _direction = new Vector2(1, 0);


        private bool _isFiring;
        private Vector2 _mousePos;
        private Vector2 _movement = new Vector2(0, 0);

        private PlayerInput _playerInput;
        private int _score;
        public static PlayerController[] SActivePlayers { get; } = new PlayerController[4];

        public Character Pawn => _pawn;
        public int PlayerId { get; private set; }

        //Unity Methods
        private void Start()
        {
            _cam = Camera.main;

            for (var i = 0; i < 4; i++)
                if (SActivePlayers[i] == null)
                {
                    PlayerId = i + 1;
                    SActivePlayers[i] = this;
                    break;
                }

            _playerInput = GetComponent<PlayerInput>();
            GameEvents.Instance.OnSetUiMode += SetInputMode;


            Instantiate(_HUDPrefab).GetComponentInChildren<HUDManager>().SetupHUD(PlayerId);


            //TODO: Change to character selection instead
            Possess(Instantiate(_characterPrefab[PlayerId - 1]).GetComponent<Character>());
        }

        private void Update()
        {
            //Updates camera even with a still mouse
            // if (_isMouse)
            // {
            //     _direction = (_mousePos - ((Vector2)_pawn.transform.position)).normalized;
            // }
            _pawn.ControlRotation(_direction);
        }

        private void FixedUpdate()
        {
            _pawn.Move(_movement);
            if (_isFiring)
                _pawn.Fire(_direction);
        }

        private void OnDestroy()
        {
            _pawn.SetPlayerControlling(null);
            SActivePlayers[PlayerId - 1] = null;
        }


        public void Possess(Character character)
        {
            if (character != null)
            {
                _pawn = character;
                _pawn.SetPlayerControlling(this);
                SetCinemachineTargetGroup.SInstance.AddCharacter(_pawn);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed) GameEvents.Instance.Pause(PlayerId);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
                _isFiring = true;
            else if (context.canceled)
            {
                _isFiring = false;
                _pawn.ReleaseFire();
            }
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed) _pawn.Reload();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed) _pawn.Interact(PlayerId);
        }

        public void OnFlashLight(InputAction.CallbackContext context)
        {
            if (context.performed) _pawn.SwitchLight();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (_pawn == null || _cam == null)
                return;
            Vector2 newDirection;

            if (context.control.device.name == "Mouse")
            {
                _mousePos = _cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
                newDirection = (_mousePos - (Vector2)_pawn.transform.position).normalized;
            }

            else
            {
                newDirection = context.ReadValue<Vector2>();
            }

            if (newDirection != Vector2.zero) _direction = newDirection;
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed) StartCoroutine(_pawn.Dash(_movement));
        }

        public void OnDropWeapon(InputAction.CallbackContext context)
        {
            if(context.performed) _pawn.DropWeapon();
        }

        private void SetInputMode(int id, bool mode)
        {
            if (id != PlayerId) return;
            
            _playerInput.SwitchCurrentActionMap(mode ? "UI" : "Player");
        }


        #region Old Input System

        // ---------------------- Old Input System --------------------------
        //Consertar o centro para ser o centro da arma
        // void Update()
        // {
        //     if (Input.GetButtonDown("Cancel"))
        //     {
        //         GameEvents.current.Pause(PlayerId);
        //     }

        //     if (HUDManager.IsPaused)
        //         return;

        //     center = pawn.transform.position;
        //     mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //     direction = (mousePos - center).normalized;

        //     if(Input.GetButton("Fire1")){
        //         pawn.Fire(direction);
        //     }
        //     if(Input.GetButtonDown("Fire2")){
        //         pawn.Interact(pawn.character_id);
        //     }
        //     if(Input.GetButtonDown("Reload")){
        //         pawn.Reload();
        //     }
        //     if(Input.GetButtonDown("Fire3")){
        //         pawn.SwitchLight();
        //         //GameEvents.current.WaveChange();
        //     }

        ////     movement.x = Input.GetAxisRaw("Horizontal");
        ////     movement.y = Input.GetAxisRaw("Vertical");
        // }
        // -----------

        #endregion-----------------------------------------------------------
    }
}
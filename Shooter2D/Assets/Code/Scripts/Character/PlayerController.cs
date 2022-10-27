using MWP.Misc;
using MWP.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace MWP
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController[] s_ActivePlayers => s_activePlayers;

        public Character Pawn => _pawn;
        public int PlayerId => _playerId;
        public Canvas PlayerCanvas;
        public MultiplayerEventSystem PlayerEventSystem;

        private static PlayerController[] s_activePlayers = new PlayerController[4];


        [SerializeField] private GameObject _HUDPrefab;

        //TEMPORARY
        [SerializeField] private GameObject[] _characterPrefab;

        [SerializeField] private Character _pawn;
        private Camera _cam;
        private Vector2 _direction = new Vector2(1, 0);
        private Vector2 _movement = new Vector2(0, 0);
        private int _score;
        private Vector2 _mousePos;

        private int _playerId;
        private PlayerInput _playerInput;


        private bool _isFiring = false;


        public void Possess(Character character)
        {
            if (character != null)
            {
                _pawn = character;
                _pawn.SetPlayerControlling(this);
                SetCinemachineTargetGroup.s_Instance.AddCharacter(_pawn);
            }
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

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                GameEvents.Instance.Pause(PlayerId);
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isFiring = true;
            }
            else if (context.canceled)
            {
                _isFiring = false;
            }
        }
        public void OnReload(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _pawn.Reload();
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _pawn.Interact(PlayerId);
            }
        }
        public void OnFlashLight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _pawn.SwitchLight();
            }
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            if (_pawn == null || _cam == null)
                return;
            Vector2 newDirection;

            if (context.control.device.name == "Mouse")
            {
                _mousePos = (Vector2)_cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
                newDirection = (_mousePos - ((Vector2)_pawn.transform.position)).normalized;
            }

            else
            {
                newDirection = context.ReadValue<Vector2>();
            }

            if (newDirection != Vector2.zero)
            {
                _direction = newDirection;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_pawn.Dash(_movement));
            }
        }

        void SetInputMode(int id, bool mode)
        {
            if (id == _playerId)
            {
                if (mode)
                {
                    // Debug.Log("UI MODE");
                    _playerInput.SwitchCurrentActionMap("UI");
                }
                else
                {
                    // Debug.Log("PLAYER MODE");
                    _playerInput.SwitchCurrentActionMap("Player");
                }

            }
        }

        //Unity Methods
        void Start()
        {
            _cam = Camera.main;

            for (int i = 0; i < 4; i++)
            {
                if (s_activePlayers[i] == null)
                {
                    _playerId = i + 1;
                    s_activePlayers[i] = this;
                    break;
                }
            }

            _playerInput = GetComponent<PlayerInput>();
            GameEvents.Instance.OnSetUiMode += SetInputMode;


            Instantiate(_HUDPrefab).GetComponentInChildren<HUDManager>().SetupHUD(_playerId);


            //TODO: Change to character selection instead
            Possess(Instantiate(_characterPrefab[_playerId - 1]).GetComponent<Character>());

        }

        void Update()
        {

            //Updates camera even with a still mouse
            // if (_isMouse)
            // {
            //     _direction = (_mousePos - ((Vector2)_pawn.transform.position)).normalized;
            // }
            _pawn.ControlRotation(_direction);
        }
        void FixedUpdate()
        {
            _pawn.Move(_movement);
            if (_isFiring)
            {
                _pawn.Fire(_direction);
            }
            else
            {
                _pawn.ReleaseFire();
            }
        }

        void OnDestroy()
        {
            _pawn.SetPlayerControlling(null);
            s_activePlayers[_playerId - 1] = null;
        }

    }
}
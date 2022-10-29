using UnityEngine;

namespace MWP.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float loopTime;
        [SerializeField] private float radius;
        private float _f;
        private Vector2 _originalPos;

        private void Start()
        {
            _originalPos = transform.localPosition;
        }

        private void Update()
        {
            _f += Time.deltaTime;
            transform.localPosition = new Vector2(_originalPos.x, _originalPos.y + Mathf.Sin(_f / loopTime) * radius);
        }
    }
}
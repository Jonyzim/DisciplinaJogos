using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float _f;
    private Vector2 _originalPos;
    [SerializeField] private float loopTime;
    [SerializeField] private float radius;

    private void Start()
    {
        _originalPos = transform.localPosition;
    }

    private void Update()
    {
        _f += Time.deltaTime;
        transform.localPosition = new Vector2(_originalPos.x, _originalPos.y + (Mathf.Sin(_f / loopTime) * radius));
    }

}

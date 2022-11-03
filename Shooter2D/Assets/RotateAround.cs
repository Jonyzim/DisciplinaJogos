using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MWP
{
    public class RotateAround : MonoBehaviour
    {
        [SerializeField] private float speed = 30f;
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        }
    }
}

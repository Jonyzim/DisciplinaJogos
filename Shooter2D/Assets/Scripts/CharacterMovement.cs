using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float speed;

    public void move(float x, float y){
        transform.position += new Vector3(x, y, 0)*speed;
    }
}

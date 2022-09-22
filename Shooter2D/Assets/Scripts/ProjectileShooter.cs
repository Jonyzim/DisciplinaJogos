using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private List<Vector3> direction;

    [SerializeField] private GameObject projectilePrefab;

    public void Shoot()
    {
        int i = 0;
        foreach(Transform transf in spawnPos)
        {
            GameObject obj=Instantiate(projectilePrefab, transf.position, Quaternion.identity);
            PhysicsBullet bullet = obj.GetComponent<PhysicsBullet>();
            if (bullet != null)
                bullet.SetVariables(direction[i], 100);
            i++;
        }
    }
}
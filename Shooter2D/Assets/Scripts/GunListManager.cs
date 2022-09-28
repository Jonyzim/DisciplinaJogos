using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunListManager", menuName = "Shooter2D/GunListManager", order = 0)]
public class GunListManager : ScriptableObject
{
    private GunEntry[] GunList;

    public Gun GetRandomWeapon()
    {
        int i;
        // Gerar números até gerar uma arma válida
        do
        {
            i = UnityEngine.Random.Range(0, GunList.Length);
        } while (!GunList[i].isAvailable);

        return GunList[i].gun;
    }

    public void UnlockGun(int index)
    {
        GunList[index].isAvailable = true;
    }

    public void Save()
    {

    }
    private void Load()
    {

    }

}

[Serializable]
public struct GunEntry
{
    public Gun gun;
    public bool isAvailable;
}
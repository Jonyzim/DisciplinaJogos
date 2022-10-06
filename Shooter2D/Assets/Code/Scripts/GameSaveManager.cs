using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Trash, usado pra teste
public class GameSaveManager : MonoBehaviour
{
    public GunListManager gunListManager;

    private void Awake()
    {
        gunListManager.Load();
    }

    private void OnDestroy()
    {
        gunListManager.Save();
    }
}

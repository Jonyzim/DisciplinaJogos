using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private string startMap;
    [SerializeField] private string endlessMap;


    // [SerializeField] GameObject Menu;
    private void Start()
    {
        Cursor.visible = true;
        menuPanel.SetActive(false);
    }



    public void StoryMode()
    {
        mapManager.LoadMap(startMap);
    }

    public void EndlessMode()
    {
        mapManager.LoadMap(endlessMap);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

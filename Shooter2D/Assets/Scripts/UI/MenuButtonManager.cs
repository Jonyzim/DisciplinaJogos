using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    private void Start()
    {
        Cursor.visible = true;
        menuPanel.SetActive(false);
    }



    public void StoryMode(){
        SceneManager.LoadScene("SampleScene");
    }

    public void MultiplayerMode(){

    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

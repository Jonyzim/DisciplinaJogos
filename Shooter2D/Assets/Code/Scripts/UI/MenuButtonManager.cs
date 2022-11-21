using MWP.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MWP.UI
{
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
            if(menuPanel!=null)
            menuPanel.SetActive(false);
            if (Time.timeScale != 1f)
                Time.timeScale = 1f;
        }


        public void StoryMode()
        {
            mapManager.LoadMap(startMap);
        }

        public void EndlessMode()
        {
            mapManager.LoadMap(endlessMap);
        }
        public void LoadSceneByName(string name)
        {
            SceneManager.LoadScene(name);
        }
        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
    }
}
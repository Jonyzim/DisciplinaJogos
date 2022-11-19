using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MWP
{
    public class PauseOptions : MonoBehaviour
    {
        public void LoadMenu(){
            SceneManager.LoadScene("Menu");
        }
    }
}

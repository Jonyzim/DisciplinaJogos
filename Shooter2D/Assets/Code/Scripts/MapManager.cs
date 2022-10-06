using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

#if (UNITY_EDITOR)
using UnityEditor.SceneManagement;
#endif

[Serializable]
[CreateAssetMenu(fileName = "MapManager", menuName = "Shooter2D/MapManager", order = 0)]
public class MapManager : ScriptableObject
{
    [SerializeField]
    private Map[] _mapList;

    [SerializeField]
    private string LoadMapName;

    private Scene[] _loadedScenes;

#if (UNITY_EDITOR)
    private SceneSetup[] _saveState;
#endif



    [Button]
    private void LoadMap()
    {
        Map? nullableMapToLoad = Map.SearchMap(_mapList, LoadMapName);


        if (nullableMapToLoad != null)
        {
            Map mapToLoad = (Map)nullableMapToLoad;

            foreach (string sceneName in mapToLoad.ScenesName)
            {
#if (UNITY_EDITOR)
                EditorSceneManager.OpenScene(sceneName, OpenSceneMode.Additive);
#else
                SceneManager.
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
#endif
            }
        }


        Debug.LogError("Map doesn't exist");

    }


#if (UNITY_EDITOR)
    [Button]
    private void SaveState()
    {
        _saveState = EditorSceneManager.GetSceneManagerSetup();
    }

    [Button]
    private void LoadState()
    {
        EditorSceneManager.RestoreSceneManagerSetup(_saveState);
    }
#endif

}

[Serializable]
public struct Map
{
    public string MapName;

    [Scene]
    public string[] ScenesName;





    public static Map? SearchMap(Map[] mapList, string mapName)
    {
        foreach (Map map in mapList)
        {
            if (string.Compare(map.MapName, mapName) == 0)
            {
                return map;
            }
        }
        return null;
    }
}

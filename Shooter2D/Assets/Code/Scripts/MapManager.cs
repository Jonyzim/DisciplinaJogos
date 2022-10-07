using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

#if (UNITY_EDITOR)
using UnityEditor.SceneManagement;
using UnityEditor;
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

    private void LoadMap()
    {
        Map? mapToLoad = Map.SearchMap(_mapList, LoadMapName);

        if (!mapToLoad.HasValue)
        {
            Debug.LogError("Map doesn't exist");
            return;
        }

        SceneManager.LoadScene((int)mapToLoad?.ActiveScene, LoadSceneMode.Single);
        foreach (int sceneName in mapToLoad?.LoadedScenes)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

    }

    // Funções exclusivas do editor
#if (UNITY_EDITOR)
    [Button("Load Map")]
    private void LoadEditorMap()
    {
        Map? mapToLoad = Map.SearchMap(_mapList, LoadMapName);

        if (!mapToLoad.HasValue)
        {
            Debug.LogError("Map doesn't exist");
            return;
        }

        // Gets scene path by index as OpenScene() requires it
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[(int)mapToLoad?.ActiveScene].path, OpenSceneMode.Single);
        foreach (int sceneName in mapToLoad?.LoadedScenes)
        {
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[sceneName].path, OpenSceneMode.Additive);
        }

    }

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
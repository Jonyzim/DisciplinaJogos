using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

#if (UNITY_EDITOR)
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace MWP.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(fileName = "MapManager", menuName = "Shooter2D/MapManager", order = 0)]
    public class MapManager : ScriptableObject
    {
        public Map[] MapList;

        [HideInInspector] public int LoadMapIndex;

        private Scene[] _loadedScenes;

#if (UNITY_EDITOR)
        private SceneSetup[] _saveState;
#endif

        public void LoadMap(string LoadMapName)
        {
            var mapToLoad = Map.SearchMap(MapList, LoadMapName);

            if (!mapToLoad.HasValue)
            {
                Debug.LogError("Map doesn't exist");
                return;
            }

            SceneManager.LoadScene((int)mapToLoad?.ActiveScene, LoadSceneMode.Single);
            foreach (var sceneName in mapToLoad?.LoadedScenes)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        // Funções exclusivas do editor
#if (UNITY_EDITOR)
        [Button("Load Map")]
        private void LoadEditorMap()
        {
            var mapToLoad = MapList[LoadMapIndex];

            // Gets scene path by index as OpenScene() requires it
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[mapToLoad.ActiveScene].path, OpenSceneMode.Single);
            foreach (var sceneName in mapToLoad.LoadedScenes)
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[sceneName].path, OpenSceneMode.Additive);
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
}
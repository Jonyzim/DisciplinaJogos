using System;
using NaughtyAttributes;

namespace MWP.ScriptableObjects
{
    [Serializable]
    public struct Map
    {
        public string MapName;

        [Scene] public int ActiveScene;

        [Scene] public int[] LoadedScenes;


        public static Map? SearchMap(Map[] mapList, string mapName)
        {
            foreach (var map in mapList)
                if (string.Compare(map.MapName, mapName) == 0)
                    return map;
            return null;
        }
    }
}
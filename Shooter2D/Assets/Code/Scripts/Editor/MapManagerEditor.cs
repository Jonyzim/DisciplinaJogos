using System.Linq;
using MWP.ScriptableObjects;
using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [CustomEditor(typeof(MapManager))]
    public class MapManagerEditor : NaughtyInspector
    {
        private int _choiceIndex;

        public override void OnInspectorGUI()
        {
            GetSerializedProperties(ref _serializedProperties);

            var anyNaughtyAttribute =
                _serializedProperties.Any(p => PropertyUtility.GetAttribute<INaughtyAttribute>(p) != null);
            if (!anyNaughtyAttribute)
                DrawDefaultInspector();
            else
                DrawSerializedProperties();

            DrawNonSerializedFields();
            DrawNativeProperties();

            DrawMapDropdown();

            DrawButtons();
        }

        private void DrawMapDropdown()
        {
            var mapManager = (MapManager)target;

            var _choices = new string[mapManager.MapList.Length];

            for (var i = 0; i < mapManager.MapList.Length; i++) _choices[i] = mapManager.MapList[i].MapName;

            EditorGUILayout.LabelField("Test");
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);


            mapManager.LoadMapIndex = _choiceIndex;

            EditorUtility.SetDirty(target);
        }
    }
}
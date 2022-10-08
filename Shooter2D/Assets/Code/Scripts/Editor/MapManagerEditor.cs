using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

namespace NaughtyAttributes.Editor
{


    [CustomEditor(typeof(MapManager))]
    public class MapManagerEditor : NaughtyAttributes.Editor.NaughtyInspector
    {
        private int _choiceIndex = 0;

        public override void OnInspectorGUI()
        {
            GetSerializedProperties(ref _serializedProperties);

            bool anyNaughtyAttribute = _serializedProperties.Any(p => PropertyUtility.GetAttribute<INaughtyAttribute>(p) != null);
            if (!anyNaughtyAttribute)
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawSerializedProperties();
            }

            DrawNonSerializedFields();
            DrawNativeProperties();

            DrawMapDropdown();

            DrawButtons();
        }

        private void DrawMapDropdown()
        {


            MapManager mapManager = (MapManager)target;

            string[] _choices = new string[mapManager.MapList.Length];

            for (int i = 0; i < mapManager.MapList.Length; i++)
            {
                _choices[i] = mapManager.MapList[i].MapName;
            }

            EditorGUILayout.LabelField("Test");
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);


            mapManager.LoadMapIndex = _choiceIndex;

            EditorUtility.SetDirty(target);
        }
    }
}
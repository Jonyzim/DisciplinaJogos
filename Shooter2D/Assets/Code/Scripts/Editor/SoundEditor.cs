using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

[CustomPropertyDrawer(typeof(Sound))]
public class SoundEditor : PropertyDrawer
{
    private Audio.AudioManager _audioManager;

    private int _choiceIndex = 0;
    private int _managerChoiceIndex = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);
        // int indent = EditorGUI.indentLevel;
        // EditorGUI.indentLevel = 0;

        DrawAudioManagerDropdown(position, property, label);
        DrawSoundDropdown(position, property, label);

        EditorGUI.EndProperty();
    }

    private void DrawAudioManagerDropdown(Rect position, SerializedProperty property, GUIContent label)
    {
        List<Audio.AudioManager> audioManagers = GetAllInstances<Audio.AudioManager>();

        string[] _choices = new string[audioManagers.Count];

        for (int i = 0; i < audioManagers.Count; i++)
        {
            _choices[i] = audioManagers[i].name;
        }

        Rect newRect = new Rect(position.x, position.y, position.width / 2, position.height);
        _managerChoiceIndex = EditorGUI.Popup(newRect, _managerChoiceIndex, _choices);

        _audioManager = audioManagers[_managerChoiceIndex];
    }

    private void DrawSoundDropdown(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty soundId = property.FindPropertyRelative("Id");
        SerializedProperty manager = property.FindPropertyRelative("_manager");

        string[] _choices = new string[_audioManager.SoundList.Length];

        for (int i = 0; i < _audioManager.SoundList.Length; i++)
        {
            _choices[i] = _audioManager.SoundList[i].identifier;
        }
        // GUIContent soundLabel = new GUIContent("test");
        Rect newRect = new Rect(position.x + (position.width / 2 + 5), position.y, (position.width / 2 - 5), position.height);

        // position = EditorGUI.PrefixLabel(position, soundLabel);
        _choiceIndex = EditorGUI.Popup(newRect, _choiceIndex, _choices);

        soundId.intValue = _choiceIndex;
        // manager.objectReferenceValue = _audioManager;
    }

    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        return AssetDatabase.FindAssets($"t: {typeof(T).FullName}").ToList()
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .ToList();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
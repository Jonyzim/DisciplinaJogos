using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
/* !DEPRECATED
[CustomPropertyDrawer(typeof(Sound))]
public class SoundEditor : PropertyDrawer
{
    [SerializeField] private MyAudio.AudioManager _audioManager;

    private SerializedProperty idProperty;
    private SerializedProperty managerProperty;

    private int _choiceIndex = 0;
    private int _managerChoiceIndex = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, label); ;
        idProperty = property.FindPropertyRelative(nameof(Sound.Id));
        managerProperty = property.FindPropertyRelative(nameof(Sound.Manager));

        DrawAudioManagerDropdown(position);
        DrawSoundDropdown(position);

        EditorGUI.EndProperty();
    }

    private void DrawAudioManagerDropdown(Rect position)
    {
        List<MyAudio.AudioManager> audioManagers = GetAllInstances<MyAudio.AudioManager>();

        string[] _choices = new string[audioManagers.Count];
        string managerName = "";
        if (((MyAudio.AudioManager)managerProperty.objectReferenceValue) != null)
        {
            managerName = ((MyAudio.AudioManager)managerProperty.objectReferenceValue).name;
        }

        for (int i = 0; i < audioManagers.Count; i++)
        {
            _choices[i] = audioManagers[i].name;
            if (audioManagers[i].name == managerName)
            {
                _managerChoiceIndex = i;
            }
        }

        Rect newRect = new Rect(position.x, position.y, position.width / 2, position.height);
        _managerChoiceIndex = EditorGUI.Popup(newRect, _managerChoiceIndex, _choices);

        _audioManager = audioManagers[_managerChoiceIndex];
        managerProperty.objectReferenceValue = _audioManager;
    }

    private void DrawSoundDropdown(Rect position)
    {
        string[] _choices = new string[_audioManager.SoundList.Length];

        for (int i = 0; i < _audioManager.SoundList.Length; i++)
        {
            _choices[i] = _audioManager.SoundList[i].identifier;
        }
        _choiceIndex = idProperty.intValue;

        Rect newRect = new Rect(position.x + (position.width / 2 + 5), position.y, (position.width / 2 - 5), position.height);
        _choiceIndex = EditorGUI.Popup(newRect, _choiceIndex, _choices);

        idProperty.intValue = _choiceIndex;

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
*/
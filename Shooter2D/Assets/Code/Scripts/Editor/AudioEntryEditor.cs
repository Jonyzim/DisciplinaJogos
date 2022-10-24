using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

[CustomEditor(typeof(Sound))]
public class SoundEditor : Editor
{
    public AudioManager _audioManager;

    private int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        DrawSoundDropdown();
    }

    private void DrawSoundDropdown()
    {
        Sound sound = (Sound)target;
        string[] _choices = new string[_audioManager.SoundList.Length];

        for (int i = 0; i < _audioManager.SoundList.Length; i++)
        {
            _choices[i] = _audioManager.SoundList[i].identifier;
        }

        EditorGUILayout.LabelField("Sound");
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);


        sound.Id = _choiceIndex;
        sound.Manager = _audioManager;

        EditorUtility.SetDirty(target);
    }
}
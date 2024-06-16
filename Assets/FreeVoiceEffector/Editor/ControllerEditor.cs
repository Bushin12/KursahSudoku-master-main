using FreeVoiceEffector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
namespace FreeVoiceEffector { 
[CustomEditor(typeof(FreeVoiceEffectController))]
public class ControllerEditor : Editor
{
    SerializedProperty effectPresetProp;
    SerializedProperty valueProp;
    void OnEnable()
    {
        effectPresetProp = serializedObject.FindProperty("effectPreset");
        valueProp = serializedObject.FindProperty("value");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(effectPresetProp);
        EditorGUILayout.Slider(valueProp,0,1);

        serializedObject.ApplyModifiedProperties();
    }
}
}

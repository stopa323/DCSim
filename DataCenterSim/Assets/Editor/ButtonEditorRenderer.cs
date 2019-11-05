using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Editor
{
    [CustomEditor(typeof(BaseButton))]
    public class BaseButtonInspector : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            BaseButton btn = target as BaseButton;

            btn.audioSource = (AudioSource)EditorGUILayout.ObjectField("Audio Source:",
                btn.audioSource, typeof(AudioSource), true);

            btn.hoverSound = (AudioClip)EditorGUILayout.ObjectField("OnHover Sound:",
                btn.hoverSound, typeof(AudioClip), true);

            btn.clickSound = (AudioClip)EditorGUILayout.ObjectField("OnClick Sound:",
                btn.clickSound, typeof(AudioClip), true);

            btn.errorSound = (AudioClip)EditorGUILayout.ObjectField("Error Sound:",
                btn.errorSound, typeof(AudioClip), true);

            base.OnInspectorGUI();
        }
    }
}

using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using Game.Events;

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

            btn.OnClickVoidEvent = (VoidEvent)EditorGUILayout.ObjectField("OnClick (void) Event:",
                btn.OnClickVoidEvent, typeof(VoidEvent), true);

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(DevicePurchaseButton))]
    public class DevicePurchaseButtonInspector : BaseButtonInspector
    {
        public override void OnInspectorGUI()
        {
            DevicePurchaseButton btn = target as DevicePurchaseButton;

            btn.OnDeviceSelected = (GameObjectEvent)EditorGUILayout.ObjectField("OnClick Event:",
                btn.OnDeviceSelected, typeof(GameObjectEvent), true);

            btn.DevicePrefab = (GameObject)EditorGUILayout.ObjectField("Device Prefab:",
                btn.DevicePrefab, typeof(GameObject), true);

            base.OnInspectorGUI();
        }
    }
}

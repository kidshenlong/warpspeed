using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformRemoveComponentScript))]
public class PlatformRemoveComponentInspector : Editor {

    private PlatformRemoveComponentScript remover;

    public void OnEnable() 
    {
        remover = (PlatformRemoveComponentScript)target;
    }

    public override void OnInspectorGUI()
    {
        remover.targetedPlatforms = (RuntimePlatform)EditorGUILayout.EnumMaskField("Remove Platform", remover.targetedPlatforms);
        remover.removeComponent = (Component)EditorGUILayout.ObjectField(remover.removeComponent, typeof(Component), true, null);
    }
}
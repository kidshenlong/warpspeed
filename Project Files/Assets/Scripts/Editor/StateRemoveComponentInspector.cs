using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateRemoveComponentScript))]
public class StateRemoveComponentInspector : Editor
{
    private StateRemoveComponentScript remover;

    public void OnEnable() 
    {
        remover = (StateRemoveComponentScript)target;
    }

    public override void OnInspectorGUI()
    {
        remover.gameStateMask = (GameState)EditorGUILayout.EnumMaskField("Remove State", remover.gameStateMask);
        remover.removeComponent = (Component)EditorGUILayout.ObjectField(remover.removeComponent, typeof(Component), true, null);
    }
}

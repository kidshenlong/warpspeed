using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PathManager))]
public class PathManagerInspector : Editor 
{

    private bool createMode;
    private PathManager manager;
    private GameObject changeSelection;

    public void OnEnable() {
        manager = (PathManager)target;
        if (manager.points==null) 
        {
            manager.points = new List<PathPoint>();
        } 
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Show");
        bool tmp = manager.ShowPaths;
        manager.ShowPaths = EditorGUILayout.Toggle(manager.ShowPaths);
        if(manager.ShowPaths != tmp){
            HandleUtility.Repaint();
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.LabelField("Number of Points: " + manager.points.Count);

        EditorGUILayout.BeginHorizontal();
        if (!createMode)
        {
            if (GUILayout.Button("Add Path Point"))
            {
                createMode = true;
                Debug.Log("Create Path Mode");
                //HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            }
        }
        else {
            if (GUILayout.Button("Cancel"))
            {
                createMode = false;
                Debug.Log("Cancel");
            }
        }
        
        if (GUILayout.Button("Delete All")) 
        {
            PathPoint[] points = manager.gameObject.GetComponents<PathPoint>();
            for (int i = 0; i < points.Length; i++) 
            {
                Editor.Destroy(points[i].gameObject);
            }
            manager.points.Clear();
        }

        EditorGUILayout.EndHorizontal();
    }

    public void OnSceneGUI() 
    {
        
        if (changeSelection!=null) 
        {
            Selection.objects = new Object[]{changeSelection};
            
            changeSelection = null;
        }
        if (createMode) 
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Vector2 pos = new Vector2(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y);
                Ray ray = Camera.current.ScreenPointToRay(pos);
                RaycastHit hit;
                
                if(Physics.Raycast(ray,out hit))
                {
                    GameObject point = new GameObject();
                    point.transform.position = hit.point + Vector3.up;
                    point.name = "Point";
                    point.transform.parent = manager.transform;
                    
                    PathPoint pathPoint = point.AddComponent<PathPoint>();
                    manager.points.Add(pathPoint);
                    pathPoint.manager = manager;

                    createMode = false;
                    Debug.Log("Path Point Created");
                    changeSelection = point;
                }
            }
        }
    }
}

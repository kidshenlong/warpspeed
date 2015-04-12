using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PathPoint))]
public class PathPointInspector : Editor
{
    private PathPoint pathPoint;
    private bool linkMode;
    private bool delinkMode;

    void OnEnable() {
        pathPoint = (PathPoint)target;
        linkMode = false;
    }

    public override void OnInspectorGUI()
    {

        int id = 0;
        List<PathPoint> points = pathPoint.manager.points;
        int length = points.Count;
        for (int i = 0; i < length; i++)
        {
            if (points[i] == pathPoint)
            {
                id = i;
                break;
            }
        }

        EditorGUILayout.LabelField("ID: " + id, EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Auto Adjust Height: ");
        pathPoint.autoHeight = EditorGUILayout.Toggle(pathPoint.autoHeight);
        EditorGUILayout.EndHorizontal();

        // draw weight
        EditorGUILayout.LabelField("Weight:");
        pathPoint.Weight = EditorGUILayout.Slider(pathPoint.Weight, 0.1f, 10f);

        // draw acceleration multiplier
        EditorGUILayout.LabelField("Acceleration Multiplier:");
        pathPoint.AccelMultiplier = EditorGUILayout.Slider(pathPoint.AccelMultiplier, 0.1f, 1f);
    }

    void OnSceneGUI() {
        Vector3 screenPoint = Camera.current.WorldToScreenPoint(pathPoint.transform.position);
        screenPoint = new Vector3(screenPoint.x+80, Camera.current.pixelHeight - screenPoint.y - 30, screenPoint.z);

        Handles.BeginGUI();

        // if link or delink mode is NOT active
        if (!linkMode && !delinkMode)
        {
            // draw ID tag
            GUILayout.BeginArea(new Rect(screenPoint.x-200, screenPoint.y - 10, 60, 20));
            int id = 0;
            List<PathPoint> points = pathPoint.manager.points;
            int length = points.Count;
            for(int i=0;i<length;i++)
            {
                if(points[i]==pathPoint)
                {
                    id = i;
                    break;
                }
            }
            if (GUILayout.Button("ID:"+id)) { }
            GUILayout.EndArea();

            // draw weight and accel multipier fields
            List<PathPoint> toLocations = pathPoint.ToLocations;
            if (toLocations.Count > 1)
            {
                for (int i = 0; i < toLocations.Count; i++)
                {
                    Vector3 toScreenPoint = Camera.current.WorldToScreenPoint(toLocations[i].transform.position);
                    toScreenPoint = new Vector3(toScreenPoint.x, Camera.current.pixelHeight - toScreenPoint.y - 30, toScreenPoint.z);

                    EditorGUI.LabelField(
                        new Rect(toScreenPoint.x, toScreenPoint.y + 20, 60, 20),
                        "Weight:");

                    toLocations[i].Weight = EditorGUI.Slider(
                        new Rect(toScreenPoint.x, toScreenPoint.y + 35, 60, 20),
                        toLocations[i].Weight,
                        0.1f, 10f);

                    EditorGUI.LabelField(
                        new Rect(toScreenPoint.x, toScreenPoint.y + 60, 100, 20),
                        "Accel Multiuplier:");

                    toLocations[i].AccelMultiplier = EditorGUI.Slider(
                        new Rect(toScreenPoint.x, toScreenPoint.y + 75, 60, 20),
                        toLocations[i].AccelMultiplier,
                        0.1f, 1f);
                }
            }
            EditorGUI.LabelField(
                    new Rect(screenPoint.x-200, screenPoint.y + 20, 60, 20),
                    "Weight:");

            pathPoint.Weight = EditorGUI.Slider(
                new Rect(screenPoint.x-200, screenPoint.y + 35, 60, 20),
                pathPoint.Weight,
                0.1f, 10f);

            EditorGUI.LabelField(
                    new Rect(screenPoint.x - 200, screenPoint.y + 60, 100, 20),
                    "Accel Multiuplier:");

            pathPoint.AccelMultiplier = EditorGUI.Slider(
                new Rect(screenPoint.x - 200, screenPoint.y + 75, 60, 20),
                pathPoint.AccelMultiplier,
                0.1f, 1f);


            // draw "new point" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y-10, 100, 20));
            if (GUILayout.Button("New Point"))
            {
                Undo.RegisterSceneUndo("Undo New PathPoint");

                GameObject point = new GameObject();
                point.transform.position = pathPoint.transform.position + (Vector3.forward * 4) + (Vector3.right * 4);
                point.name = "Point";
                point.transform.parent = pathPoint.manager.transform;

                PathPoint tmp = point.AddComponent<PathPoint>();
                pathPoint.manager.points.Add(tmp);
                tmp.manager = pathPoint.manager;

                pathPoint.ToLocations.Add(tmp);
                tmp.FromLocations.Add(pathPoint);

                Selection.activeGameObject = point;
            }

            GUILayout.EndArea();

            // draw "link" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y+10, 100, 20));
            if (GUILayout.Button("Link"))
            {
                linkMode = true;
            }
            GUILayout.EndArea();

            // draw "delink" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y + 30, 100, 20));
            if (GUILayout.Button("Delink"))
            {
                delinkMode = true;
            }
            GUILayout.EndArea();

            // draw "destroy" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y + 60, 100, 20));
            if (GUILayout.Button("Destroy"))
            {
                Undo.RegisterSceneUndo("Undo Destroy PathPoint");
                DestroyImmediate(pathPoint.gameObject);
            }
            GUILayout.EndArea();
        }
        // if link mode is active
        else if (linkMode) 
        {
            // disable keyboard focus
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            // draw help text for link mode
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y, 200, 20));
            GUILayout.Label("Click another point to Link");
            GUILayout.EndArea();

            // draw "cancel" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y + 20, 100, 20));
            if (GUILayout.Button("Cancel"))
            {
                linkMode = false;
            }
            GUILayout.EndArea();

            // listen for mouse left press event
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                // use raycasting to see what object was click on
                Vector2 pos = new Vector2(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y);
                Ray ray = Camera.current.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 point = hit.point;
                    List<PathPoint> points = pathPoint.manager.points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        float distance = Vector3.Distance(point, points[i].transform.position);
                        
                        if (pathPoint != points[i] && 2 > distance)
                        {
                            bool test = true;
                            // check if the pathpoint already is link to a next pathpoint
                            for (int x = 0; x < pathPoint.ToLocations.Count; x++)
                            {
                                if(pathPoint.ToLocations[x]==points[i])
                                {
                                    test = false;
                                    break;
                                }
                            }
                            // check if the pathpoint already is link to a previous pathpoint
                            for (int x = 0; x < pathPoint.FromLocations.Count; x++)
                            {
                                if (pathPoint.FromLocations[x] == points[i])
                                {
                                    test = false;
                                    break;
                                }
                            }
                            // if the current pathpoint isnt connected to the selected pathpoint then pair the two pathpoints
                            if (test)
                            {
                                Undo.RegisterSceneUndo("Undo Link PathPoint");
                                pathPoint.ToLocations.Add(points[i]);
                                points[i].FromLocations.Add(pathPoint);
                                linkMode = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        // if delink mode is active
        else if (delinkMode) {
            // disable keyboard focus
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            // draw help text for link mode
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y, 200, 20));
            GUILayout.Label("Click another point to Delink");
            GUILayout.EndArea();

            // draw "cancel" button
            GUILayout.BeginArea(new Rect(screenPoint.x, screenPoint.y + 20, 100, 20));
            if (GUILayout.Button("Cancel"))
            {
                delinkMode = false;
            }
            GUILayout.EndArea();

            // listen for mouse left press event
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                // use raycasting to see what object was click on
                Vector2 pos = new Vector2(Event.current.mousePosition.x, Camera.current.pixelHeight - Event.current.mousePosition.y);
                Ray ray = Camera.current.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 point = hit.point;
                    List<PathPoint> points = pathPoint.manager.points;

                    // check if the pathpoint registered to "ToLocations"
                    for (int i = 0; i < pathPoint.ToLocations.Count; i++) { 
                        float distance = Vector3.Distance(point, pathPoint.ToLocations[i].transform.position);
                        if (pathPoint != pathPoint.ToLocations[i] && 2 > distance)
                        {
                            pathPoint.ToLocations[i].FromLocations.Remove(pathPoint);
                            pathPoint.ToLocations.Remove(pathPoint.ToLocations[i]);
                            delinkMode = false;
                            break;
                        }
                    }

                    // check if the pathpoint registered to "FromLocations"
                    for (int i = 0; i < pathPoint.FromLocations.Count; i++)
                    {
                        float distance = Vector3.Distance(point, pathPoint.FromLocations[i].transform.position);
                        if (pathPoint != pathPoint.FromLocations[i] && 2 > distance)
                        {
                            pathPoint.FromLocations[i].ToLocations.Remove(pathPoint);
                            pathPoint.FromLocations.Remove(pathPoint.FromLocations[i]);
                            delinkMode = false;
                            break;
                        }
                    }
                }
            }
        }

        Handles.EndGUI();
    }
    
}

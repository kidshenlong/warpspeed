using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("WARPSPEED/AI/Path Manager")]
public class PathManager : MonoBehaviour
{
    /******************************************************
     * variables
     ******************************************************/
    private static readonly int REPATH_VOID_LAYER = 1 << 31;
    private static PathManager instance;

    [HideInInspector]
    [SerializeField]
    public List<PathPoint> points;

    public bool ShowPaths = true;

    /******************************************************
     * MonoBehaviour methods, Awake
     ******************************************************/

    void Awake() 
    {
        instance = this;
    }

    /******************************************************
     * removePoint, this is meant to only be call from a editor class
     ******************************************************/

    public bool removePoint(PathPoint point)
    {
        return points.Remove(point);
    }

    /******************************************************
     * FindPathPointById
     ******************************************************/

    public static PathPoint FindPathPointById(int id) 
    { 
        return instance.points[id];
    }

    /******************************************************
     * FindClosestPoint
     ******************************************************/

    public static PathPoint FindClosestPoint(Vector3 pos)
    {
        Vector2 basePos = new Vector2(pos.x,pos.z);
        List<PathPoint> points = instance.points;
        PathPoint closestPoint = points[0];

        for (int i = 1; i < points.Count; i++) 
        {
            if (Vector2.Distance(closestPoint.Pos2d, basePos) >
                Vector2.Distance(points[i].Pos2d, basePos))
            {
                Vector2 nextPoint = points[i].getNextClosestPathPoint().Pos2d;
                if (Vector2.Distance(points[i].Pos2d, basePos) >
                    (Vector2.Distance(nextPoint,basePos) + PathFollowScript.SMOOTH_MARGIN))
                {
                    Ray ray = new Ray(pos,points[i].Pos3d - pos);
                    if(!Physics.Raycast(ray,1000,REPATH_VOID_LAYER))
                    {
                       closestPoint = points[i];
                    }
                }
            }
        }

        return closestPoint;
    }
}

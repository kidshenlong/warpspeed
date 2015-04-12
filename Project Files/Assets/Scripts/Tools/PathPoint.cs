using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PathPoint : MonoBehaviour
{
    /******************************************************
     * variables
     ******************************************************/

    private static readonly float OBSTABLE_DIVIDER = 10f;

    [HideInInspector]
    public PathManager manager;

    [HideInInspector]
    public bool showGizmos;

    [HideInInspector]
    [SerializeField]
    public List<PathPoint> FromLocations = new List<PathPoint>();

    [HideInInspector]
    [SerializeField]
    public List<PathPoint> ToLocations = new List<PathPoint>();

    public bool autoHeight = true;

    private Vector2 pos2d;
    public Vector2 Pos2d
    {
        get
        {
            return pos2d;
        }
    }

    private Vector3 pos3d;
    public Vector3 Pos3d
    {
        get
        {
            return pos3d;
        }
    }

    [SerializeField]
    private float weight = 0.5f;
    public float Weight
    {
        get { return weight; }
        set { weight = Mathf.Clamp(value, 0.1f, 10f); }
    }
    public IWeight[] iWeights;

    [SerializeField]
    private float accelMultiplier = 1.0f;
    public float AccelMultiplier
    {
        get { return accelMultiplier; }
        set { accelMultiplier = Mathf.Clamp(value,0.1f,1f); }
    }
    

    /******************************************************
     * MonoBehaviour methods, Awake
     ******************************************************/

    void Awake() 
    {
        pos2d = new Vector2(transform.position.x, transform.position.z);
        pos3d = transform.position;

        Component[] components = GetComponents(typeof(IWeight));
        iWeights = new IWeight[components.Length];
        for (int i = 0; i < components.Length; i++)
            iWeights[i] = (IWeight)components[i];
    }

    /******************************************************
     * MonoBehaviour methods, Update
     ******************************************************/

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying && autoHeight)
        {
            Transform myTransform = this.transform;
            Ray ray = new Ray(new Vector3(myTransform.position.x, 1000, myTransform.position.z), -Vector3.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                myTransform.position = new Vector3(myTransform.position.x, hit.point.y, myTransform.position.z) + Vector3.up;
            }
        }
    }
#endif

    /******************************************************
     * MonoBehaviour methods, OnDrawGizmos
     ******************************************************/

#if UNITY_EDITOR
    void OnDrawGizmos() 
    {

        if (manager.ShowPaths) 
        {
            if (Selection.gameObjects.Length > 0 && Selection.gameObjects[0] == this.gameObject)
            {
                Gizmos.color = Color.red;
                Handles.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
                Handles.color = Color.green;
            }
            for (int i = 0; i < ToLocations.Count; i++)
            {
                Gizmos.DrawLine(this.transform.position, ToLocations[i].transform.position);
                Handles.ArrowCap(0,
                        this.transform.position,
                        Quaternion.LookRotation(-(this.transform.position - ToLocations[i].transform.position).normalized),
                        6);
            }
            IWeight weightInterface = (IWeight)GetComponent(typeof(IWeight));
            if (weightInterface != null)
                Gizmos.color = Color.blue;
            else
                Gizmos.color = Color.yellow;
            
            Gizmos.DrawSphere(transform.position, 1);
        }

        
    }
#endif

    /******************************************************
     * MonoBehaviour methods, OnDestroy
     ******************************************************/

    void OnDestroy()
    {
        for (int i = 0; i < ToLocations.Count; i++)
            ToLocations[i].FromLocations.Remove(this);

        for (int i = 0; i < FromLocations.Count; i++)
            FromLocations[i].ToLocations.Remove(this);

        manager.removePoint(this);
    }

    /******************************************************
     * getCalculatedWeight, use with the method "getNextPathPoint"
     * 
     * * obstacle lower weight values
     * * custom IWeight interface can change weight values
     ******************************************************/

    private float getCalculatedWeight(PathPoint fromPoint)
    {
        float finalWeight = Weight;
        Vector3 fromPos = fromPoint.transform.position;
        Vector3 thisPos = this.transform.position;
        Vector3 magnitude = thisPos - fromPos;

        Ray ray = new Ray(fromPos, magnitude.normalized);
        RaycastHit[] hit = Physics.RaycastAll(ray, magnitude.magnitude);
        bool hasObstacle = false;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != this.collider)
            {
                hasObstacle = true;
                break;
            }
        }

        if (hasObstacle)
            finalWeight /= OBSTABLE_DIVIDER;
        
        for (int i = 0; i < iWeights.Length; i++)
            finalWeight = iWeights[i].adjustWeight(finalWeight);
        
        return finalWeight;
    }

    /******************************************************
     * getNextClosestPathPoint, get the next point base on there distance
     ******************************************************/

    public PathPoint getNextClosestPathPoint()
    {
        PathPoint pathPoint = ToLocations[0];
        for (int i = 1; i < ToLocations.Count; i++) 
        {
            if (Vector2.Distance(pathPoint.pos2d,this.pos2d) > 
                Vector2.Distance(ToLocations[i].pos2d,this.pos2d)) 
            {
                pathPoint = ToLocations[i];
            }
        }
        return pathPoint;
    }


    /******************************************************
     * getNextWeightedPathPoint, get the next point base on there weight
     ******************************************************/

    public PathPoint getNextWeightedPathPoint() 
    {
        if(ToLocations.Count>1)
        {
            float totalWeight = 0;

            float[] calculatedWeights = new float[ToLocations.Count];
            for (int i = 0; i < ToLocations.Count; i++)
            {
                calculatedWeights[i] = ToLocations[i].getCalculatedWeight(this);
                totalWeight += calculatedWeights[i];
            }
            float randomWeightValue = Random.value * totalWeight;
            float sumTotal = 0;
            
            for(int i=0;i<calculatedWeights.Length;i++)
            {
                sumTotal += calculatedWeights[i];
                if (randomWeightValue <= sumTotal)
                {
                    return ToLocations[i];
                }
            }
        }

        return ToLocations[0];
    }
}

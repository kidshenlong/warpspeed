using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class StartPositionNodeScript : MonoBehaviour 
{

    void Start() 
    {
        if (Application.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
        {
            Transform myTransform = this.transform;
            Ray ray = new Ray(new Vector3(myTransform.position.x, myTransform.position.y + 100, myTransform.position.z), - Vector3.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                myTransform.position = new Vector3(myTransform.position.x, hit.point.y, myTransform.position.z) + (Vector3.up * HoverScript.RAY_LENGTH);
            }
        }
    }
#endif

}

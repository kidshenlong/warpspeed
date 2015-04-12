using UnityEngine;
using System.Collections;

public class GravitySwitchScript : MonoBehaviour 
{
    public Vector3 gravity = Physics.gravity;
    private Vector3 defaultGravity;

    void Awake() 
    {
        defaultGravity = Physics.gravity;
        Physics.gravity = gravity;
    }

    void OnDestroy() 
    {
        Physics.gravity = defaultGravity;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HoverScript : MonoBehaviour {

    /******************************************************
     * variables
     ******************************************************/

    public static readonly float RAY_LENGTH = 2f;

    public static readonly float HOVER_UPPER_BOUND = 1.5f;
    public static readonly float HOVER_CENTER_BOUND = 1f;
    public static readonly float HOVER_LOWER_BOUND = 0.5f;

    public static readonly float HOVER_BALANCE_MAX_SPEED = 2f;
    public static readonly float HOVER_BALANCE_ACCELERATION = 5f;

    public static readonly float HOVER_ANIMATE_MAX_SPEED = 1f;
    public static readonly float HOVER_ANIMATE_ACCELERATION = 1f;

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private bool isMovingUp;

    /******************************************************
     * MonoBehaviour methods, Start
     ******************************************************/

	void Start () 
    {
        // save transform and rigidbody into local variable, for performance.
        this.myTransform = transform;
        this.myRigidbody = rigidbody;
	}

    /******************************************************
     * MonoBehaviour methods, FixedUpdate
     ******************************************************/

    void FixedUpdate () 
    {
        // cast a ray from the object that is directed downwards
        Ray ray = new Ray(myTransform.position, -Vector3.up);
        RaycastHit hit;
        // if no collider is detected from a distance of RAY_LENGTH then do nothing
        if (!Physics.Raycast(ray, out hit, RAY_LENGTH) 
            && hit.distance < RAY_LENGTH)
            return;

        // null the effects of gravity
        myRigidbody.AddForce(-Physics.gravity * myRigidbody.mass);

        // if the object is moving
        Vector3 vel = myRigidbody.velocity;
        if (vel.x < -0.1f || vel.x > 0.1f || vel.z < -0.1f || vel.z > 0.1f)
        {
            // apply a balancing force to the object so the hover distance is equal to the HOVER_CENTER_BOUND
            if (hit.distance > HOVER_CENTER_BOUND) 
            {
                if (vel.y < -HOVER_BALANCE_MAX_SPEED)
                {
                    myRigidbody.AddForce(Vector3.up * HOVER_BALANCE_ACCELERATION * myRigidbody.mass);
                }
                else if (vel.y > -HOVER_BALANCE_MAX_SPEED)
                    myRigidbody.AddForce(-Vector3.up * HOVER_BALANCE_ACCELERATION * myRigidbody.mass);
            }
            else
            {
                if (vel.y < HOVER_BALANCE_MAX_SPEED)
                    myRigidbody.AddForce(Vector3.up * HOVER_BALANCE_ACCELERATION * myRigidbody.mass);
                else if (vel.y > HOVER_BALANCE_MAX_SPEED)
                    myRigidbody.AddForce(-Vector3.up * HOVER_BALANCE_ACCELERATION * myRigidbody.mass);
            }
        }
        // if not, do hover animation
        else
        {
            // apply a tiny force forward so the camera can work properly
            myRigidbody.AddForce(myTransform.forward / 2f);

            // is animating up
            if (isMovingUp)
            {
                // apply a balancing force to the object so the the up velocity is equal to the HOVER_SPEED
                if (vel.y < HOVER_ANIMATE_MAX_SPEED)
                    myRigidbody.AddForce(Vector3.up * HOVER_ANIMATE_ACCELERATION * myRigidbody.mass);
                else if (vel.y > HOVER_ANIMATE_MAX_SPEED)
                    myRigidbody.AddForce(-Vector3.up * HOVER_ANIMATE_ACCELERATION * myRigidbody.mass);

                // if the object has past the upper bound, switch to animating down
                if (hit.distance > HOVER_UPPER_BOUND)
                    isMovingUp = false;
            }
            // is animating down
            else
            {
                // apply force to the object so the the up velocity is equal to the -HOVER_SPEED
                if (vel.y < -HOVER_ANIMATE_MAX_SPEED)
                    myRigidbody.AddForce(Vector3.up * HOVER_ANIMATE_ACCELERATION * myRigidbody.mass);
                else if (vel.y > -HOVER_ANIMATE_MAX_SPEED)
                    myRigidbody.AddForce(-Vector3.up * HOVER_ANIMATE_ACCELERATION * myRigidbody.mass);

                // if the object has past the lower bound, switch to animating up
                if (hit.distance < HOVER_LOWER_BOUND)
                    isMovingUp = true;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerMotionScript : MonoBehaviour , IPlayerMotionListener
{
    /******************************************************
     * Variables
     ******************************************************/

    private static readonly float RAYCAST_DISTANCE = 5f;
    private static readonly float MAX_ROTATION_ANGLE = 15f;
    private static readonly float TURN_ROTATION_SPEED = 5f;

    private Vector3 startRotation;
    private Transform myTransform;

    private PlayerTurnMotion currentTurnMotion = PlayerTurnMotion.Center;
    private Quaternion turnMotion;

    /******************************************************
     * MonoBehaviour methods, Awake
     ******************************************************/

    void Awake() {
        this.myTransform = transform;
        startRotation = myTransform.eulerAngles;
    }

    /******************************************************
     * MonoBehaviour methods, Start
     ******************************************************/

    void Start()
    {
        PlayerScript player = transform.parent.parent.GetComponent<PlayerScript>();
        player.addMotionListener(this);
    }

    /******************************************************
     * MonoBehaviour methods, Update
     ******************************************************/

	void Update () 
    {
        
        // path normal rotation
        RaycastHit hit;
        if (Physics.Raycast(myTransform.position, -Vector3.up, out hit))
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                (Quaternion.FromToRotation(myTransform.up, hit.normal)
                    * myTransform.rotation), Time.deltaTime * 2f);
        }
        else
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                new Quaternion(), Time.deltaTime * 2f);
        }
        
        
        // turn rotation
        switch (currentTurnMotion)
        {
            case PlayerTurnMotion.Right:
                myTransform.localRotation = Quaternion.Slerp(
                    myTransform.localRotation,
                    myTransform.localRotation *
                    Quaternion.Euler(Vector3.forward *
                        ((myTransform.localEulerAngles.z > 360 - MAX_ROTATION_ANGLE ||
                            myTransform.localEulerAngles.z < MAX_ROTATION_ANGLE
                        ) ?
                            (360 - MAX_ROTATION_ANGLE)
                                - myTransform.localEulerAngles.z
                            : 
                            MAX_ROTATION_ANGLE)),
                    Time.deltaTime * TURN_ROTATION_SPEED);
                break;
            case PlayerTurnMotion.Left:
                myTransform.localRotation = Quaternion.Slerp(
                    myTransform.localRotation,
                    myTransform.localRotation *
                    Quaternion.Euler(Vector3.forward *
                        ((myTransform.localEulerAngles.z > MAX_ROTATION_ANGLE) ?
                            MAX_ROTATION_ANGLE - myTransform.localEulerAngles.z
                            :
                            MAX_ROTATION_ANGLE)),
                    Time.deltaTime * TURN_ROTATION_SPEED);
                break;
            case PlayerTurnMotion.Center:
                myTransform.localRotation = Quaternion.Slerp(
                    myTransform.localRotation,
                    Quaternion.Euler(Vector3.zero) * myTransform.localRotation,
                    Time.deltaTime * 5f);
                break;
        }
	}

    /******************************************************
     * Unused IPlayerMotionListener methods
     ******************************************************/

    public void OnAccelrate(){}
    public void OnIdle(){}
    public void OnReverse(){}
    public void OnUpdateTurnLeft(){}
    public void OnUpdateTurnRight(){}

    /******************************************************
     * IPlayerMotionListener methods, OnTurnLeft
     ******************************************************/

    public void OnTurnLeft()
    {
        currentTurnMotion = PlayerTurnMotion.Left;
    }

    /******************************************************
     * IPlayerMotionListener methods, OnTurnRight
     ******************************************************/

    public void OnTurnRight()
    {
        currentTurnMotion = PlayerTurnMotion.Right;
    }

    /******************************************************
     * IPlayerMotionListener methods, OnCenter
     ******************************************************/

    public void OnCenter()
    {
        currentTurnMotion = PlayerTurnMotion.Center;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PathFollowScript))]
public class PlayerControls : MonoBehaviour
{
    public PlayerScript player;
    public int itemeffect;
    public PathFollowScript pathFollower;
    public Transform myTransform;

    // control toggles
    public bool isAccel;
    public bool isReversing;
    public bool turningRight;
    public bool turningLeft;
    // is movement allowed?
    public bool isMoving = true;

    void Start()
    {
        player = GetComponent<PlayerScript>();
        pathFollower = GetComponent<PathFollowScript>();
        myTransform = transform;
    }

    void Update()
    {
        if (isMoving == true)
        {
            // Item Controls
            if (Input.GetKeyDown(KeyCode.Return))
            {
                /*if (pathFollower.enabled == true)
                {
                    pathFollower.repathPoints();
                    pathFollower.enabled = false;
                }
                else
                {
                    pathFollower.enabled = true;
                    isAccel = false;
                    isReversing = false;
                    turningLeft = false;
                    turningRight = false;
                }*/

                if (player.itemHeld == 0f)
                {

                }
                else
                {
                    //Debug.Log(player.itemHeld);
                    itemeffect = GetComponent<itemEffect>().effect(player.itemHeld);
                    //might want to move this into the itemeffect script...
                    //player.itemHeld = 0;
                }
            }
            if (!pathFollower.enabled)
            {
                // accel control
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                    player.applyAcceleration();

                else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
                    player.applyIdleMotion();

                // reverse control
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S))
                    player.applyReverse();
                else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.S))
                    player.applyIdleMotion();

                // right control
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                    player.turnRight();
                else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
                    player.turnCenter();

                // left control
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                    player.turnLeft();
                else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
                    player.turnCenter();
            }
        }
		else{
			player.applyIdleMotion();
			player.turnCenter();
		}
    }

    void FixedUpdate()
    {
        /*
#if !(UNITY_EDITOR || !(UNITY_IPHONE || UNITY_ANDROID))
        float x = Input.acceleration.x;
        if (x < -0.1f)
            player.turnLeft(-x/2f);
        else if(x > 0.1f)
            player.turnRight(x/2f);
#endif
         */
    }
}

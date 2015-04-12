using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerScript))]
public class PathFollowScript : MonoBehaviour, IAccelMultiplier
{
    /******************************************************
     * variables
     ******************************************************/

    private static readonly int layerMask = (~0) ^ (1 << 10);
    private static readonly int terrainMask = 1 << 8;

    public static readonly float NEXT_DISTANCE = 5f;
    public static readonly float SMOOTH_MARGIN = 6f;
    public static readonly float GAP_MARGIN = 15f;
    public static readonly float PIT_MARGIN = 10f;

    public static readonly float NUM_OF_INTERVALS = 5f;
    public static readonly float INTERVAL_MARGIN = 2f;

    public PathPoint pathPoint;
    public PathPoint pathPointNextClosest;

    public PlayerScript player;

    private Transform myTransform;
    // is movement allowed?
    public bool isMoving = true;

    // anti stuck var
    private static readonly float STUCK_DISABLE_TIME = 2;
    private static readonly float REQUIRED_STUCK_TIME = 1;
    private static readonly float REQUIRED_MOVE_RESET = 5;
    private bool stuckDisable;
    private float stuckDisableTimeCounter;
    private float stuckTimeCounter;
    private float stuckMoveCounter;
    private Vector3 lastUpdatePosition;

    /******************************************************
     * MonoBehaviour methods, Start
     ******************************************************/

    void Start()
    {
        pathPoint = PathManager.FindClosestPoint(this.transform.position);
        pathPointNextClosest = pathPoint.getNextClosestPathPoint();
        player = GetComponent<PlayerScript>();
        this.myTransform = transform;
    }

    /******************************************************
     * MonoBehaviour methods, FixedUpdate
     ******************************************************/

    void FixedUpdate()
    {
        if (isMoving && !stuckDisable)
        {
            Vector3 current3dPos = myTransform.position;
            Vector2 current2dPos = new Vector2(current3dPos.x, current3dPos.z);

            float angle = Vector2.Angle(pathPointNextClosest.Pos2d - pathPoint.Pos2d, current2dPos - pathPoint.Pos2d);
            //Debug.Log(angle);

            // if the distance between current pos to pathpoint + pathpoint to next pathpoint 
            // is greater than going from current to next pathpoint
            if ((Vector2.Distance(pathPoint.Pos2d, current2dPos) +
                Vector2.Distance(pathPoint.Pos2d, pathPointNextClosest.Pos2d)
                > Vector2.Distance(pathPointNextClosest.Pos2d, current2dPos) + SMOOTH_MARGIN ||
                Vector2.Distance(pathPoint.Pos2d, current2dPos) < SMOOTH_MARGIN ||
                (angle < 120)) &&
                Vector2.Distance(pathPoint.Pos2d, current2dPos) < SMOOTH_MARGIN * 4 ||
                (angle < 100)
                )
            {
                pathPoint = pathPoint.getNextWeightedPathPoint();
                pathPointNextClosest = pathPoint.getNextClosestPathPoint();
            }

            Quaternion saveRotation = myTransform.rotation;

            Vector2 direction = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
            Vector3 relativePoint = transform.InverseTransformPoint(pathPoint.transform.position);
            Vector3 forward = myTransform.forward;
            transform.rotation = saveRotation;

            Vector3 targetDir = pathPoint.transform.position - transform.position;
            Vector3 normal = rigidbody.velocity.normalized;

            angle = Vector3.Angle(targetDir, forward);


            bool hasTurned = false;

            // collision turning rays
            Ray forwardRay = new Ray(current3dPos, rigidbody.velocity.normalized);
            Ray forwardDownRay = new Ray(current3dPos + (rigidbody.velocity.normalized * GAP_MARGIN), -Vector3.up);

            Ray leftRay = new Ray(current3dPos, -myTransform.right);
            Ray leftDownRay = new Ray(current3dPos + (-myTransform.right * GAP_MARGIN), -Vector3.up);

            Ray rightRay = new Ray(current3dPos, myTransform.right);
            Ray rightDownRay = new Ray(current3dPos + (myTransform.right * GAP_MARGIN), -Vector3.up);

            bool forwardTest = Physics.Raycast(forwardRay, GAP_MARGIN, layerMask);
            bool forwardDownTest = Physics.Raycast(forwardDownRay, PIT_MARGIN, layerMask);

            bool leftTest = Physics.Raycast(leftRay, GAP_MARGIN, layerMask);
            bool leftDownTest = Physics.Raycast(leftDownRay, PIT_MARGIN, layerMask);

            bool rightTest = Physics.Raycast(rightRay, GAP_MARGIN, layerMask);
            bool rightDownTest = Physics.Raycast(rightDownRay, PIT_MARGIN, layerMask);

            // pit detection turning
            if (!forwardDownTest && !hasTurned)
            {
                if (!leftDownTest)
                {
                    player.turnRight();
                    hasTurned = true;
                }
                else if (!rightDownTest)
                {
                    player.turnLeft();
                    hasTurned = true;
                }
            }

            // obstacle turning
            if (forwardTest && !hasTurned)
            {
                bool stopLeftTesting = false;
                bool stopRightTesting = false;

                for (int i = 1; i <= NUM_OF_INTERVALS; i++)
                {
                    if (!stopLeftTesting)
                    {
                        if (!Physics.Raycast(leftRay, INTERVAL_MARGIN * i, layerMask))
                        {
                            Vector3 leftPoint = current3dPos + (-myTransform.right * INTERVAL_MARGIN * i);
                            Vector3 leftVector = leftPoint - pathPoint.Pos3d;
                            Ray leftFollowIntervalRay = new Ray(leftPoint, leftVector.normalized);
                            if (Physics.Raycast(leftFollowIntervalRay, leftVector.magnitude))
                            {
                                Ray leftIntervalDownRay = new Ray(leftPoint, -Vector3.up);
                                if (!Physics.Raycast(leftIntervalDownRay, PIT_MARGIN))
                                {
                                    player.turnLeft();
                                    hasTurned = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            stopLeftTesting = true;
                        }
                    }

                    if (!stopRightTesting)
                    {
                        if (!Physics.Raycast(rightRay, INTERVAL_MARGIN * i, layerMask))
                        {
                            Vector3 rightPoint = current3dPos + (myTransform.right * INTERVAL_MARGIN * i);
                            Vector3 rightVector = rightPoint - pathPoint.Pos3d;
                            Ray rightIntervalRay = new Ray(rightPoint, rightVector.normalized);
                            if (Physics.Raycast(rightIntervalRay, rightVector.magnitude))
                            {
                                Ray rightIntervalDownRay = new Ray(rightPoint, Vector3.up);
                                if (!Physics.Raycast(rightIntervalDownRay, PIT_MARGIN))
                                {
                                    player.turnRight();
                                    hasTurned = true;
                                    break;
                                }
                                else
                                    stopRightTesting = true;
                            }
                        }
                        else
                        {
                            stopRightTesting = true;
                        }
                    }
                }

                /*
                if (!hasTurned)
                {
                    if (!leftTest)
                    {
                        if (leftDownTest)
                        {
                            player.turnRight();
                            hasTurned = true;
                        }
                    }
                    else if (!rightTest)
                    {
                        if (rightDownTest)
                        {
                            player.turnLeft();
                            hasTurned = true;
                        }
                    }
                }*/
            }

            // normal turning
            if (!hasTurned)
            {
                if (relativePoint.x < 0f && angle > 15)
                    player.turnLeft();
                else if (relativePoint.x > 0f && angle > 15)
                    player.turnRight();
                else
                    player.turnCenter();
            }


            // normal accelerate
            if (angle < 90)
                player.applyAcceleration();
            else
                player.applyIdleMotion();

            stuckTimeCounter += Time.deltaTime;
            stuckMoveCounter += Vector3.Distance(myTransform.position, lastUpdatePosition);
            lastUpdatePosition = myTransform.position;
            if (stuckMoveCounter >= REQUIRED_MOVE_RESET)
            {
                stuckTimeCounter = 0;
                stuckMoveCounter = 0;
            }
            else if (stuckTimeCounter >= REQUIRED_STUCK_TIME)
            {
                stuckTimeCounter = 0;
                stuckMoveCounter = 0;
                stuckDisable = true;
            }
        }
		
		
        else if(stuckDisable)
        {
            stuckDisableTimeCounter += Time.deltaTime;
            player.applyReverse();
			
				
            if (stuckDisableTimeCounter >= STUCK_DISABLE_TIME) 
            {
                stuckDisableTimeCounter = 0;
                stuckDisable = false;
            }
        }
			else if (isMoving == false){
			
			player.applyIdleMotion();
			 player.turnCenter();
			
		}
	
    }

    /******************************************************
     * MonoBehaviour methods, OnDrawGizmos
     ******************************************************/

    void OnDrawGizmos()
    {
        if (pathPoint != null && (player==null || !player.IsHuman))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, pathPoint.transform.position);
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity);
    }

    /******************************************************
     * repathPoints
     ******************************************************/
    /// <summary> set the path to the closest node </summary>

    public void repathPoints()
    {
        pathPoint = PathManager.FindClosestPoint(this.transform.position);
        pathPointNextClosest = pathPoint.getNextClosestPathPoint();
    }

    /******************************************************
     * adjustAccelMultiplier
     ******************************************************/

    public float adjustAccelMultiplier(float multiplier)
    {
        if (enabled && pathPoint != null)
            return multiplier * pathPoint.AccelMultiplier;
        else
            return multiplier;
    }
}


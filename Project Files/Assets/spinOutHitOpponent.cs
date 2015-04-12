using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class spinOutHitOpponent : MonoBehaviour {
	//public int speed= 50;
	public GameObject positionchecker;
	public Transform target;
	//public Transform origin;
	//public int numOfPlayers;
	//public List<Transform> players = new List<Transform>();
	
	public float speed = 200.0f;
	
	// PATH FOLLOW STUFF
    public PathPoint pathPoint;
    public PathPoint pathPointNextClosest;

    public PlayerScript player;

    private Transform myTransform;

    public static readonly float NEXT_DISTANCE = 5f;
    public static readonly float SMOOTH_MARGIN = 6f;

    // PATH FOLLOW STUFF

	
	
	// Use this for initialization
	void Start () {
		
		positionchecker = GameObject.Find("positionchecker");

			//this.transform;// players[1].transform; 	
	//target = GetComponent<PlayerPositionScript>().position2[0];
		target = positionchecker.transform.GetComponent<positionchecker>().positions[0];
		           pathPoint = PathManager.FindClosestPoint(this.transform.position);
         //pathPoint = pathPoint.getNextClosestPathPoint();
           pathPointNextClosest = pathPoint.getNextClosestPathPoint();
           //player = GetComponent<PlayerScript>();
           this.myTransform = transform;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	target = positionchecker.transform.GetComponent<positionchecker>().positions[0];
	}
	
	
	
    void FixedUpdate()
    {
        float distancetotarget = Vector3.Distance(this.transform.position, target.transform.position);
        //Debug.Log(distancetotarget);
        if(distancetotarget>50){
        /*pathPoint = PathManager.FindClosestPoint(this.transform.position);
        pathPointNextClosest = pathPoint.getNextClosestPathPoint();*/

        Vector3 current3dPos = myTransform.position;
        Vector2 current2dPos = new Vector2(current3dPos.x, current3dPos.z);

        float angle = Vector2.Angle(pathPointNextClosest.Pos2d - pathPoint.Pos2d, current2dPos - pathPoint.Pos2d);
        //Debug.Log(angle);
        transform.LookAt(pathPointNextClosest.transform.position);

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


       transform.rigidbody.velocity = transform.forward * speed;
        } else{
			transform.LookAt(target.transform.position);
			transform.rigidbody.velocity = transform.forward * 300f;
            //Destroy(this.gameObject);

        } 
		
    }
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player")
        {
			other.transform.GetComponent<PlayerScript>().HP = other.transform.GetComponent<PlayerScript>().HP - 50;
		    Destroy(this.gameObject);
		}
	}
}

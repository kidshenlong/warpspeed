using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class hitOpponent : MonoBehaviour {
	//public int speed= 50;
	public Transform target;
	public Transform origin;
	//public int numOfPlayers;
	public List<Transform> players = new List<Transform>();
	
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
	foreach (GameObject i in GameObject.FindGameObjectsWithTag("Player")){
			if(i.transform == origin){
			}else{
			players.Add(i.transform);
			}
		}
		
		   for (int i = 0; i < players.Count ; i++)
        {
			
			float tempdist = Vector3.Distance(transform.position, players[i].transform.position);	
			float dist = Vector3.Distance(transform.position, players[0].transform.position);	
		if (tempdist<dist && tempdist > 0f){
			target = players[i].transform; 

		
	}else{
			target = players[1].transform; 	
			}
	}
		
		           pathPoint = PathManager.FindClosestPoint(this.transform.position);
         //pathPoint = pathPoint.getNextClosestPathPoint();
           pathPointNextClosest = pathPoint.getNextClosestPathPoint();
           //player = GetComponent<PlayerScript>();
           this.myTransform = transform;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	
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
        /*
        Quaternion saveRotation = myTransform.rotation;

        Vector2 direction = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        Vector3 relativePoint = transform.InverseTransformPoint(pathPoint.transform.position);
        Vector3 forward = myTransform.forward;
        transform.rotation = saveRotation;

        Vector3 targetDir = pathPoint.transform.position - transform.position;
        Vector3 normal = rigidbody.velocity.normalized;

        angle = Vector3.Angle(targetDir, forward);*/

       transform.rigidbody.velocity = transform.forward * speed;
        } else{
			transform.LookAt(target.transform.position);
			transform.rigidbody.velocity = transform.forward * 300f;
            //Destroy(this.gameObject);

        } 
		
    }
	
	void OnCollisionEnter(Collision other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
		if(player != null && other.transform != origin)
        {
            player.HP -= HPSettings.MissileDamage;
		    Destroy(this.gameObject);
		}
	}
	
	
	
}

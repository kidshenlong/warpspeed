using UnityEngine;
using System.Collections;

public class shieldBehaviour : MonoBehaviour {
	
	  public GameObject Track;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Track = GameObject.Find("TrackFBX1");
	Physics.IgnoreCollision(collider, Track.collider);
	}
}

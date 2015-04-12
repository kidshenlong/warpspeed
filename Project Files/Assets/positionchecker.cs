using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class positionchecker : MonoBehaviour {
	
	public List<Transform> positions = new List<Transform>();
	public Transform[] checkPointArray;
	

	// Use this for initialization
	void Start () {
	
		foreach (GameObject i in GameObject.FindGameObjectsWithTag("Player")){
			positions.Add(i.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
        positions = positions.OrderByDescending(x => x.GetComponent<PlayerPositionScript>().currentprogress).ToList();
	}
}

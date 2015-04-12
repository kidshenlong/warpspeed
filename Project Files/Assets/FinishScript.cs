using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishScript : MonoBehaviour {
	
	public List<Transform> players = new List<Transform>();

	// Use this for initialization
	void Start () {
		foreach (GameObject i in GameObject.FindGameObjectsWithTag("Player")){
			players.Add(i.transform);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	public AudioClip sceneMus;
	// Use this for initialization
	void Start () {
	
		
		audio.loop = true;
		audio.clip = sceneMus;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}

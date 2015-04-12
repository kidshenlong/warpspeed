using UnityEngine;
using System.Collections;

public class CameraRespawnScript : MonoBehaviour {
	public int cameracount;
	//public GameObject[] = Camera.allCameras;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	 void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           PlayerScript player = other.GetComponent<PlayerScript>();
			
			if(player.IsHuman == true ){
				player.GetComponent<PlayerControls>().isMoving = false;
			}else{
				player.GetComponent<PathFollowScript>().isMoving = false;
				
			}
			cameracount = Camera.allCameras.Length;
			for(int i = 0; i<cameracount; i++){
				if(Camera.allCameras[i].GetComponent<CarCamera>()!=null){
				if(Camera.allCameras[i].GetComponent<CarCamera>().targetPlayerId == player.playerId){
				Camera.allCameras[i].GetComponent<CarCamera>().isRespawning = true;
						
						Camera.allCameras[i].GetComponent<fadeScript>().fadeOut=true;
						
				//Debug.Log (Camera.allCameras[i]);
				}
				}
			}
         
        }
    }
}

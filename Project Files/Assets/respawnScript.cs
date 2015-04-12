using UnityEngine;
using System.Collections;

public class respawnScript : MonoBehaviour
	
	//public int cameracount;
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            player.HP -= HPSettings.RespawnHPCost;

            PlayerPositionScript posScript = other.GetComponent<PlayerPositionScript>();

            Transform previouscheckpoint = posScript.previous;
            other.gameObject.transform.position = new Vector3(previouscheckpoint.position.x, -3, previouscheckpoint.position.z);

            Vector3 currentcheckpoint = new Vector3(posScript.current.position.x, 0, posScript.current.position.z);
            other.gameObject.transform.LookAt(currentcheckpoint);

            PathFollowScript follower = other.GetComponent<PathFollowScript>();
            follower.repathPoints();
			
			int cameracount = Camera.allCameras.Length;
			for(int i = 0; i<cameracount; i++){
				if(Camera.allCameras[i].GetComponent<CarCamera>()!=null){
				if(Camera.allCameras[i].GetComponent<CarCamera>().targetPlayerId == player.playerId){
				Camera.allCameras[i].GetComponent<CarCamera>().isRespawning = false;
						Camera.allCameras[i].GetComponent<fadeScript>().fadeOut = false;
				Debug.Log (Camera.allCameras[i]);
				}
				}
			}
			
			
			if(player.IsHuman == true){
				other.GetComponent<PlayerControls>().isMoving = true;
				
			}else{
				follower.isMoving = true;
			}
			if(other.GetComponent<PlayerScript>().playerStatus == 1){
				other.GetComponent<itemEffect>().poisonOff();
			//other.GetComponent<PlayerScript>().playerStatus = 0;
				
			}else if(other.GetComponent<PlayerScript>().playerStatus == 2){
				other.GetComponent<itemEffect>().shieldOff();
				//other.GetComponent<PlayerScript>().playerStatus = 0;
				
				
			}
			
			if(other.GetComponent<itemEffect>().isAutoPilot== true){
				other.GetComponent<itemEffect>().autoPilotOff();
			}
			if(player.itemHeld != 0){
				player.itemHeld = 0;
			}
        }
    }
}

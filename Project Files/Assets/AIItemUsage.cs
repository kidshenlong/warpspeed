using UnityEngine;
using System.Collections;

public class AIItemUsage : MonoBehaviour {
	
	public int itemHeld;
	public int useItem;
	public int  useItemCap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<PlayerScript>().isHuman==false){
		itemHeld = GetComponent<PlayerScript>().itemHeld;
		if(GetComponent<PlayerPositionScript>().raceposition<=3){
			//aggressive 
			useItemCap = 25;
		}else{
			//passive	
			useItemCap = 50;
		}
	
		
		if (itemHeld!=0){
			useItem = Random.Range(1,useItemCap);
			if(useItem==5){
			// GetComponent<itemEffect>().effect(itemHeld);
			GetComponent<itemEffect>().effect(itemHeld);
				//useItem = 0;
			}
			
		}
		}
	}
}

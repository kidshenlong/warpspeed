using UnityEngine;
using System.Collections;

public class UIItemHeldScript : MonoBehaviour {
	
	private UILabel label;
    private PlayerScript player;
    public int playerId;
	
	
	
	   void Awake()
    {
        label = GetComponent<UILabel>();

    }
	// Use this for initialization
	void Start () {
	
		        player = PlayerScript.FindPlayerById(playerId);
        if (player == null)
        {
            Debug.LogWarning("player Id=" + playerId + " cant be found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
		        if (player != null)
        {
			
			
		if(player.itemHeld>=1 && player.itemHeld <=15){
           
					label.text = "Poison";	
                
		}else if (player.itemHeld>= 16 && player.itemHeld <=35){
			
				label.text = "No Item";	
                  
			
		}else if( player.itemHeld>=36 && player.itemHeld<= 50){
			
			label.text = "Shield";	
				
			
		}else if( player.itemHeld>=51 && player.itemHeld <=55){
			label.text = "Autopilot";	
				
			
		}else if( player.itemHeld>=56 && player.itemHeld <=70){
			label.text = "Missile";
				
			
		}else if( player.itemHeld >=71 && player.itemHeld <=85){
		label.text = "Ultra Missile";	
				
		
		}else if(player.itemHeld>=86 && player.itemHeld <=100){
			label.text = "Boost";	
				
		
		}else{
			label.text = "No Item";	
				
		}
            //label.text =  "" + player.itemHeld;
        }
		
		
	}
}

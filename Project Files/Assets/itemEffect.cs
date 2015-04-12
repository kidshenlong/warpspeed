using UnityEngine;
using System.Collections;


public class itemEffect : MonoBehaviour {

	// Use this for initialization
	
	public Material shieldMat;
	public Material poisonMat;
	public Material initialMat;
	
	/*public Renderer shieldMat;
	public Renderer poisonMat;
	public Renderer initialMat;*/
	
	
	public Transform missile;
	public Transform spinMissile;
	//public Transform ship;
	//public GameObject ship;
	public Renderer ship;
	
	public bool newPoison;
	public bool isAutoPilot;
	public bool poisonOn = false;
	public bool healthOn = false;
	public bool shieldOn = false;
	
	public GameObject autopilottext;
	public GameObject autopilotsprite;
	
	public float initialAcceleration;
	
	public float racePosition;
	

	void Start () {
	
		//ship = this.GetComponentInChildren.<Renderer>() as Renderer;
		//ship = this.GetComponentInChildren<Renderer>();
		//ship = this.GetComponentInChildren<GameObject>();
		//ship = transform.FindChild("Anchor");
		
		//ship2 = ship.GetComponentInChildren<Renderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		
		  if(GetComponent<PlayerScript>().itemHeld>=1 && GetComponent<PlayerScript>().itemHeld <=15){
                    //might want to move this into the itemeffect script...
		  
                    //might want to move this into the itemeffect script...
			if(poisonOn==false){
					poison();
			}
                 //  GetComponent<PlayerScript>().itemHeld = 0;
		}else if (GetComponent<PlayerScript>().itemHeld>= 16 && GetComponent<PlayerScript>().itemHeld <=35){
			if(healthOn==false){
				health();
			}
                  // GetComponent<PlayerScript>().itemHeld = 0;
			
		}
		
		
		if(GetComponent<PlayerScript>().playerStatus == 1)
        {
			GetComponent<PlayerScript>().HP -= HPSettings.PoisonDamage * Time.deltaTime;
			
		} 
        else if(GetComponent<PlayerScript>().playerStatus == 2)
        {
			GetComponent<PlayerScript>().HP += HPSettings.ShieldRegeneration * Time.deltaTime;
		}
		
		if(isAutoPilot==true){
			if(GetComponent<PlayerPositionScript>().raceposition==1){
				autoPilotOff();
			}
		}
	}
	
	public int effect(int item){

		if( item>=36 && item<= 50){
			
			shield();
			return item;	
		}
		else if( item>=51 && item <=55){
			autoPilotOn();
			return item;
		}
		
		else if( item>=56 && item <=70){
			launchMissile();
			return item;
		}
		else if( item>=71 && item <=85){
		spinOutMissile();
				return item;
		}
			
			
		else if( item>=86 && item <=100){
			boost ();
			return item;
		}
		else{
			return item;		
		}
		
		//return 
	}
	
	
	/*public void shield(){
		this.renderer.material = rim;
		
	}*/
	
	public void shield(){
		if(shieldOn==false){
		StartCoroutine(shieldstart());
		}
	}
	
	IEnumerator shieldstart(){
		shieldOn= true;
		/*foreach (Transform child in transform)
{*/
    // do whatever you want with child transform object here
			//initialMat = ship.material;
		ship.material = shieldMat;
		//ship.gameObject.layer = LayerMask.NameToLayer("Shield");
		Debug.Log ("Shield On");
		this.GetComponent<PlayerScript>().playerStatus = 2;
		yield return new WaitForSeconds(10);
		
		if(GetComponent<PlayerScript>().playerStatus==2){
		shieldOff();
		}
		
//}
		
	}
	
	public void shieldOff(){
		
		GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log ("Shield Off");
		StopCoroutine("shieldStart");
		this.GetComponent<PlayerScript>().playerStatus = 0;
		
		ship.material = initialMat;
		shieldOn= false;
		
	}
	
	
	
		public void poison(){
		poisonOn = true;
		StartCoroutine(poisonstart());
	}
		IEnumerator poisonstart(){
		
			/*foreach (Transform child in transform)
{*/
		//initialMat = ship.material;
		ship.material = poisonMat;
		Debug.Log ("Poison Effect On");
			this.GetComponent<PlayerScript>().playerStatus = 1;
		StartCoroutine(poisonCounter());
		yield return new WaitForSeconds(10);
		if(GetComponent<PlayerScript>().playerStatus==1){
		poisonOff();
		}
		//}

	}
	
	public void poisonOff(){
		 GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log ("Poison Effect Off");
		StopCoroutine("poisonStart");
			this.GetComponent<PlayerScript>().playerStatus = 0;
		
		ship.material = initialMat;
		poisonOn = false;
	}
	
		IEnumerator poisonCounter(){
		newPoison = true;
	
		yield return new WaitForSeconds(3);
		
		newPoison = false;
}
	
	
	
	public void boost(){
		 GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log ("Boost");
		StartCoroutine(speedCapTimer());
		this.rigidbody.AddForce(this.transform.forward * 5000);
		
	}
	
	IEnumerator speedCapTimer(){
		 GetComponent<PlayerScript>().speedCap = false;
		yield return new WaitForSeconds(2);
		 GetComponent<PlayerScript>().speedCap = true;
		
	}
	
	public void health()
    {
		GetComponent<PlayerScript>().HP += HPSettings.HealthRegeneration;
		healthOn = false;
		GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log("Health Up!");
	}
	
	public void launchMissile(){
		 GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log ("Missile Launch");
		Transform newMissile = (Transform)Instantiate(missile, new Vector3(transform.position.x-10, transform.position.y+1, transform.position.z), Quaternion.identity);
		
		Debug.Log (newMissile.transform.name);
		 newMissile.GetComponent<hitOpponent>().origin = transform;
	}
	
		public void autoPilotOn(){
		StartCoroutine(autopilotstart());
	}
		IEnumerator autopilotstart(){
		
		if(GetComponent<PlayerScript>().isHuman==true){
		
		autopilottext.SetActive(true);
		autopilotsprite.SetActive(true);
		}
		isAutoPilot=true;
		initialAcceleration = GetComponent<PlayerScript>().acceleration;
		GetComponent<PlayerScript>().acceleration = 5000f;
		
		GetComponent<PathFollowScript>().enabled = true;
		yield return new WaitForSeconds(15);
		if(isAutoPilot==true){
		autoPilotOff();
		}
		
	}
	
	public void autoPilotOff(){
		if(GetComponent<PlayerScript>().isHuman==true){
				autopilottext.SetActive(false);
		autopilotsprite.SetActive(false);	
		}
		 GetComponent<PlayerScript>().itemHeld = 0;
		GetComponent<PlayerScript>().acceleration = initialAcceleration;
		GetComponent<PlayerScript>().applyIdleMotion();
		GetComponent<PlayerScript>().turnCenter();
		GetComponent<PathFollowScript>().enabled = false;
		
		
		isAutoPilot=false;
		
	}
	
	public void spinOutMissile(){
				 GetComponent<PlayerScript>().itemHeld = 0;
		Debug.Log ("Missile Launch");
		Transform newSpinMissile = (Transform)Instantiate(spinMissile, new Vector3(transform.position.x-10, transform.position.y+1, transform.position.z), Quaternion.identity);
		
		Debug.Log (newSpinMissile.transform.name);
		 //newMissile.GetComponent<hitOpponent>().origin = transform;
		
		
	}
}

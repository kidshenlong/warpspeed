using UnityEngine;
using System.Collections;

public class fadeScript : MonoBehaviour {
	
	//public Texture2D fadeTexture;
	public float fadeSpeed = 0.12f;
	public int drawDepth = -1000;
	
	public float alphaIn = 1f;
	public float alphaOut = 0f;
	//public float alpha = 0f;
	public float fadeDir = -1f;
	
	//public bool fadeIn;
	public bool fadeOut;
	
	
		
	
	void  OnGUI (){
 /*if(fadeIn==true){
			
    alphaIn += fadeDir * fadeSpeed * Time.deltaTime;  
			//alpha = Mathf.Lerp(0,1,5);
    alphaIn = Mathf.Clamp01(alphaIn);   
 
		
		Color thisColor = GUI.color;
		thisColor.a = alphaIn;
    GUI.color = thisColor;
 
    GUI.depth = drawDepth;
		Texture2D fadeTexture = new Texture2D(1,1);
		fadeTexture.SetPixel(0,0,Color.black);
fadeTexture.Apply();
 
    GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		}
		 else if(fadeOut==true){
			
    alphaOut -= fadeDir * fadeSpeed * Time.deltaTime;  
			//alpha = Mathf.Lerp(1,0,5);
    alphaOut = Mathf.Clamp01(alphaOut);   
 
		
		Color thisColor = GUI.color;
		thisColor.a = alphaOut;
    GUI.color = thisColor;
 
    GUI.depth = drawDepth;
		Texture2D fadeTexture = new Texture2D(1,1);
		fadeTexture.SetPixel(0,0,Color.black);
fadeTexture.Apply();
 
    GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		}*/
		
		
		if(fadeOut==true){
			alphaOut -= fadeDir * fadeSpeed * Time.deltaTime; 
			alphaOut = Mathf.Clamp01(alphaOut);
				Color thisColor = GUI.color;
		thisColor.a = alphaOut;
    GUI.color = thisColor;
 
    GUI.depth = drawDepth;
		Texture2D fadeTexture = new Texture2D(1,1);
		fadeTexture.SetPixel(0,0,Color.black);
fadeTexture.Apply();
 
    GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		}else{
			
		alphaOut = 0;	
		}
}
	// Use this for initialization
	void Start () {
	
	}
	
	/*public void fadeIn(){
		
		//alpha += fadeDir * fadeSpeed * Time.deltaTime;  
    alpha = Mathf.Clamp01(alpha);   
 
		
		Color thisColor = GUI.color;
		thisColor.a = alpha;
    GUI.color = thisColor;
 
    GUI.depth = drawDepth;
		Texture2D fadeTexture = new Texture2D(1,1);
		fadeTexture.SetPixel(0,0,Color.black);
fadeTexture.Apply();
 
    GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
	}
	
	public void fadeOut(){
		//alpha -= fadeDir * fadeSpeed * Time.deltaTime;  
		alpha = Mathf.Lerp(0,1,5);
    alpha = Mathf.Clamp01(alpha);   
 
		
		Color thisColor = GUI.color;
		thisColor.a = alpha;
    GUI.color = thisColor;
 
    GUI.depth = drawDepth;
		Texture2D fadeTexture = new Texture2D(1,1);
		fadeTexture.SetPixel(0,0,Color.black);
fadeTexture.Apply(); 
    GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
}

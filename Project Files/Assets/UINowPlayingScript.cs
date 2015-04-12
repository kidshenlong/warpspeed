using UnityEngine;
using System.Collections;

public class UINowPlayingScript : MonoBehaviour, IGameStateListener{
	
	
	    public SpringPosition springPosition;
    public SpringPosition springBackPosition;
	
	
	private UILabel label;
	public GameObject playerCamera;
	public AudioSource[] audio1;
	// Use this for initialization
	
	  void Awake()
    {
        label = GetComponent<UILabel>();

    }
	void Start () {
        GameStateScript.addStateListener(this);

	// AudioSource mainAudio = FindObjectOfType(typeof(AudioSource));
		//mainAudio = GetComponents<AudioSource>();
		
		        audio1 = playerCamera.GetComponents<AudioSource>();
       /* foreach (HingeJoint joint in hingeJoints) {
            joint.useSpring = false;
	}*/
		//audio1 = playerCamera[0];
	}
	
	// Update is called once per frame
	void Update () {
	    if(audio1[0].clip!=null)
        {
	        label.text = "Now Playing - "+ audio1[0].clip.name;
		}
	}
	
	public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.Pause:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;

            case GameState.Play:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
				
        }
    }
}

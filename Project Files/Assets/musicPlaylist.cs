using UnityEngine;
using System.Collections;

public class musicPlaylist : MonoBehaviour {
	public AudioClip[] playList;
	public GameObject gamestate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if( == GameState.Play){
		//if(gamestate.GetComponent<GameStateScript>().gam
	  if(!audio.isPlaying)
        playRandomMusic();
		//}
	}
	
	void  playRandomMusic (){
    audio.clip = playList[Random.Range(0, playList.Length)];
		//audio.clip = playList[4];
    audio.Play();
}
}

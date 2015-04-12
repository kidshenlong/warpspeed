using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPositionScript : MonoBehaviour {

    private UILabel label;
    private PlayerScript player;
    public int playerId;
    public int position;
	/*public GameObject position1;
	public List<Transform> position2;
	public GameObject position3;*/

    void Awake()
    {
        label = GetComponent<UILabel>();

    }
    void Start()
    {
		
		//position1 = GameObject.Find("positionchecker");
        player = PlayerScript.FindPlayerById(playerId);
        if (player == null)
        {
            Debug.LogWarning("player Id=" + playerId + " cant be found.");
        }
    }

    void Update()
    {
		
		
        if (player != null)
        {
			   /*   position2 = position1.GetComponent<positionchecker>().positions;
			for (int i = 0; i<=3; i++){
			if(position2[i].GetComponent<PlayerScript>().playerId ==playerId){	
					for (int j = 0; j<=3; j++){
						if( position2[i].transform == position2[j].transform ){
     		//position = position2[i].GetComponent<playerposition>().position;
            label.text = "Position: "+ (j+1);
					}
					}
			}
			}*/
            label.text = "" + player.GetComponent<PlayerPositionScript>().raceposition;
        }
    }
}

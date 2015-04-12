using UnityEngine;
using System.Collections;

public class UILapScript : MonoBehaviour {

    private UILabel label;
    private PlayerScript player;
    public int playerId;
    public int lap;

    void Awake()
    {
        label = GetComponent<UILabel>();

    }
    void Start()
    {
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
            if (player.GetComponent<PlayerPositionScript>().currentLap == 0)
            {
				
			}
			else{
                label.text = player.GetComponent<PlayerPositionScript>().currentLap + " / 3";
        }
		}
    }
}

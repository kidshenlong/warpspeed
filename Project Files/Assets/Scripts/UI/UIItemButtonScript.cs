using UnityEngine;
using System.Collections;

public class UIItemButtonScript : MonoBehaviour {

    public int playerId;
    private PlayerScript player;
    private itemEffect itemEffectScript;

	void Start () 
    {
        player = PlayerScript.FindPlayerById(playerId);
        itemEffectScript = player.GetComponent<itemEffect>();
	}

    public void activateItem() 
    {
        if (player.itemHeld != 0)
        {
            itemEffectScript.effect(player.itemHeld);
        }
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class RBPlayerPositionScript : MonoBehaviour, IAccelMultiplier
{
    private PlayerScript player;
    
    private float RBPlayerPositionMultiplier;
    private float lastRBPlayerPositionMultiplier;

	void Start () {
        player = GetComponent<PlayerScript>();
	}

	// Update is called once per frame
	void Update () 
    {
        if (player.IsHuman)
            RBPlayerPositionMultiplier = 1;
        else
            RBPlayerPositionMultiplier = RubberBandSettings.RubberBandPlayerPositionMultiplier(
                PlayerScript.getFirstHumanPlayerPosition(), lastRBPlayerPositionMultiplier);

        lastRBPlayerPositionMultiplier = RBPlayerPositionMultiplier;
	}

    public float adjustAccelMultiplier(float multiplier)
    {
        return RBPlayerPositionMultiplier * multiplier;
    }
}

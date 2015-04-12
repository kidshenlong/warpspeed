using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PlayerPositionScript))]
public class RBPositionScript : MonoBehaviour, IAccelMultiplier
{
    private PlayerScript player;
    private PlayerPositionScript playerPosition;

    private float RBPositionMultiplier;
    private float lastRBPositionMultiplier;

    void Start()
    {
        player = GetComponent<PlayerScript>();
        playerPosition = GetComponent<PlayerPositionScript>();
    }
	
	void Update () 
    {
        if (player.IsHuman)
            RBPositionMultiplier = 1;
        else
            RBPositionMultiplier = RubberBandSettings.RubberBandPositionMultiplier(
                playerPosition.raceposition, lastRBPositionMultiplier);

        lastRBPositionMultiplier = RBPositionMultiplier;
	}

    public float adjustAccelMultiplier(float multiplier)
    {
        return RBPositionMultiplier * multiplier;
    }
}

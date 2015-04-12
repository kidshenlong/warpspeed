using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class RBRandomSeedScript : MonoBehaviour, IAccelMultiplier 
{
    private PlayerScript player;

    private float randomSeed;

    void Start() 
    {
        player = GetComponent<PlayerScript>();
        randomSeed = RubberBandSettings.getRandomAISeed();
    }

    public float adjustAccelMultiplier(float multiplier)
    {
        if (player.IsHuman)
            return multiplier;
        else
            return randomSeed * multiplier;
    }
}

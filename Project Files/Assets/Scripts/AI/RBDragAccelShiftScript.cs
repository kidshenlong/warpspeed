using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript), typeof(PlayerPositionScript))]
public class RBDragAccelShiftScript : MonoBehaviour, IAccelMultiplier
{
    private PlayerScript player;
    private PlayerPositionScript position;
    private Rigidbody myRigidbody;

    private float lastDragAccelShift = 1;
    private float multiplier;

	void Start () {
        player = GetComponent<PlayerScript>();
        position = GetComponent<PlayerPositionScript>();
        myRigidbody = rigidbody;
	}
	
	void Update () 
    {
        if (player.IsHuman)
        {
            myRigidbody.drag = 1;
            multiplier = 1;
        }
        else
        { 
            myRigidbody.drag = RubberBandSettings.RubberBandDragAccelShiftMultiplier(position.raceposition, lastDragAccelShift);
            lastDragAccelShift = myRigidbody.drag;
            multiplier = myRigidbody.drag;
        }
	}

    public float adjustAccelMultiplier(float multiplier) 
    {
        return this.multiplier * multiplier;
    }
}

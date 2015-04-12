using UnityEngine;
using System.Collections;

public class UIKeyStateBindingScript : UIButtonKeyBinding
{
    // one event per frame
    private static bool toggle;

    public GameState activeGameState;
	
	void Update () {
        if (toggle && GameStateScript.CurrentGameState == activeGameState)
        {
            sendEvents();
            toggle = false;
        }
	}

    void LateUpdate() 
    {
        toggle = true;
    }
}

using UnityEngine;
using System.Collections;

public class StateRemoveComponentScript : MonoBehaviour {

    public GameState gameStateMask;
    public Component removeComponent;

    void Start() 
    {
        if (((int)GameStateScript.CurrentGameState & (int)gameStateMask) > 0)
        {
            if (removeComponent == null)
                Destroy(this.gameObject);
            else
                Destroy(removeComponent);
        }
        else
        {
            enabled = false;
        }
    }
}

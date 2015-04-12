using UnityEngine;
using System.Collections;

public class UIPauseOptionAnimateScript : MonoBehaviour , IGameStateListener 
{
    public SpringPosition springPosition;
    public SpringPosition springBackPosition;

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.PauseOption:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;
            case GameState.Pause:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }
}

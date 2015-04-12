using UnityEngine;
using System.Collections;

public class UIMainMenuScript : MonoBehaviour, IGameStateListener 
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
            case GameState.Main:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;
            case GameState.MainTrack:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }
}

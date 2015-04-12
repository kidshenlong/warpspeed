using UnityEngine;
using System.Collections;

public class UIMainTrackMenuScript : MonoBehaviour, IGameStateListener
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
            case GameState.MainTrack:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;
            case GameState.Main:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
            case GameState.MainCraft:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }

}

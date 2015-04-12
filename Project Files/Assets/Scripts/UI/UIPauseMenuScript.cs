using UnityEngine;
using System.Collections;

public class UIPauseMenuScript : MonoBehaviour, IGameStateListener
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
            case GameState.Pause:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;
            case GameState.PauseOption:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
            case GameState.Play:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }
}

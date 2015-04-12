using UnityEngine;
using System.Collections;

public class UIMainCraftMenuScript : MonoBehaviour, IGameStateListener
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
            case GameState.MainCraft:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                break;
            case GameState.MainTrack:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
            case GameState.MainFinal:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }
}

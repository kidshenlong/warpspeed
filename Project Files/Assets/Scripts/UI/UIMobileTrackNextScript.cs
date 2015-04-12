using UnityEngine;
using System.Collections;

public class UIMobileTrackNextScript : MonoBehaviour, IGameStateListener
{
    public TweenPosition pos;
    private bool isSelected;

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void show() 
    {
        isSelected = true;
        pos.Play(true);
        
    }

    public void next()
    {
        GameStateScript.instance.trackToCraftSelection();
    }

    public void changeGameState(GameState change)
    {
        switch (change) 
        { 
            case GameState.MainTrack:
                if(isSelected)
                    pos.Play(true);
                break;
            default:
                pos.Play(false);
                break;
        }
    }
}

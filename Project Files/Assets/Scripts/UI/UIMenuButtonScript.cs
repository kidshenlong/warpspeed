using UnityEngine;
using System.Collections;

public class UIMenuButtonScript : MonoBehaviour, IGameStateListener 
{
    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void onClick() 
    {
        GameStateScript.instance.pause();
    }

    public void changeGameState(GameState change) 
    {
        switch(change)
        {
            case GameState.Pause:
                gameObject.SetActive(false);
                break;
            case GameState.Play:
                gameObject.SetActive(true);
                break;
        }
    }
}

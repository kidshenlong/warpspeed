using UnityEngine;
using System.Collections;

public class UIMobileCraftNextScript : MonoBehaviour {

    public TweenPosition pos;
    private bool isSelected;

    public void show()
    {
        isSelected = true;
        pos.Play(true);
    }

    public void next()
    {
        GameStateScript.instance.craftToFinal();
        pos.Play(false);
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.MainCraft:
                if (isSelected)
                    pos.Play(true);
                break;
            default:
                pos.Play(false);
                break;
        }
    }
}

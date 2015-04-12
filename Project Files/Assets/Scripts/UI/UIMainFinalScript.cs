using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class UIMainFinalScript : MonoBehaviour, IGameStateListener 
{
    public SpringPosition springPosition;
    public SpringPosition springBackPosition;

    public UILabel trackLabel;
    public UILabel craftLabel;

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.MainFinal:
                springPosition.enabled = true;
                springBackPosition.enabled = false;
                trackLabel.text = "Track: " + PlayerSelection.selectdedLevelName;
                craftLabel.text = "Craft: " + Regex.Replace(PlayerSelection.selectedCraft.ToString(),"_"," ");
                break;
            case GameState.MainCraft:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                break;
        }
    }
}

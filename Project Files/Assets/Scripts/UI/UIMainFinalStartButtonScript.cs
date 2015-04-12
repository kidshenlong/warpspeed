using UnityEngine;
using System.Collections;

public class UIMainFinalStartButtonScript : MonoBehaviour {

    public void loadLevel() 
    {
        GameStateScript.instance.loadLevel(Application.loadedLevelName);
    }
}

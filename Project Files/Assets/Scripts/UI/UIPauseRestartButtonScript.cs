using UnityEngine;
using System.Collections;

public class UIPauseRestartButtonScript : MonoBehaviour {

	public void restart()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
}

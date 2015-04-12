using UnityEngine;
using System.Collections;

public class UIGameScript : MonoBehaviour {

    void Awake() 
    {
        UIMainScript mainUIScript = (UIMainScript)FindObjectOfType(typeof(UIMainScript));
        if (mainUIScript != null)
        {
            GetComponent<UIPanel>().enabled = false;
        }
    }
}

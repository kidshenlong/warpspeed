using UnityEngine;
using System.Collections;

public class UIMainScript : MonoBehaviour {

    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

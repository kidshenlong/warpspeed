using UnityEngine;
using System.Collections;

public class UIMapChangeScript : MonoBehaviour 
{
    public Track map;
    private UITrackMapScript mapScript;

    void Awake() 
    {
        mapScript = (UITrackMapScript)FindObjectOfType(typeof(UITrackMapScript));
    }

    public void OnHover(bool isOver) 
    {
        if (isOver)
        {
            mapScript.changeTrackMap(map);
        }
    }
}

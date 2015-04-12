using UnityEngine;
using System.Collections;

public class MobileAccelButtonScript : MonoBehaviour
{
    public int playerIndex;
    private PlayerScript player;

#if !(UNITY_IPHONE || UNITY_ANDROID)
    void Awake() 
    {
        Destroy(this.gameObject);
    }
#endif
    
    void Start() 
    {
        player = PlayerScript.FindPlayerById(playerIndex);
    }

    public void startAccelerate()
    {
        player.applyAcceleration();
    }

    public void stopAccelerate()
    {
        player.applyIdleMotion();
    }
}

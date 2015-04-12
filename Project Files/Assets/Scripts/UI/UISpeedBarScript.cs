using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UISprite))]
public class UISpeedBarScript : MonoBehaviour {

    private static readonly float DIVIDER = 100f;

    private UISprite sprite;
    private PlayerScript player;
    public int playerId;

    private Transform myTransform;
    private Vector3 startSize;

    void Awake() 
    {
        sprite = GetComponent<UISprite>();
        myTransform = transform;
        startSize = myTransform.localScale;
    }

    void Start() 
    { 
        player = PlayerScript.FindPlayerById(playerId);
        if (player == null) {
            Debug.LogWarning("player Id="+playerId+" cant be found.");
        }
    }
	
	void Update () 
    {
        if (player != null)
        {
            float velocity = player.getSpeed();
            float speedScale = Mathf.Clamp(velocity / DIVIDER, 0.01f, 1f) * startSize.x;
            myTransform.localScale = new Vector3(
                speedScale,
                startSize.y,
                startSize.z);
        }
	}
}

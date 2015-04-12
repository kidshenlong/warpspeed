using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UISprite))]
public class UIHPBarScript : MonoBehaviour {

    private PlayerScript player;
    private UISprite sprite;
    private TweenScale scale;

    private Transform myTransform;

    private Vector3 startSize;
    private float lastChange;

    public int playerId;

    void Awake() 
    {
        sprite = GetComponent<UISprite>();
        scale = GetComponent<TweenScale>();
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
            float health = player.HP;
            float widthPercent = Mathf.Clamp(health / player.maxHP, -0.01f, 1);

            if (health > 0 && widthPercent > 0.1f) 
                sprite.enabled = true;
            else 
                sprite.enabled = false;

            float width = widthPercent * startSize.x;

            if (lastChange != width)
            {
                lastChange = width;
                scale.from = myTransform.localScale;
                scale.to = new Vector3(width, myTransform.localScale.y, myTransform.localScale.z);
                scale.Reset();
                scale.Play(true);
            }
        }
	}
}

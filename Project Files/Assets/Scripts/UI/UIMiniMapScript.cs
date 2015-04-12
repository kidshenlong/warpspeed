using UnityEngine;
using System.Collections;

public class UIMiniMapScript : MonoBehaviour , IGameStateListener 
{
    private static GameObject playerPointPrefab;

    
    public SpringPosition springPosition;
    public SpringPosition springBackPosition;

    private UISprite sprite;
    private TweenScale tScale;

    public float posX;
    public float posZ;

    public float offsetX;
    public float offsetZ;

    private float miniMapWidth;
    private float miniMapHeight;

    public float mapWidth;
    public float mapHeight;

    public string minimapSpriteName;
    public string mapSpriteName;

    private Transform[] transforms;
    private Transform[] pointTransforms;

    void Awake()
    {
        if (playerPointPrefab == null)
            playerPointPrefab = (GameObject)Resources.Load("UIMiniMapPlayer");

        sprite = GetComponent<UISprite>();
        tScale = GetComponent<TweenScale>();
    }

	void Start () 
    {
        GameStateScript.addStateListener(this);

        transforms = PlayerScript.GetAllPlayerTransforms();
        pointTransforms = new Transform[transforms.Length];
        for (int i = 0; i < transforms.Length; i++) 
        {
            GameObject tmp = (GameObject)Instantiate(playerPointPrefab);

            pointTransforms[i] = tmp.transform;
            Vector3 originalScale = pointTransforms[i].localScale;
            pointTransforms[i].parent = transform.parent;
            pointTransforms[i].localScale = originalScale;

            if (i == 0) 
            {
                UISprite sprite = tmp.GetComponent<UISprite>();
                sprite.color = new Color(1,0.2f,0.2f);
                sprite.depth = 10;
            }
        }

        updatePosition();
	}

    void Update()
    {
        updatePosition();
    }

    void updatePosition() 
    {
        posX = transform.localPosition.x;
        posZ = transform.localPosition.y;

        miniMapWidth = transform.localScale.x;
        miniMapHeight = transform.localScale.y;

        for (int i = 0; i < transforms.Length; i++) 
        {
            Vector3 pos = transforms[i].localPosition;
            float percentX = (pos.x - (offsetX - (mapWidth / 2))) / mapWidth;
            float percentZ = (pos.z - (offsetZ - (mapHeight / 2))) / mapHeight;

            pointTransforms[i].localPosition = new Vector3(
                (posX + (percentX * miniMapWidth)), 
                (posZ - (miniMapHeight * 1.5f) + (percentZ * miniMapHeight)), 
                0);
        }
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.Pause:
                springBackPosition.enabled = false;
                springPosition.enabled = true;
                tScale.Play(true);
                sprite.spriteName = mapSpriteName;
                break;
            case GameState.Play:
                springPosition.enabled = false;
                springBackPosition.enabled = true;
                tScale.Play(false);
                sprite.spriteName = minimapSpriteName;
                break;
        }
    }
}

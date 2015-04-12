using UnityEngine;
using System.Collections;

public class UISwapPadScript : MonoBehaviour, IGameStateListener
{
    private static float INCH_REQUIRED_TURN_DISTANCE = 0.2f;
    private static int FALLBACK_TURN_DISTANCE = 50;

    public UISprite sprite;
    public Transform spriteTransform;

    private UISprite areaSprite;
    private Transform myTransform;
    
    public int playerId;
    private PlayerScript player;

    private int pixelTurnLength;
    private Vector3 pressStartPosition;
    private bool isPressed;
    
    void Awake() 
    {
        myTransform = transform;
        myTransform.localScale = new Vector3(Screen.width, Screen.height);

        spriteTransform = sprite.transform;

        areaSprite = GetComponent<UISprite>();
        areaSprite.enabled = false;

        float dpi = Screen.dpi;
        if (dpi > 0)
        {
            pixelTurnLength = (int)Mathf.Floor(dpi * INCH_REQUIRED_TURN_DISTANCE);
        }
        else 
        {
            pixelTurnLength = FALLBACK_TURN_DISTANCE;
        }
    }

	void Start () 
    {
        GameStateScript.addStateListener(this);
        player = PlayerScript.FindPlayerById(playerId);
	}
	
	void Update () 
    {
        if (isPressed)
        {
            if (pressStartPosition.x + pixelTurnLength < Input.mousePosition.x)
            {
                player.turnRight();
            }
            else if (pressStartPosition.x - pixelTurnLength > Input.mousePosition.x)
            {
                player.turnLeft();
            }
            else
            {
                player.turnCenter();
            }
        }
	}

    public void OnPress(bool isPressed) 
    {
        if (isPressed)
        {
            pressStartPosition = Input.mousePosition;
            player.applyAcceleration();

            sprite.enabled = true;
            spriteTransform.localPosition = Input.mousePosition;
        }
        else
        {
            player.applyIdleMotion();
            player.turnCenter();

            sprite.enabled = false;
        }

        this.isPressed = isPressed;
    }

    public void changeGameState(GameState change)
    {
        switch (change) 
        { 
            case GameState.Play:
                collider.enabled = true;
                break;
            default:
                collider.enabled = false;
                break;
        }
    }
}

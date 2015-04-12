using UnityEngine;
using System.Collections;

public class UIHPOverHeadScript : MonoBehaviour, ICameraUpdateListener, IStatListener
{
    private static readonly float MAX_FADE_OUT_DISTANCE = 60f;
    private static readonly float MIN_FADE_OUT_DISTANCE = 10f;

    private static readonly float MAX_FADE_IN_DISTANCE = 55f;
    private static readonly float MIN_FADE_IN_DISTANCE = 15f;

    private bool isFadedOut;

    public int playerId;
    private PlayerScript player;
    private Transform playerTransform;

    public UISprite hpSprite;
    public UISprite backSprite;

    private TweenAlpha hpAlphaTween;
    private TweenAlpha backAlphaTween;
    
    private Transform hpTransform;
    private Transform backTransform;
    private Transform myTransform;

    private static readonly float BASE_PADDING_WIDTH = 8;

    private float startWidth;
    private float adjustedWidth;

    private float lastHpChangePercent = 1f;

    void Awake() 
    {
        hpAlphaTween = hpSprite.GetComponent<TweenAlpha>();
        backAlphaTween = backSprite.GetComponent<TweenAlpha>();
    }

    void Start() 
    {
        player = PlayerScript.FindPlayerById(playerId);
        playerTransform = player.transform;

        hpTransform = hpSprite.transform;
        backTransform = backSprite.transform;
        myTransform = transform;

        // link up all the listeners
        player.addStatListener(this);
        CarCamera.Instance.addCamUpdateListener(this);
    }

    public void OnCameraUpdatePosition()
    {
        Vector3 above = playerTransform.position + (Vector3.up * 2f);
        myTransform.localPosition = Camera.main.WorldToScreenPoint(above) -
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        if ((isFadedOut && myTransform.localPosition.z > MIN_FADE_OUT_DISTANCE && 
            myTransform.localPosition.z < MAX_FADE_OUT_DISTANCE)
            ||
            (!isFadedOut && myTransform.localPosition.z > MIN_FADE_IN_DISTANCE &&
            myTransform.localPosition.z < MAX_FADE_IN_DISTANCE))
        {
            backTransform.localScale = new Vector3(3000 / myTransform.localPosition.z,
                backTransform.localScale.y, backTransform.localScale.z);

            hpTransform.localPosition = new Vector3(-((backTransform.localScale.x / 2f) - (BASE_PADDING_WIDTH / 2f)), 0, 0);
            hpTransform.localScale = new Vector3(
                Mathf.Clamp(((3000 / myTransform.localPosition.z) - BASE_PADDING_WIDTH) * lastHpChangePercent, 0.01f,2000f),
                hpTransform.localScale.y, hpTransform.localScale.z);

            hpAlphaTween.Play(true);
            backAlphaTween.Play(true);
            isFadedOut = false;
        }
        else
        {
            hpAlphaTween.Play(false);
            backAlphaTween.Play(false);
            isFadedOut = true;
        }
    }

    public void OnHPChange(float change) { }

    public void OnHPChangePercent(float percent)
    {
        if (lastHpChangePercent != percent)
        {
            lastHpChangePercent = percent;
        }
    }
}

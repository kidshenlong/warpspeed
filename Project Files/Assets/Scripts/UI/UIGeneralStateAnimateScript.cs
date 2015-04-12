using UnityEngine;
using System.Collections;

public class UIGeneralStateAnimateScript : MonoBehaviour, IGameStateListener
{
    public GameState activeGameState;

    public bool applyToTweenAlpha = true;
    public bool applyToTweenColor = true;
    public bool applyToTweenPosition = true;
    public bool applyToTweenRotate = true;
    public bool applyToTweenScale = true;

    private TweenAlpha alpha;
    private TweenColor color;
    private TweenPosition position;
    private TweenRotation rotate;
    private TweenScale scale;

    void Awake() 
    {
        alpha = GetComponent<TweenAlpha>();
        color = GetComponent<TweenColor>();
        position = GetComponent<TweenPosition>();
        rotate = GetComponent<TweenRotation>();
        scale = GetComponent<TweenScale>();
    }

    void Start() 
    {
        GameStateScript.addStateListener(this);
    }

    public void changeGameState(GameState change)
    {
        if (change == activeGameState)
        {
            if (applyToTweenAlpha && alpha != null)
                alpha.Play(true);
            if (applyToTweenColor && color != null)
                color.Play(true);
            if (applyToTweenPosition && position != null)
                position.Play(true);
            if (applyToTweenRotate && rotate != null)
                rotate.Play(true);
            if (applyToTweenScale && scale != null)
                scale.Play(true);
        }
        else
        {
            if (applyToTweenAlpha && alpha != null)
                alpha.Play(false);
            if (applyToTweenColor && color != null)
                color.Play(false);
            if (applyToTweenPosition && position != null)
                position.Play(false);
            if (applyToTweenRotate && rotate != null)
                rotate.Play(false);
            if (applyToTweenScale && scale != null)
                scale.Play(false);
        }
    }
}

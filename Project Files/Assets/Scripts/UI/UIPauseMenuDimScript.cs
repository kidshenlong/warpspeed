using UnityEngine;
using System.Collections;

public class UIPauseMenuDimScript : MonoBehaviour, IGameStateListener
{
    private UISprite sprite;
    private TweenColor tColor;

    void Awake() 
    {
        sprite = GetComponent<UISprite>();
        tColor = GetComponent<TweenColor>();
    }

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.Pause:
                gameObject.SetActive(true);
                tColor.Play(true);
                break;
            case GameState.Play:
                tColor.Play(false);
                StartCoroutine(fadeOut());
                break;
        }
    }

    IEnumerator fadeOut()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

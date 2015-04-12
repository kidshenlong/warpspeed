using UnityEngine;
using System.Collections;

public class UICountdownScript : MonoBehaviour
{
    private TweenAlpha alpha;
    private TweenScale scale;

    private UILabel label;
    public float startTime;
    public int countdown;
    public int maxCountdown = 3;
    public int numOfPlayers;
    private PlayerScript player;
    public GameObject sprite;
    public Transform target = null;
	public GameObject playerCamera;

    private string lastTextChange;

    void Awake()
    {
        alpha = GetComponent<TweenAlpha>();
        scale = GetComponent<TweenScale>();
        label = GetComponent<UILabel>();
    }

    void Start()
    {
        // disable all players
        PlayerScript.EnableAllPlayers(false);

        // disable UI time
        UITimeScript.Enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        label.text = "" + Time.timeSinceLevelLoad;

        countdown = maxCountdown - Mathf.FloorToInt(Time.timeSinceLevelLoad);
        if (countdown == 0)
        {
            label.text = "GO";

            // enable all players
            PlayerScript.EnableAllPlayers(true);

            // enable UI time
            UITimeScript.Enabled = true;
        }
        else if (countdown <= 0)
        {
            this.gameObject.SetActive(false);
            sprite.SetActive(false);
			playerCamera.GetComponent<musicPlaylist>().enabled = true;
        }
        else
        {
            label.text = (countdown).ToString();
        }

        if (lastTextChange != label.text)
        {
            lastTextChange = label.text;
            alpha.Reset();
            alpha.Play(true);
            scale.Reset();
            scale.Play(true);
        }
    }
}

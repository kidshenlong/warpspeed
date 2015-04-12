using UnityEngine;
using System.Collections;

public class UITimeScript : MonoBehaviour {

    private static UITimeScript instance;

    private UILabel label;
    private PlayerScript player;
    public UILabel centisecondLabel;
    public int playerId;
    public float time;

    public static bool Enabled
    {
        get { return instance.enabled; }
        set { instance.enabled = value; }
    }

    void Awake()
    {
        instance = this;
        label = GetComponent<UILabel>();
    }
    void Start()
    {
        player = PlayerScript.FindPlayerById(playerId);
        if (player == null)
        {
            Debug.LogWarning("player Id=" + playerId + " cant be found.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            time += Time.deltaTime;
            float min = Mathf.Floor(time / 60);
            float seconds = time % 60;
            label.text = min.ToString("0") + ":" + Mathf.Floor(seconds).ToString("00") + ".";
            centisecondLabel.text = ((seconds % 0.95f) * 100).ToString("00");
        }
    }
}

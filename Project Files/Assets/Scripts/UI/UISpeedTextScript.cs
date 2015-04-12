using UnityEngine;
using System.Collections;

public class UISpeedTextScript : MonoBehaviour {

    private UILabel label;
    private PlayerScript player;
    public int playerId;

    void Awake()
    {
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
            float velocity = player.getSpeed();
            float velocityClamp = ((velocity > HoverScript.HOVER_BALANCE_MAX_SPEED) ? velocity : 0f);
            label.text = metersPerSecToMilesPerHour(velocityClamp).ToString("0") + "mph";
        }
    }

    float metersPerSecToMilesPerHour(float velocity) 
    {
        float metersPerHour = velocity * 3600;
        float milesPerHour = metersPerHour / 1600;
        return milesPerHour;
    }
}

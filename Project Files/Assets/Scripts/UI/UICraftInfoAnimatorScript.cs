using UnityEngine;
using System.Collections;

public class UICraftInfoAnimatorScript : MonoBehaviour {

    private static readonly float BASE_WIDTH = 40;
    private static readonly float STAT_MAX = 10;

    private TweenScale scale;
    private float maxWidth;

    public Stats statToUpdate;

    void Awake()
    {
        maxWidth = transform.localScale.x - BASE_WIDTH;
        Vector3 tmpScale = transform.localScale;
        scale = GetComponent<TweenScale>();
    }

    public void updateBar(Craft craft) 
    {
        scale.from = transform.localScale;
        scale.to = new Vector3(BASE_WIDTH + (maxWidth * (CraftInfo.getStat(craft, statToUpdate) / STAT_MAX)), 
            transform.localScale.y, transform.localScale.z);
        scale.Reset();
        scale.Play(true);
    }
}

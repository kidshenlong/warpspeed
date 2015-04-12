using UnityEngine;
using System.Collections;

public class UICraftUpdateInfoScript : MonoBehaviour {

    public Craft selectedCraft;
    public GameObject sendMessageTo;

    public void OnHover(bool isOver) 
    {
        if (isOver) 
        {
            sendMessageTo.BroadcastMessage("updateBar", selectedCraft, SendMessageOptions.DontRequireReceiver);
        }
    }
}

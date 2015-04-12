using UnityEngine;
using System.Collections;

public class collisionEffect : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            itemEffect itemEffectScript = player.GetComponent<itemEffect>();
            if (player.playerStatus == 2)
            {
                //immune
            }
            else if (player.playerStatus == 1)
            {
                if (itemEffectScript.newPoison == false)
                {
					Debug.Log("POISON TOUCH");
                    player.playerStatus = 0;
                    itemEffectScript.poisonOff();
                    GetComponent<PlayerScript>().playerStatus = 1;
                    GetComponent<itemEffect>().poison();
                }
            }
            else
            {
                if (other.impactForceSum.magnitude >= HPSettings.MinImpactForce)
                {
					
                    player.HP -= Mathf.Round(other.impactForceSum.magnitude * (HPSettings.ImpactDamageMultiplier + (player.weight*0.01f)));
                }
            }
        }
    }
}
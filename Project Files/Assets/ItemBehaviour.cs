using UnityEngine;
using System.Collections;

public class ItemBehaviour : MonoBehaviour
{
    /******************************************************
     * variables
     ******************************************************/
	
	public int[] position1 = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,36,37,56,57,58,59,60,71,72,73,74,75,86,87,88,89};
	public int[] position1low = {1,2,3,4,5,6,7,8,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,56,57,58,59,60,71,72,73,74,75,86,87,88,89};
	public int[] position2 = {1,2,3,4,5,6,7,8,9,10,11,12,13,16,17,18,19,20,21,22,23,24,25,36,37,38,39,51,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93};
	public int[] position2low = {1,2,3,4,5,6,7,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,51,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93};
	
	public int[] position5 = {1,2,3,4,5,6,7,8,16,17,18,19,20,21,22,23,24,25,36,37,38,39,40,41,51,52,53,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93,94,95,96};
	public int[] position5low = {1,2,3,4,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,43,44,45,46,47,51,52,53,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93,94,95,96};
	public int[] position6 = {1,2,3,4,16,17,18,19,20,21,22,23,24,25,36,37,38,39,40,41,42,51,52,53,54,55,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100};
	public int[] position6low = {1,2,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,71,72,73,74,75,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100};
	
    private bool isRegenerating;

    public bool IsRegenerating
    {
        get { return isRegenerating; }
        set
        {
            if (value != isRegenerating)
            {
                renderer.enabled = !renderer.enabled;
                isRegenerating = value;
            }
        }
    }

    /******************************************************
     * MonoBehaviour methods, Update
     ******************************************************/

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 70, 0);
    }

    /******************************************************
     * MonoBehaviour methods, OnTriggerEnter
     ******************************************************/

    void OnTriggerEnter(Collider other)
    {
        PlayerSoundScript soundScript = other.gameObject.GetComponent<PlayerSoundScript>();
        if (soundScript != null)
            soundScript.playGunShotSound();

        if (!IsRegenerating)
        {
            // a collider with the PlayerScript can only collect the item
            PlayerScript player = other.GetComponent<PlayerScript>();
	
            if (player!=null)
            {
                IsRegenerating = true;

                // do something here
				/*if(position==1){
				player.itemHeld = 27;//Random.Range(1,100);
				}
				else if(position==2){
					
				}*/
                float position = other.GetComponent<PlayerPositionScript>().raceposition;
				float HP = other.GetComponent<PlayerScript>().HP;
				if(player.itemHeld == 0 ){
					if(position == 1){
						if(HP<50){
						player.itemHeld = Random.Range(position1low[0],position1low.Length);	
						}else{
						player.itemHeld = Random.Range(position1[0],position1.Length);
						}
					} else if(position == 2){
						if(HP<50){
						player.itemHeld = Random.Range(position2low[0],position2low.Length);
						}else{
						player.itemHeld = Random.Range(position2[0],position2.Length);
						}
					} else if(position == 3 || position == 4){
						player.itemHeld = Random.Range(1,100);
					} else if(position == 5){	
						if(HP<50){
						player.itemHeld = Random.Range(position5low[0],position5low.Length);	
						}else{
						player.itemHeld = Random.Range(position5[0],position5.Length);
						}
					}else{
						if(HP<50){
						player.itemHeld = Random.Range(position6low[0],position6low.Length);	
						}else{
						player.itemHeld = Random.Range(position6[0],position6.Length);
						}
					}
				}
            }
        }
    }
}

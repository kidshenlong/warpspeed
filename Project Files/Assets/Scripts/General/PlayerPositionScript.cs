// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPositionScript : MonoBehaviour
{
    public int currentCheckpoint = 0; //Current checkpoint
    public int currentLap = 0; //Current lap

    public Transform pchecker;
    public Transform current;
    public Transform previous;
    public float distanceto;
    public float distancefrom;
    public float distancebetween;
    public float distancetopercent;
    public float distancefrompercent;
    //public int progress;
    public float baseprogress = 0;
    public float relativeprogress = 0;
    public float currentprogress;
    public int raceposition;
    public GameObject position1;
    public List<Transform> position2;
    public int playerId;
    public int numOfPlayers;

    void Start()
    {
        pchecker = GameObject.Find("positionchecker").transform;
        position1 = GameObject.Find("positionchecker");
        playerId = this.gameObject.GetComponent<PlayerScript>().playerId;
        numOfPlayers = PlayerScript.GetAllPlayerTransforms().Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "checkpoint")
        {
            if (other.transform == pchecker.GetComponent<positionchecker>().checkPointArray[currentCheckpoint].transform)
            {
                if (currentCheckpoint + 1 < pchecker.GetComponent<positionchecker>().checkPointArray.Length)
                {
                    if (currentCheckpoint == 0)
                    {
                        currentLap++;
						//Add best last PlayerPrefs Here...
                    }
                    currentCheckpoint++;
                    baseprogress += 100;
                }
                else
                {
                    currentCheckpoint = 0;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        current = pchecker.GetComponent<positionchecker>().checkPointArray[currentCheckpoint].transform;

        if (currentCheckpoint >= 1)
        {
            previous = pchecker.GetComponent<positionchecker>().checkPointArray[currentCheckpoint - 1].transform;
        }

        else
        {

            int arraylength = pchecker.GetComponent<positionchecker>().checkPointArray.Length;

            previous = pchecker.GetComponent<positionchecker>().checkPointArray[arraylength - 1].transform;
        }
        Vector3 differenceto = (transform.position - current.position);
        Vector3 differencefrom = (previous.position - transform.position);

        Vector3 differencebetween = (previous.position - current.position);

        Debug.DrawLine(current.position, previous.position, Color.red);


        distanceto = differenceto.magnitude;
        distancefrom = differencefrom.magnitude;


        distancebetween = differencebetween.magnitude;

        distancetopercent = Mathf.RoundToInt((Mathf.Min(distanceto / distancebetween * 100, 100)));
        relativeprogress = distancefrompercent = Mathf.RoundToInt((Mathf.Max(1, distancefrom / distancebetween * 100)));

        currentprogress = baseprogress + relativeprogress;


        //myList = myList.GetComponent<positionchecker>()
        //Debug.Log(myList[0]);
        for (int i = 0; i < numOfPlayers; i++)
        {
            position2 = position1.GetComponent<positionchecker>().positions;
            //Debug.Log (position2[2]);
            //GameObject.Find("positionscreen").guiText.text=""+position2[i];
            if (position2[i].transform == gameObject.transform.root)
            {
                //if(this.GetComponentInChildren<TextMesh>().text =)
                //this.GetComponentInChildren<TextMesh>().text = "Position "+i;
                raceposition = (i + 1);
            }
        }

    }


}
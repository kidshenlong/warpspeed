using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class playerSetup : MonoBehaviour
{
    public List<Transform> players = new List<Transform>();
    public List<Transform> startNode = new List<Transform>();

    //private Vector3[] racePositions = { new Vector3(208f, -2.5f, 112f), new Vector3(221f, -2.5f, 96f), new Vector3(234f, -2.5f, 112f), new Vector3(247f, -2.5f, 96f), new Vector3(263f, -2.5f, 112f), new Vector3(279f, -2.5f, 96f) };

    // Use this for initialization
    void Start()
    {
        Vector3[] racePositions = new Vector3[6];
        for (int i = 0; i < 6; i++)
            racePositions[i] = startNode[i].position;

        RandomizeArray(racePositions);

        bool[] craftSelected = new bool[6];


        //players[5].GetComponent<anchorArrayScript>().anchors[0].active = false;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = racePositions[i];

            if (players[i].GetComponent<PlayerScript>().isHuman == true)
            {


                //players[i].GetComponent<PlayerScript>().acceleration = CraftInfo.getSpeed(PlayerSelection.selectedCraft) * 600;
                switch (PlayerSelection.selectedCraft)
                {

                    case Craft.P_Money: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.P_Money].active = true;


                        break;
                    case Craft.Ghetts: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.Ghetts].active = true;


                        break;
                    case Craft.Durrty_Goodz: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.Durrty_Goodz].active = true;


                        break;
                    case Craft.Crazy_Titch: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.Crazy_Titch].active = true;


                        break;
                    case Craft.JME: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.JME].active = true;


                        break;
                    case Craft.D_Double_E: players[i].GetComponent<anchorArrayScript>().anchors[(int)Craft.D_Double_E].active = true;

                        break;

                    //players[i].GetComponent<anchorArrayScript>().anchors[0].active = false;


                }
                craftSelected[(int)PlayerSelection.selectedCraft] = true;
                players[i].GetComponent<itemEffect>().ship = players[i].GetComponent<anchorArrayScript>().anchors[(int)PlayerSelection.selectedCraft].gameObject.GetComponentInChildren<Renderer>();
                players[i].GetComponent<PlayerScript>().maxSpeed = CraftInfo.getSpeed(PlayerSelection.selectedCraft) / 10 * 20 + 60;
                players[i].GetComponent<PlayerScript>().acceleration = players[i].GetComponent<PlayerScript>().acceleration - 400 / CraftInfo.getSpeed(PlayerSelection.selectedCraft);
                players[i].GetComponent<PlayerScript>().rotationSpeed = CraftInfo.getFlow(PlayerSelection.selectedCraft) / 10 * 20 + 90;
                players[i].GetComponent<PlayerScript>().maxHP = CraftInfo.getLyricism(PlayerSelection.selectedCraft) / 10 * 20 + 90;
                players[i].GetComponent<PlayerScript>().HP = players[i].GetComponent<PlayerScript>().maxHP;
                players[i].GetComponent<PlayerScript>().weight = CraftInfo.getRep(PlayerSelection.selectedCraft);
                players[i].GetComponent<PlayerScript>().chosenPlayer = "" + PlayerSelection.selectedCraft;
                players[i].GetComponent<PlayerScript>().chosenPlayer = players[i].GetComponent<PlayerScript>().chosenPlayer.Replace("_", " ");

                players[i].GetComponent<PlayerSoundScript>().addCrashSound(PlayerSelection.selectedCraft);
            }

            else
            {

                for (int x = 0; x < 6; x++)
                {

                    if (!craftSelected[x])
                    {


                        /*PlayerScript.*/
                        players[i].GetComponent<anchorArrayScript>().anchors[x].active = true;
                        players[i].GetComponent<itemEffect>().ship = players[i].GetComponent<anchorArrayScript>().anchors[x].gameObject.GetComponentInChildren<Renderer>();
                        players[i].GetComponent<PlayerScript>().maxSpeed = CraftInfo.getSpeed(PlayerSelection.getCraftNameById(x)) / 10 * 20 + 60;
                        players[i].GetComponent<PlayerScript>().acceleration = players[i].GetComponent<PlayerScript>().acceleration - 400 / CraftInfo.getSpeed(PlayerSelection.getCraftNameById(x));
                        players[i].GetComponent<PlayerScript>().rotationSpeed = CraftInfo.getFlow(PlayerSelection.getCraftNameById(x)) / 10 * 20 + 90;
                        players[i].GetComponent<PlayerScript>().rotationSpeed = CraftInfo.getLyricism(PlayerSelection.getCraftNameById(x)) / 10 * 20 + 90;
                        players[i].GetComponent<PlayerScript>().maxHP = CraftInfo.getLyricism(PlayerSelection.getCraftNameById(x)) / 10 * 20 + 90;
                        players[i].GetComponent<PlayerScript>().HP = players[i].GetComponent<PlayerScript>().maxHP;
                        players[i].GetComponent<PlayerScript>().weight = CraftInfo.getRep(PlayerSelection.getCraftNameById(x));
                        players[i].GetComponent<PlayerScript>().chosenPlayer = "" + PlayerSelection.getCraftNameById(x);
                        players[i].GetComponent<PlayerScript>().chosenPlayer = players[i].GetComponent<PlayerScript>().chosenPlayer.Replace("_", " ");

                        players[i].GetComponent<PlayerSoundScript>().addCrashSound(PlayerSelection.getCraftNameById(x));
                        craftSelected[x] = true;
                        break;

                    }

                }




                players[i].GetComponent<PathFollowScript>().repathPoints();

            }

            players[i].GetComponent<itemEffect>().initialMat = players[i].GetComponent<itemEffect>().ship.material;
            
        }

    }

    void RandomizeArray(Vector3[] Array)
    {
        for (int i = startNode.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            Vector3 tmp = Array[i];
            Array[i] = Array[r];
            Array[r] = tmp;
        }
    }
}

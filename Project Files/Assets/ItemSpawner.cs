using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour, IWeight
{
    /******************************************************
     * variables
     ******************************************************/

    private static readonly float NO_ITEM_WEIGHT_DIVIDER = 5f;

    private static GameObject itemPrefab;

    public float itemSpawnTime = 10f;
    ItemBehaviour itemBehavior;

    public float timeCounter = 0;

    /******************************************************
     * MonoBehaviour methods, Awake
     ******************************************************/

    void Awake()
    {
        // load perfab
        if (itemPrefab == null)
        {
            itemPrefab = (GameObject)Resources.Load("itemPrefab");
        }

        GameObject tmp = (GameObject)Instantiate(itemPrefab, transform.position, Quaternion.identity);
        itemBehavior = tmp.GetComponent<ItemBehaviour>();
    }

    /******************************************************
     * MonoBehaviour methods, Update
     ******************************************************/

    // Update is called once per frame
    void Update()
    {
        if (itemBehavior.IsRegenerating)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= itemSpawnTime)
            {
                timeCounter = 0;
                itemBehavior.IsRegenerating = false;
            }
        }

    }

    /******************************************************
     * IWeight methods, adjustWeight
     ******************************************************/

    public float adjustWeight(float weight)
    {
        if (itemBehavior.IsRegenerating)
            return weight / NO_ITEM_WEIGHT_DIVIDER;
        else
            return weight;
    }
}


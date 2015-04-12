using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarCamera : MonoBehaviour
{
    private static CarCamera instance;
    public static CarCamera Instance
    {
        get { return CarCamera.instance; }
    }


    private PlayerScript player;
    public int targetPlayerId;

    public Transform target;
    public float height = 1f;
    public float positionDamping = 3f;
    public float velocityDamping = 3f;
    public float distance = 4f;
    public LayerMask ignoreLayers = -1;

    private RaycastHit hit = new RaycastHit();

    private Vector3 prevVelocity = Vector3.zero;
    private LayerMask raycastLayers = -1;

    private Vector3 currentVelocity = Vector3.zero;

    public bool isRespawning;

    public List<Transform> playerList = new List<Transform>();

    public List<ICameraUpdateListener> camUpdateListener;

    void Awake() 
    {
        instance = this;
        camUpdateListener = new List<ICameraUpdateListener>();
    }

    void Start()
    {
        raycastLayers = ~ignoreLayers;

        //target = PlayerScript.FindPlayerById(targetPlayerId).transform;
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerList.Add(i.transform);
        }

    }

    void FixedUpdate()
    {

        //target = PlayerScript.FindPlayerById(targetPlayerId).transform;
        //target.GetComponent<PlayerScript>().isFinished;

        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].transform.GetComponent<PlayerScript>().playerId == targetPlayerId)
            {
                target = playerList[i].transform;
            }
        }

        if (isRespawning == false)
        {
            currentVelocity = Vector3.Lerp(prevVelocity, target.root.rigidbody.velocity, velocityDamping * Time.deltaTime);
            currentVelocity.y = 0;
            prevVelocity = currentVelocity;
        }
    }

    void LateUpdate()
    {
        if (isRespawning == false)
        {
            float speedFactor = Mathf.Clamp01(target.root.rigidbody.velocity.magnitude / 70.0f);
            camera.fieldOfView = Mathf.Lerp(55, 72, speedFactor);
            float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

            currentVelocity = currentVelocity.normalized;

            Vector3 newTargetPosition = target.position + Vector3.up * height;
            Vector3 newPosition;
            if (target.GetComponent<PlayerScript>().isFinished == true)
            {
                newPosition = newTargetPosition - (currentVelocity * -currentDistance);
            }
            else
            {
                newPosition = newTargetPosition - (currentVelocity * currentDistance);
            }
            newPosition.y = newTargetPosition.y;

            Vector3 targetDirection = newPosition - newTargetPosition;
            if (Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
                newPosition = hit.point;

            transform.position = newPosition - (transform.forward * distance);
            transform.LookAt(newTargetPosition);

        }
        else
        {
            transform.LookAt(target.position + Vector3.up * height);
        }

        // call back all ICameraUpdateListener
        for (int i = 0; i < camUpdateListener.Count; i++)
            camUpdateListener[i].OnCameraUpdatePosition();
    }

    /******************************************************
     * add/remove ICameraUpdateListener
     ******************************************************/
    /// <summary>
    /// listener should only be added after "Awake" method
    /// </summary>
    public void addCamUpdateListener(ICameraUpdateListener listener)
    {
        if (!camUpdateListener.Contains(listener))
        {
            camUpdateListener.Add(listener);
        }
    }

    public void removeCamUpdateListener(ICameraUpdateListener listener)
    {
        if (!camUpdateListener.Contains(listener))
        {
            camUpdateListener.Remove(listener);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody), typeof(PlayerPositionScript))]
public class PlayerScript : MonoBehaviour
{
    /******************************************************
     * variables
     ******************************************************/

    private static readonly float REVERSE_FRACTION = 2f;
    public static PlayerScript[] players;

    public int playerId;
    public string chosenPlayer;

    public float maxHP = 100;

    [SerializeField]
    private float hp = 100;
    public float HP
    {
        get { return hp; }
        set
        {
            bool change = hp != value;
            hp = Mathf.Clamp(value, 0, maxHP);
            if (change)
            {
                for (int i = 0; i < statListeners.Count; i++)
                {
                    statListeners[i].OnHPChange(hp);
                    statListeners[i].OnHPChangePercent(hp / maxHP);
                }
            }
        }
    }

    public float maxSpeed = 70;
    public float acceleration = 4000;
    public float rotationSpeed = 100;
    public bool speedCap = true;

    public float weight = 5;

    public int currentLap;
    public float time;
    public string timeformat;

    public GameObject UIObject;

    public int playerStatus = 0;

    public int itemHeld;

    public bool isFinished = false;

    private PlayerPositionScript playerPosition;
    private PlayerControls controls;
    private PathFollowScript follower;

    private Transform myTransform;
    private Rigidbody myRigidbody;

    private IAccelMultiplier[] accelMultipliers;
    private List<IPlayerMotionListener> motionListeners;
    private List<IStatListener> statListeners;

    private PlayerMotion currentMotion = PlayerMotion.Idle;
    private PlayerTurnMotion currentTurnMotion = PlayerTurnMotion.Center;
    private float turningFraction;

    public UILabel label;

    public GameObject mainUIRoot;
    public GameObject finalUIRoot;

    [SerializeField]
    public bool isHuman;
    public bool IsHuman
    {
        get { return isHuman; }
        set
        {
            isHuman = value;

            // controls
            if (isHuman)
            {
                controls.enabled = true;
                follower.enabled = false;
            }
            else
            {
                controls.enabled = false;
                follower.enabled = true;
            }
        }
    }

    /******************************************************
     * MonoBehaviour methods, Awake
     ******************************************************/

    void Awake()
    {

        if (players == null)
            players = new PlayerScript[8];

        players[playerId] = this;

        this.myTransform = transform;
        this.myRigidbody = rigidbody;

        playerPosition = GetComponent<PlayerPositionScript>();
        controls = GetComponent<PlayerControls>();
        follower = GetComponent<PathFollowScript>();


        UIMainScript mainUIScript = (UIMainScript)FindObjectOfType(typeof(UIMainScript));
        if (mainUIScript != null)
        {
            follower.isMoving = true;
            IsHuman = false;
        }

        // register all the multiplier components within this gameobject
        Component[] multiplierComponents = GetComponentsInChildren(typeof(IAccelMultiplier));
        accelMultipliers = new IAccelMultiplier[multiplierComponents.Length];
        for (int i = 0; i < multiplierComponents.Length; i++)
            accelMultipliers[i] = (IAccelMultiplier)multiplierComponents[i];

        motionListeners = new List<IPlayerMotionListener>();
        statListeners = new List<IStatListener>();

        IsHuman = isHuman;
    }

    /******************************************************
     * MonoBehaviour methods, Start
     ******************************************************/

    void Start()
    {
        label = GameObject.Find("Label - Finish").GetComponent<UILabel>();

    }

    /******************************************************
     * MonoBehaviour methods, Update
     ******************************************************/

    void Update()
    {
        currentLap = playerPosition.currentLap;
        
        time += Time.deltaTime;

        if (currentLap > 3)
        {
            if (!isFinished)
            {
                int finishposition = playerPosition.raceposition;
                float finishTime = time;
                //PlayerPrefs.SetFloat("Player " + playerId + " Time", time);
                //PlayerPrefs.SetInt("Current Race Position", finishposition);

                float min = Mathf.Floor(time / 60);
                label.text += "Position: " + finishposition + ": " + chosenPlayer + " Finish Time: " + (timeformat = min.ToString("0") + ":" + (time % 60).ToString("00.00")) + "\n";
                //Debug.Log("Player "+(playerId+1)+" Finished at: "+timeformat+" In Position "+finishposition);
                //PlayerPrefs.Save();

                isFinished = true;

                if (IsHuman)
                {
                    GameStateScript.instance.finished();

                    acceleration = 2000;
                    follower.enabled = true;

                    UIContainer container = mainUIRoot.GetComponentInChildren<UIContainer>();
                    if (container != null)
                        container.gameObject.SetActive(false);
                }
                else
                {
                    label.text += chosenPlayer + " Finish Time: DID NOT FINISH" + "\n";
                    acceleration = 2000;
                }
            }
        }

        else if (hp <= 0)
        {
            if (IsHuman)
            {
                GameStateScript.instance.gameOver();

                UIContainer container = mainUIRoot.GetComponentInChildren<UIContainer>();
                if (container != null)
                    container.gameObject.SetActive(false);

                controls.isMoving = false;
            }
            else
            {
                follower.isMoving = false;
            }
        }
    }

    /******************************************************
     * MonoBehaviour methods, FixedUpdate
     ******************************************************/

    void FixedUpdate()
    {



        // accelerate motion
        if (currentMotion == PlayerMotion.Accel)
        {
            float iAccelMultiplier = 1;
            for (int i = 0; i < accelMultipliers.Length; i++)
                iAccelMultiplier = accelMultipliers[i].adjustAccelMultiplier(iAccelMultiplier);

            float multiplier = acceleration *
                                ((hp / maxHP) * HPSettings.MaxSpeedReduction + (1 - HPSettings.MaxSpeedReduction)) *
                                rigidbody.mass *
                                Time.deltaTime *
                                iAccelMultiplier;

            myRigidbody.AddForce(myTransform.forward * multiplier);
        }
        // reverse motion
        else if (currentMotion == PlayerMotion.Reverse)
        {
            float multiplier = -acceleration *
                            ((hp / maxHP) * HPSettings.MaxSpeedReduction + (1 - HPSettings.MaxSpeedReduction)) *
                            rigidbody.mass *
                            Time.deltaTime
                            / REVERSE_FRACTION;

            myRigidbody.AddForce(myTransform.forward * multiplier);
        }

        // turn left
        if (currentTurnMotion == PlayerTurnMotion.Left)
        {
            Vector3 currentR = myTransform.eulerAngles;
            myTransform.eulerAngles -= Vector3.up * rotationSpeed * Time.deltaTime * Mathf.Clamp01(turningFraction);
        }
        // turn right
        else if (currentTurnMotion == PlayerTurnMotion.Right)
        {
            Vector3 currentR = myTransform.eulerAngles;
            myTransform.eulerAngles += Vector3.up * rotationSpeed * Time.deltaTime * Mathf.Clamp01(turningFraction);
        }

        if (speedCap == true)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
    }




    /******************************************************
     * FindPlayerById
     ******************************************************/

    public static PlayerScript FindPlayerById(int id)
    {
        return players[id];
    }

    /******************************************************
     * GetAllPlayerTransforms
     ******************************************************/

    public static Transform[] GetAllPlayerTransforms()
    {
        List<Transform> allTransforms = new List<Transform>();

        for (int i = 0; i < players.Length; i++)
            if (players[i] != null)
                allTransforms.Add(players[i].transform);

        return allTransforms.ToArray();
    }

    /******************************************************
     * public method for getting speed of the attach gameobject
     ******************************************************/

    public float getSpeed()
    {
        return myRigidbody.velocity.magnitude;
    }

    /******************************************************
     * public method for enabling acceleration
     ******************************************************/

    public void applyAcceleration()
    {
        if (!enabled) return;

        if (currentMotion == PlayerMotion.Accel) return;

        for (int i = 0; i < motionListeners.Count; i++)
            motionListeners[i].OnAccelrate();

        currentMotion = PlayerMotion.Accel;
    }

    /******************************************************
     * public method for disabling acceleration/reverse
     ******************************************************/

    public void applyIdleMotion()
    {
        if (currentMotion == PlayerMotion.Idle) return;

        for (int i = 0; i < motionListeners.Count; i++)
            motionListeners[i].OnIdle();

        currentMotion = PlayerMotion.Idle;
    }

    /******************************************************
     * public method for enabling reverse motion
     ******************************************************/

    public void applyReverse()
    {
        if (currentMotion == PlayerMotion.Reverse) return;

        for (int i = 0; i < motionListeners.Count; i++)
            motionListeners[i].OnReverse();

        currentMotion = PlayerMotion.Reverse;
    }

    /******************************************************
     * public method for turning left
     ******************************************************/

    public void turnLeft()
    {
        turnLeft(1);
    }
    public void turnLeft(float fraction)
    {
        if (!enabled) return;

        turningFraction = fraction;

        if (currentTurnMotion == PlayerTurnMotion.Left)
        {
            for (int i = 0; i < motionListeners.Count; i++)
                motionListeners[i].OnUpdateTurnLeft();
        }
        else
        {
            for (int i = 0; i < motionListeners.Count; i++)
                motionListeners[i].OnTurnLeft();

            currentTurnMotion = PlayerTurnMotion.Left;
        }
    }

    /******************************************************
     * public method for centering
     ******************************************************/

    public void turnCenter()
    {
        if (currentTurnMotion == PlayerTurnMotion.Center) return;

        for (int i = 0; i < motionListeners.Count; i++)
            motionListeners[i].OnCenter();

        currentTurnMotion = PlayerTurnMotion.Center;
    }

    /******************************************************
     * public method for turning right
     ******************************************************/

    public void turnRight()
    {
        turnRight(1);
    }
    public void turnRight(float fraction)
    {
        if (!enabled) return;

        turningFraction = fraction;

        if (currentTurnMotion == PlayerTurnMotion.Right)
        {
            for (int i = 0; i < motionListeners.Count; i++)
                motionListeners[i].OnUpdateTurnRight();
        }
        else
        {
            for (int i = 0; i < motionListeners.Count; i++)
                motionListeners[i].OnTurnRight();

            currentTurnMotion = PlayerTurnMotion.Right;
        }
    }

    /******************************************************
     * getFirstHumanPlayerPosition
     ******************************************************/

    public static int getFirstHumanPlayerPosition()
    {
        int pos = players.Length;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null &&
                players[i].isHuman &&
                players[i].playerPosition.raceposition < pos)
            {
                pos = players[i].playerPosition.raceposition;
            }
        }
        return pos;
    }

    /******************************************************
     * enable all players
     ******************************************************/

    public static void EnableAllPlayers(bool enable)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                players[i].enabled = enable;
            }
        }
    }

    /******************************************************
     * add/remove statListener
     ******************************************************/
    /// <summary>
    /// listener should only be added after "Awake" method
    /// </summary>
    public void addStatListener(IStatListener listener)
    {
        if (!statListeners.Contains(listener))
        {
            statListeners.Add(listener);
        }
    }

    public void removeStatListener(IStatListener listener)
    {
        if (!statListeners.Contains(listener))
        {
            statListeners.Remove(listener);
        }
    }

    /******************************************************
     * add/remove playerMotionListener
     ******************************************************/
    /// <summary>
    /// listener should only be added after "Awake" method
    /// </summary>
    public void addMotionListener(IPlayerMotionListener listener)
    {
        if (!motionListeners.Contains(listener))
        {
            motionListeners.Add(listener);
        }
    }

    public void removeMotionListener(IPlayerMotionListener listener)
    {
        if (!motionListeners.Contains(listener))
        {
            motionListeners.Remove(listener);
        }
    }
}

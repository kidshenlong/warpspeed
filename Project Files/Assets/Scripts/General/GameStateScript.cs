using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateScript : MonoBehaviour {
    public static GameStateScript instance;

    public GameState startGameState;
    private GameState gameState;
    public static GameState CurrentGameState
    {
        get { return instance.gameState; }
        set 
        {
            if (instance.gameState != value)
            {
                instance.gameState = value;
                for (int i = 0; i < instance.gameStateListeners.Count; i++)
                    instance.gameStateListeners[i].changeGameState(value);
            }
        }
    }

    private List<IGameStateListener> gameStateListeners;

	void Awake () 
    {
        DontDestroyOnLoad(this.gameObject);
        Object[] gameStates = FindObjectsOfType(typeof(GameStateScript));
        if (gameStates.Length == 1)
        {
            instance = this;
            gameState = startGameState;
        }
        else
        {
            Destroy(this.gameObject);
        }

        gameStateListeners = new List<IGameStateListener>();
	}

    void Start() 
    {
        /*
        GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        List<IGameStateListener> list = new List<IGameStateListener>();

        foreach(GameObject go in allGameObjects)
        {
            Component[] tmp = go.GetComponents(typeof(IGameStateListener));
            for (int i = 0; i < tmp.Length; i++)
                list.Add((IGameStateListener)tmp[i]);
        }

        listeners = list.ToArray();
         */
    }

    void gameToMainMenu() 
    {
        if (gameState == GameState.Pause)
        {
            CurrentGameState = GameState.Main;
            Time.timeScale = 1;
            Destroy(this.gameObject);
            Application.LoadLevel("main");
        }
    }

    public void loadLevel(string levelName) 
    {
        Destroy(this.gameObject);
        UIMainScript main = (UIMainScript)FindObjectOfType(typeof(UIMainScript));
        if (main != null) {
            Destroy(main.gameObject);
        }
        Application.LoadLevel(levelName);
    }

    public void restartLevel()
    {
        Destroy(this.gameObject);
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void pause() 
    {
        if (gameState == GameState.Play) 
        {
            CurrentGameState = GameState.Pause;
            Time.timeScale = 0;
        }
    }

    public void pauseOptionBack() 
    {
        if (gameState == GameState.PauseOption)
        {
            CurrentGameState = GameState.Pause;
        }
    }

    public void pauseOption()
    {
        if (gameState == GameState.Pause) 
        {
            CurrentGameState = GameState.PauseOption;
        }
    }

    public void resume() 
    {
        if (gameState == GameState.Pause)
        {
            CurrentGameState = GameState.Play;
            Time.timeScale = 1;
        }
    }

    public void mainToTrackMenu() 
    {
        if (gameState == GameState.Main)
        {
            CurrentGameState = GameState.MainTrack;
        }
    }

    public void trackToMainMenu()
    {
        if (gameState == GameState.MainTrack)
        {
            CurrentGameState = GameState.Main;
        }
    }

    public void trackToCraftSelection() 
    {
        if (gameState == GameState.MainTrack)
        {
            CurrentGameState = GameState.MainCraft;
        }
    }

    public void craftToTrackSelection()
    {
        if (gameState == GameState.MainCraft)
        {
            CurrentGameState = GameState.MainTrack;
        }
    }

    public void craftToFinal()
    {
        if (gameState == GameState.MainCraft)
        {
            CurrentGameState = GameState.MainFinal;
        }
    }

    public void finalToCraftSelection()
    {
        if (gameState == GameState.MainFinal)
        {
            CurrentGameState = GameState.MainCraft;
        }
    }
	
	public void finished()
    {
        if (gameState == GameState.Play)
        {
			Debug.Log("Finish");
            CurrentGameState = GameState.Finished;
        }
		
    }
	
		public void gameOver()
    {
        if (gameState == GameState.Play)
       {
		    Debug.Log("GameOver");
            CurrentGameState = GameState.GameOver;
        }
    }
	
    void gameOverToMainMenu() 
    {
        if (gameState == GameState.GameOver)
        {
            CurrentGameState = GameState.Main;
            Time.timeScale = 1;
            Destroy(this.gameObject);
            Application.LoadLevel("main");
        }
    }
	
	void finishToMainMenu() 
    {
        if (gameState == GameState.Finished)
        {
            CurrentGameState = GameState.Main;
            Time.timeScale = 1;
            Destroy(this.gameObject);
            Application.LoadLevel("main");
        }
    }

    /******************************************************
     * add/remove gameStateListener
     ******************************************************/
    /// <summary>
    /// listener should only be added after "Awake" method
    /// </summary>
    public static void addStateListener(IGameStateListener listener)
    {
        if (!instance.gameStateListeners.Contains(listener))
        {
            instance.gameStateListeners.Add(listener);
        }
    }

    public static void removeStateListener(IGameStateListener listener)
    {
        if (!instance.gameStateListeners.Contains(listener))
        {
            instance.gameStateListeners.Remove(listener);
        }
    }
}

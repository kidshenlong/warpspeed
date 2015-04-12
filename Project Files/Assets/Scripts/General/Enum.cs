public enum PlayerMotion { Idle, Accel, Reverse };
public enum PlayerTurnMotion { Left, Center, Right };
public enum GameState 
{ 
    Main = 1, 
    MainTrack = 1 << 1,
    MainOption = 1 << 2,
    MainCraft = 1 << 3,
    MainFinal = 1 << 4,
    Play = 1 << 5,
    Pause = 1 << 6,
    PauseOption = 1 << 7,
	Finished = 1 << 8,
	GameOver = 1 << 9
};
public enum Track
{ 
    None = 1,
    Track1 = 1 << 1,
    Track2 = 1 << 2,
    Track3 = 1 << 3
}
public enum Craft 
{ 
    P_Money = 0, 
    Ghetts = 1, 
    Durrty_Goodz = 2, 
    Crazy_Titch = 3, 
    JME = 4, 
    D_Double_E = 5
};
public enum Stats
{ 
    Speed,
    Flow,
    Lyricism,
    Rep
};
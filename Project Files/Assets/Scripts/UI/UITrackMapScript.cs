using UnityEngine;
using System.Collections;

public class UITrackMapScript : MonoBehaviour, IGameStateListener
{
    public TweenScale tweenScale;

    public TweenAlpha track1;
    public TweenAlpha track2;
    public TweenAlpha track3;

    private Track startTrack = Track.Track1;
    private Track selectedTrack;

    void Awake()
    {
        selectedTrack = startTrack;
    }

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    public void changeTrackMap(Track track)
    {
        switch (track)
        {
            case Track.Track1:
                track1.Play(true);
                track2.Play(false);
                track3.Play(false);
                selectedTrack = Track.Track1;
                break;
            case Track.Track2:
                track1.Play(false);
                track2.Play(true);
                track3.Play(false);
                selectedTrack = Track.Track2;
                break;
            case Track.Track3:
                track1.Play(false);
                track2.Play(false);
                track3.Play(true);
                selectedTrack = Track.Track3;
                break;
            case Track.None:
                track1.Play(false);
                track2.Play(false);
                track3.Play(false);
                selectedTrack = 0;
            break;
        }
    }

    public void changeGameState(GameState change)
    {
        switch (change)
        {
            case GameState.Main:
                tweenScale.Play(false);
                changeTrackMap(Track.None);
                break;
            case GameState.MainTrack:
                tweenScale.Play(true);
                changeTrackMap(selectedTrack);
                break;
            case GameState.MainCraft:
                tweenScale.Play(false);
                changeTrackMap(Track.None);
                break;
        }
    }
}

using UnityEngine;
using System.Collections;

public class UITrackSelectionScript : MonoBehaviour 
{
    public string loadTrackName;
    private AsyncOperation async;

    public TweenAlpha loadLabel;
    public TweenAlpha fade;

	void Update () {
        
	}

    public void loadTrack() 
    {
        if(loadTrackName != Application.loadedLevelName)
            StartCoroutine("loadTrackCoroutine");
        else
            GameStateScript.instance.trackToCraftSelection();
    }

    private IEnumerator loadTrackCoroutine() 
    {
        PlayerSelection.selectdedLevelName = loadTrackName;
        loadLabel.Play(true);
        fade.Play(true);
        yield return new WaitForSeconds(0.5f);
        async = Application.LoadLevelAsync(loadTrackName);
        yield return async;
        yield return new WaitForSeconds(0.5f);
        loadLabel.Play(false);
        fade.Play(false);
#if !UNITY_ANDROID && !UNITY_IPHONE
        GameStateScript.instance.trackToCraftSelection();
#else
        UICamera.selectedObject = this.gameObject;
        UIMobileTrackNextScript script = FindObjectOfType(typeof(UIMobileTrackNextScript)) as UIMobileTrackNextScript;
        script.show();
#endif

    }
}

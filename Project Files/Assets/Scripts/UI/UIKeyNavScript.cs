using UnityEngine;
using System.Collections;

public class UIKeyNavScript : MonoBehaviour , IGameStateListener
{
    public GameState ActiveGameState;

    public UIButton[] button;
    
    private int currentHighlightIndex;

    private GameObject lastSelectedGO;

    void Awake() 
    {
        if(button==null && button.Length > 0)Destroy(this);

#if !UNITY_ANDROID && !UNITY_IPHONE
        UICamera.selectedObject = button[0].gameObject;
        lastSelectedGO = UICamera.selectedObject;
#endif
    }

    void Start()
    {
        GameStateScript.addStateListener(this);
    }

    void Update()
    {
        if (GameStateScript.CurrentGameState != ActiveGameState) return;

        if (UICamera.hoveredObject != null &&
            UICamera.selectedObject != UICamera.hoveredObject)
        {
            UICamera.selectedObject = null;
        }
        if (UICamera.hoveredObject == null)
        {
            UICamera.selectedObject = lastSelectedGO;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentHighlightIndex - 1 >= 0) currentHighlightIndex--;
            UICamera.selectedObject = button[currentHighlightIndex].gameObject;
            lastSelectedGO = UICamera.selectedObject;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentHighlightIndex + 1 < button.Length) currentHighlightIndex++;
                UICamera.selectedObject = button[currentHighlightIndex].gameObject;

            lastSelectedGO = UICamera.selectedObject;
        }
    }

    public void changeGameState(GameState change)
    {
        if (UICamera.selectedObject == lastSelectedGO)
            UICamera.selectedObject = null;
    }
}

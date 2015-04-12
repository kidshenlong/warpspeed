using UnityEngine;
using System.Collections;

public class UICraftSelectionScript : MonoBehaviour 
{
    public void selectCraft1() 
    {
        switchPath(Craft.P_Money);
    }

    public void selectCraft2()
    {
        switchPath(Craft.Ghetts);
    }

    public void selectCraft3()
    {
        switchPath(Craft.Durrty_Goodz);
    }

    public void selectCraft4()
    {
        switchPath(Craft.Crazy_Titch);
    }

    public void selectCraft5()
    {
        switchPath(Craft.JME);
    }

    public void selectCraft6()
    {
        switchPath(Craft.D_Double_E);
    }

    private void switchPath(Craft craft) 
    {
        PlayerSelection.selectedCraft = craft;
#if !UNITY_ANDROID && !UNITY_IPHONE
        GameStateScript.instance.craftToFinal();
#else
        UIMobileCraftNextScript script = FindObjectOfType(typeof(UIMobileCraftNextScript)) as UIMobileCraftNextScript;
        script.show();
#endif
    }
}

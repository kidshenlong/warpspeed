using UnityEngine;
using System.Collections;

public class PlayerSelection
{
    public static string selectdedLevelName;
    public static Craft selectedCraft = Craft.P_Money;

    public static Craft getCraftNameById(int id) 
    {
        switch (id)
        {
            case 0: return Craft.P_Money;
            case 1: return Craft.Ghetts;
            case 2: return Craft.Durrty_Goodz;
            case 3: return Craft.Crazy_Titch;
            case 4: return Craft.JME;
            case 5: return Craft.D_Double_E;
        }

        return Craft.P_Money;
    }
}

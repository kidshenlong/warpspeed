using UnityEngine;
using System.Collections;

public class CraftInfo
{
    private static readonly float[] speed = { 6, 7, 9, 7, 6, 7 };
    private static readonly float[] flow = { 7, 7, 8, 6, 7, 8 };
    private static readonly float[] lyricism = { 7, 7, 7, 5, 7, 5 };
    private static readonly float[] rep = { 8, 7, 4, 10, 8, 8 };

    public static float getStat(Craft craft, Stats stat) 
    {
        switch (stat) { 
            case Stats.Speed:
                return getSpeed(craft);
            case Stats.Flow:
                return getFlow(craft);
            case Stats.Lyricism:
                return getLyricism(craft);
            case Stats.Rep:
                return getRep(craft);
        }

        // should never reach here
        return 0;
    }

    public static float getSpeed(Craft craft)
    {
        return speed[(int)craft];
    }

    public static float getFlow(Craft craft)
    {
        return flow[(int)craft];
    }

    public static float getLyricism(Craft craft)
    {
        return lyricism[(int)craft];
    }

    public static float getRep(Craft craft)
    {
        return rep[(int)craft];
    }
}

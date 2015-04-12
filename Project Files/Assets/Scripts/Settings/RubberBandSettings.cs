#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;
using UnityEngine;

public class RubberBandSettings : ScriptableObject
{
    private static RubberBandSettings rubberBandSettings;

    public bool enablePlayerPositionRB = true;
    public float[] rubberBandPlayerPositionMultipliers;

    public bool enablePositionRB = true;
    public float[] rubberBandPositionMultipliers;

    public bool enableDragAccelShiftRB = true;
    public float[] rubberBandDragAccelShift;

    public float randomAISeedMinLimit = 1;
    public float randomAISeedMaxLimit = 1;
    public float transactionTime = 4f;

#if UNITY_EDITOR
    [MenuItem("WarpSpeed Settings/Rubber Band")]
    static void Init()
    {
        rubberBandSettings = (RubberBandSettings)AssetDatabase.LoadAssetAtPath(@"Assets\Resources\RubberBandSettings.asset", typeof(RubberBandSettings));

        if (rubberBandSettings == null)
        {
            if (!Directory.Exists(@"Assets\Resources"))
            {
                Debug.Log("Create \"resource\" folder");
                AssetDatabase.CreateFolder(@"Assets", "Resources");
            }
            Debug.Log("new RubberBandSettings created");
            rubberBandSettings = ScriptableObject.CreateInstance<RubberBandSettings>();
            AssetDatabase.CreateAsset(rubberBandSettings, @"Assets\Resources\RubberBandSettings.asset");
        }

        if (!AssetDatabase.Contains(rubberBandSettings))
            AssetDatabase.CreateAsset(rubberBandSettings, @"Assets\Resources\RubberBandSettings.asset");

        AssetDatabase.SaveAssets();
        Selection.objects = new Object[] { rubberBandSettings };
    }
#endif

    public static float RubberBandPlayerPositionMultiplier(int pos, float lastRBMutiplier) 
    {
        if (rubberBandSettings == null)
            rubberBandSettings = (RubberBandSettings)Resources.Load("RubberBandSettings");

        if (!rubberBandSettings.enablePlayerPositionRB)
            return lastRBMutiplier;

        float multiplier;

        pos--;
        if (pos < 0)
            pos = 0;

        if (pos >= rubberBandSettings.rubberBandPlayerPositionMultipliers.Length)
            multiplier = rubberBandSettings.rubberBandPlayerPositionMultipliers[rubberBandSettings.rubberBandPlayerPositionMultipliers.Length - 1];
        else
            multiplier = rubberBandSettings.rubberBandPlayerPositionMultipliers[pos];

        if (lastRBMutiplier == 0)
            lastRBMutiplier = multiplier;

        return lastRBMutiplier +
            ((Time.deltaTime / rubberBandSettings.transactionTime) *
            (multiplier - 
            lastRBMutiplier));
    }

    public static float RubberBandPositionMultiplier(int pos, float lastRBMutiplier)
    {
        if (rubberBandSettings == null)
            rubberBandSettings = (RubberBandSettings)Resources.Load("RubberBandSettings");

        if (!rubberBandSettings.enablePositionRB)
            return lastRBMutiplier;

        float multiplier;

        pos--;
        if (pos < 0)
            pos = 0;

        if (pos >= rubberBandSettings.rubberBandPositionMultipliers.Length)
            multiplier = rubberBandSettings.rubberBandPositionMultipliers[rubberBandSettings.rubberBandPositionMultipliers.Length - 1];
        else
            multiplier = rubberBandSettings.rubberBandPositionMultipliers[pos];

        if (lastRBMutiplier == 0)
            lastRBMutiplier = multiplier;

        return lastRBMutiplier +
            ((Time.deltaTime / rubberBandSettings.transactionTime) *
            (multiplier -
            lastRBMutiplier));
    }

    public static float RubberBandDragAccelShiftMultiplier(int pos, float lastRBMutiplier) 
    {
        if (rubberBandSettings == null)
            rubberBandSettings = (RubberBandSettings)Resources.Load("RubberBandSettings");

        if (!rubberBandSettings.enableDragAccelShiftRB)
            return lastRBMutiplier;

        float multiplier;

        pos--;
        if (pos < 0)
            pos = 0;

        if (pos >= rubberBandSettings.rubberBandDragAccelShift.Length)
            multiplier = rubberBandSettings.rubberBandDragAccelShift[rubberBandSettings.rubberBandDragAccelShift.Length - 1];
        else
            multiplier = rubberBandSettings.rubberBandDragAccelShift[pos];

        if (lastRBMutiplier == 0)
            lastRBMutiplier = multiplier;

        return lastRBMutiplier +
            ((Time.deltaTime / rubberBandSettings.transactionTime) *
            (multiplier -
            lastRBMutiplier));
    }

    public static float getRandomAISeed()
    {
        if (rubberBandSettings == null)
            rubberBandSettings = (RubberBandSettings)Resources.Load("RubberBandSettings");

        float min = rubberBandSettings.randomAISeedMinLimit;
        float max = rubberBandSettings.randomAISeedMaxLimit;
        
        if (max - min <= 0)
            return 1;

        float range = max - min;
        return min + (Random.value * range);
    }
}
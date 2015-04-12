#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;
using UnityEngine;

public class HPSettings : ScriptableObject
{
    private static HPSettings hpSettings;

    public float respawnHPCost;
    public static float RespawnHPCost
    {
        get {
            if (hpSettings==null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.respawnHPCost; 
        }
    }

    public float maxSpeedReduction = 0.4f;
    public static float MaxSpeedReduction
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.maxSpeedReduction;
        }
    }

    public float minImpactForce = 20f;
    public static float MinImpactForce
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.minImpactForce;
        }
    }

    public float impactDamageMultiplier = 0.5f;
    public static float ImpactDamageMultiplier
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.impactDamageMultiplier;
        }
    }

    public float missileDamage = 20f;
    public static float MissileDamage
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.missileDamage;
        }
    }

    public float poisonDamage = 1f;
    public static float PoisonDamage
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.poisonDamage;
        }
    }

    public float healthRegeneration = 20f;
    public static float HealthRegeneration
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.healthRegeneration;
        }
    }

    public float shieldRegeneration = 1f;
    public static float ShieldRegeneration
    {
        get
        {
            if (hpSettings == null)
                hpSettings = (HPSettings)Resources.Load("HPSettings");

            return hpSettings.shieldRegeneration;
        }
    }

#if UNITY_EDITOR
    [MenuItem("WarpSpeed Settings/HP")]
    static void Init()
    {
        hpSettings = (HPSettings)AssetDatabase.LoadAssetAtPath(@"Assets\Resources\HPSettings.asset", typeof(HPSettings));

        if (hpSettings == null)
        {
            if (!Directory.Exists(@"Assets\Resources"))
            {
                Debug.Log("Create \"resource\" folder");
                AssetDatabase.CreateFolder(@"Assets", "Resources");
            }
            Debug.Log("new HPSettings created");
            hpSettings = ScriptableObject.CreateInstance<HPSettings>();
            AssetDatabase.CreateAsset(hpSettings, @"Assets\Resources\HPSettings.asset");
        }

        if (!AssetDatabase.Contains(hpSettings))
            AssetDatabase.CreateAsset(hpSettings, @"Assets\Resources\HPSettings.asset");

        AssetDatabase.SaveAssets();
        Selection.objects = new Object[] { hpSettings };
    }
#endif
}
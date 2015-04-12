#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.IO;
using UnityEngine;

public class SoundSettings : ScriptableObject
{
    private static SoundSettings soundSettings;

    public float minImpactForce = 15;
    public static float MinImpactForce
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            return soundSettings.minImpactForce;
        }
    }

    public float audioVolume = 1;
    public static float AudioVolume
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            return soundSettings.audioVolume;
        }
    }

    public AudioClip[] pMoneyCrash;
    public static AudioClip PMoneyCrashRandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if(soundSettings.pMoneyCrash.Length > 0)
                return soundSettings.pMoneyCrash[Random.Range(0,soundSettings.pMoneyCrash.Length)];
            else
                return null;
        }
    }

    public AudioClip[] ghetts;
    public static AudioClip GhettsRandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.ghetts.Length > 0)
                return soundSettings.ghetts[Random.Range(0, soundSettings.ghetts.Length)];
            else
                return null;
        }
    }

    public AudioClip[] durrtyGoodz;
    public static AudioClip DurrtyGoodzRandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.durrtyGoodz.Length > 0)
                return soundSettings.durrtyGoodz[Random.Range(0, soundSettings.durrtyGoodz.Length)];
            else
                return null;
        }
    }

    public AudioClip[] crazyTitch;
    public static AudioClip CrazyTitchRandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.crazyTitch.Length > 0)
                return soundSettings.crazyTitch[Random.Range(0, soundSettings.crazyTitch.Length)];
            else
                return null;
        }
    }

    public AudioClip[] jME;
    public static AudioClip JMERandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.jME.Length > 0)
                return soundSettings.jME[Random.Range(0, soundSettings.jME.Length)];
            else
                return null;
        }
    }

    public AudioClip[] dDoubleE;
    public static AudioClip DDoubleERandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.dDoubleE.Length > 0)
                return soundSettings.dDoubleE[Random.Range(0, soundSettings.dDoubleE.Length)];
            else
                return null;
        }
    }

    public AudioClip[] gunShots;
    public static AudioClip GunShotsRandom
    {
        get
        {
            if (soundSettings == null)
                soundSettings = (SoundSettings)Resources.Load("SoundSettings");

            if (soundSettings.gunShots.Length > 0)
                return soundSettings.gunShots[Random.Range(0, soundSettings.gunShots.Length)];
            else
                return null;
        }
    }

#if UNITY_EDITOR
    [MenuItem("WarpSpeed Settings/Sound")]
    static void Init()
    {
        soundSettings = (SoundSettings)AssetDatabase.LoadAssetAtPath(@"Assets\Resources\SoundSettings.asset", typeof(SoundSettings));

        if (soundSettings == null)
        {
            if (!Directory.Exists(@"Assets\Resources"))
            {
                Debug.Log("Create \"resource\" folder");
                AssetDatabase.CreateFolder(@"Assets", "Resources");
            }
            Debug.Log("new SoundSettings created");
            soundSettings = ScriptableObject.CreateInstance<SoundSettings>();
            AssetDatabase.CreateAsset(soundSettings, @"Assets\Resources\SoundSettings.asset");
        }

        if (!AssetDatabase.Contains(soundSettings))
            AssetDatabase.CreateAsset(soundSettings, @"Assets\Resources\SoundSettings.asset");

        AssetDatabase.SaveAssets();
        Selection.objects = new Object[] { soundSettings };
    }
#endif
}
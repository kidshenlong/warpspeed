using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerScript))]
public class PlayerSoundScript : MonoBehaviour
{
    private static readonly float DISABLE_TIME = 1f;

    private AudioSource source;
    private PlayerScript player;
    private float collisionImpact;
    private float audioVolume;

    private float disableTimeCounter;
    private bool isSoundEnable;
    private Craft craft;

    void Awake()
    {
        source = this.gameObject.AddComponent<AudioSource>();
        source.rolloffMode = AudioRolloffMode.Custom;
            source.maxDistance = 50;
        if (source == null)
            source = (AudioSource)FindObjectOfType(typeof(AudioSource));
        player = GetComponent<PlayerScript>();
    }

    public void addCrashSound(Craft craft)
    {
        this.craft = craft;

        collisionImpact = SoundSettings.MinImpactForce;
        audioVolume = SoundSettings.AudioVolume;
    }

    void Update() 
    {
        if (!isSoundEnable) 
        {
            disableTimeCounter += Time.deltaTime;
            if (disableTimeCounter >= DISABLE_TIME) 
            {
                isSoundEnable = true;
                disableTimeCounter = 0;
            }
        }
    }

    public void playGunShotSound() 
    { 
        AudioClip itemSoundToPlay = SoundSettings.GunShotsRandom;
        if(itemSoundToPlay!=null)
            source.PlayOneShot(itemSoundToPlay, audioVolume);
    }

    void OnCollisionEnter(Collision other)
    {
        if (isSoundEnable && collisionImpact < other.impactForceSum.magnitude)
        {
            PlayerScript otherPlayer = other.gameObject.GetComponent<PlayerScript>();
            if (otherPlayer != null)
            {
                if (!player.IsHuman && otherPlayer.IsHuman)
                {
                    isSoundEnable = false;

                    AudioClip crashSoundToPlay = null;
                    switch (craft)
                    {
                        case Craft.P_Money:
 
                            crashSoundToPlay = SoundSettings.PMoneyCrashRandom;
                            break;
                        case Craft.Ghetts:
                            crashSoundToPlay = SoundSettings.GhettsRandom;
                            break;
                        case Craft.Durrty_Goodz:
                            crashSoundToPlay = SoundSettings.DurrtyGoodzRandom;
                            break;
                        case Craft.Crazy_Titch:
                            crashSoundToPlay = SoundSettings.CrazyTitchRandom;
                            break;
                        case Craft.JME:
                            crashSoundToPlay = SoundSettings.JMERandom;
                            break;
                        case Craft.D_Double_E:
                            crashSoundToPlay = SoundSettings.DDoubleERandom;
                            break;
                    }

                    if(crashSoundToPlay!=null)
                        source.PlayOneShot(crashSoundToPlay, audioVolume);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
    public static Soundmanager Instance { get; private set;}
    public enum Sound{
        BuildingPlaced,
        GameOver,
        BuildingDestoryed,
        BuildingDamaged,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
    }
    private AudioSource audioSource;
    private Dictionary<Sound ,AudioClip> audioClipDictionary;
    private float volume =0.5f;
    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            audioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot( audioClipDictionary[sound],volume);
    }
    public void VolumeIncrease()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);

    }
    public void VolumeDecrease()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
    }
    public float GetSoundVolume()
    {
        return volume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume = 0.5f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

    }
    public void IncreaseVolume()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }

    public void DcreasesVolume()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}

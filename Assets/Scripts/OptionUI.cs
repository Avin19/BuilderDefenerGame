using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionUI : MonoBehaviour
{
    private TextMeshProUGUI soundText, musicText;

    private void Awake()
    {
        soundText = transform.Find("soundText").Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        transform.Find("soundText").Find("soundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Soundmanager.Instance.VolumeIncrease();
            soundText.SetText(Mathf.RoundToInt(Soundmanager.Instance.GetSoundVolume() * 10).ToString());

        });
        transform.Find("soundText").Find("soundDecreseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Soundmanager.Instance.VolumeDecrease();
            soundText.SetText(Mathf.RoundToInt(Soundmanager.Instance.GetSoundVolume() * 10).ToString());
        });
        musicText = transform.Find("musicText").Find("musicVolumeText").GetComponent<TextMeshProUGUI>();
        transform.Find("musicText").Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            musicText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
        });
        transform.Find("musicText").Find("musicDcreasesBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.DcreasesVolume();
            musicText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneMAnager.Load(GameSceneMAnager.Scene.MainMenuScene);
        });

    }
    private void Start()
    {
        soundText.SetText(Mathf.RoundToInt(Soundmanager.Instance.GetSoundVolume() * 10).ToString());
        musicText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
        Show();

    }
    public void Show()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if(gameObject.activeSelf)
        {
            Time.timeScale =0f;
        }
        else{
            Time.timeScale =1f;
        }
    }
}

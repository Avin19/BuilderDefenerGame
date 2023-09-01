using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{   
    public static GameOverUI Instance { get; private set;}

    private void Awake()
    {   

        Instance = this;
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(()=>{GameSceneMAnager.Load(GameSceneMAnager.Scene.GameScene);});
        transform.Find("mainMenBtn").GetComponent<Button>().onClick.AddListener(()=>{GameSceneMAnager.Load(GameSceneMAnager.Scene.MainMenuScene);});
        Hide();

    }
    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("text").GetComponent<TextMeshProUGUI>().SetText("You Survived "+EnemyWaveManager.Instance.NextWave()+" waves !");

    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Awake() {
        transform.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(()=>{
            GameSceneMAnager.Load(GameSceneMAnager.Scene.GameScene);
        });
        transform.Find("OuitBtn").GetComponent<Button>().onClick.AddListener(()=>{
            Application.Quit();
        });
    }
}


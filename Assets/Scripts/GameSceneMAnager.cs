using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneMAnager 
{
   public enum Scene {
    GameScene,
    MainMenuScene,
   }
   public static void Load(Scene sceneLoad)
   {
        SceneManager.LoadScene(sceneLoad.ToString());
   }
}

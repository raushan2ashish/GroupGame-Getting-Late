using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Level1");
        

    }
    public void QuitGame()
    {
        // For the editor, log a message since Application.Quit does not work in the editor.
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the Unity editor
        #else
        Application.Quit(); // Quit the application in a built game
        #endif


    }
}

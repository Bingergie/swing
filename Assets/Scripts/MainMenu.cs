using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    
    public void StartGame()
    {
        transition.SetTrigger("Transition");
        Invoke(nameof(LoadFirstLevel), 1f);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameObject player;
    public Animator transition;

    public float endX = 10;
    public float deathY = -10;

    void Update()
    {
        if (player.transform.position.x >= endX)
        {
            transition.SetTrigger("Transition");
            Invoke(nameof(LoadNextLevel), 1f);
        }
        else if (player.transform.position.y <= deathY)
        {
            transition.SetTrigger("Transition");
            Invoke(nameof(LoadDeathScreen), 1f);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void LoadDeathScreen()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
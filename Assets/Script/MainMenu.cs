﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}

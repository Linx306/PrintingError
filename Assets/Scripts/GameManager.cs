using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public GameObject gameOverText;
    public GameObject resumeButton;

    private bool isPaused = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    //  PAUSA
    public void PauseGame()
    {
        isPaused = true;

        gameOverPanel.SetActive(true);
        gameOverText.SetActive(false);
        resumeButton.SetActive(true);

        Time.timeScale = 0f;
    }

    //  REANUDAR
    public void ResumeGame()
    {
        isPaused = false;

        gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    //  GAME OVER
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.SetActive(true);
        resumeButton.SetActive(false);

        Time.timeScale = 0f;
    }

    // PARA BLOQUEAR INPUTS
    public bool IsPaused()
    {
        return isPaused;
    }

    // REINICIAR NIVEL
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  IR AL MENÚ
    public void LoadMenu(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    //  SALIR DEL JUEGO
    public void QuitGame()
    {
        Application.Quit();
    }
}
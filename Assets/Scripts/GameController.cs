using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject gameOverMenu;
    public GameObject levelCompleteMenu;
    public GameObject levelCompleteText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        gameOverMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        levelCompleteText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(2f);
        levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        levelCompleteMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

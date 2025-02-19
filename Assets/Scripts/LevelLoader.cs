using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reload()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex+1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

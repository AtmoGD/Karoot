using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class UILoader : MonoBehaviour
{
    public GameObject[] objs;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (!pauseMenuUI)
        {
            return;
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause()
    {
        if (!pauseMenuUI)
        {
            return;
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        // Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exitbutton was clicked");
    }

    IEnumerator Timer(int theScene)
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadSceneAsync(theScene);
    }

    public void GoToNextScene(int theScene)
    {
        DeactivateAllButtons();
        StartCoroutine(Timer(theScene));
    }

    public void DeactivateAllButtons()
    {
        objs = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject button in objs)
        {
            button.GetComponent<Button>().interactable = false;
            //button.interactable = false ;
        }
    }
}

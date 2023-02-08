using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(true);
    }

    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            if (isPaused)
            {
                Resume();
            }
            else 
            {       
                Pause();
            }
        }
    }

    public void Pause()
    {
        Debug.Log("pause");
        
        //Debug.Log("space pressed");
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.transform.localScale = new Vector3(1f, 1f, 0f);
      //  pauseMenu.SetActive(true);
        //Menu.SetActive(true);
    }

    public void Resume()
    {
        //    pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        pauseMenu.transform.localScale = new Vector3(0f, 0f, 0f);

        Debug.Log("Resume");
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}

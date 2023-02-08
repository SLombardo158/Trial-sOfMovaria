using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int player;
    public PlayerAnimations play;
    public GameObject videoPlayer;
    public AudioSource audio;



    public void ChangeSceneByAlice(string name)
    {
        SceneManager.LoadScene(name);
        player = 1;
        Debug.Log("Alice");
        //    play.startalice();
    }

    public void ChangeSceneByName(string name)
    {
        //videoPlayer.SetActive(false);
        // SceneManager.LoadScene(name);
        videoPlayer.SetActive(true);
        StartCoroutine(waiter());
        player = 1;
        Debug.Log("asfvasvafsvasfv");
    //    play.startalice();
    }
    public void ChangeSceneByName6(string name)
    {
        
         SceneManager.LoadScene(name);
        
    }

    public void ChangeSceneByName2(string name)
    {
        Debug.Log("Jordan");
        videoPlayer.SetActive(true);
        StartCoroutine(waiter());
        //SceneManager.LoadScene(name);
        player = 2;
    }
    public void ChangeSceneByName3(string name)
    {
        SceneManager.LoadScene(name);
        player = 3;
    }
    public void ChangeSceneByName4(string name)
    {
        //SceneManager.LoadScene(name);
        videoPlayer.SetActive(true);
        StartCoroutine(waiter());
        player = 4;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!!");
    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(16f); 
        string name = "Game";
        SceneManager.LoadScene(name);

    }
}

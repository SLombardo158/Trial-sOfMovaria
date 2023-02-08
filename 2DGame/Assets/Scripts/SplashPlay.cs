using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashPlay : MonoBehaviour
{

    public GameObject videoPlayer;

    void Start()
    {
        videoPlayer.SetActive(true);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(6f); //16
        string name = "Menu";
        SceneManager.LoadScene(name);

    }
}

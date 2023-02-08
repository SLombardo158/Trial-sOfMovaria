using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalVideo : MonoBehaviour
{
    public GameObject videoPlayer;
    public AudioSource song;
    void Start()
    {
        videoPlayer.SetActive(false);
    }
    public void called()

    {
        song.Pause();
        videoPlayer.SetActive(true);
        StartCoroutine(waiter());



    }






    IEnumerator waiter()

    {
        yield return new WaitForSecondsRealtime(46f);
        string name = "Menu";
        SceneManager.LoadScene(name);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cut : MonoBehaviour

{
    public static bool iscuton;
    public static bool hasp = false;
    public GameObject videoPlayer;
    public AudioSource song;
    [SerializeField] GameObject canvas;


    void OnTriggerEnter2D(Collider2D player)
    {
        videoPlayer.SetActive(false);
        if (player.gameObject.tag == "Player" && hasp == false)
        {
            song.Pause();
            iscuton = true;
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, 1f);
            StartCoroutine(waiter());
            Time.timeScale = 0f;
            canvas.SetActive(false);
        }

    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(27f);
        hasp = true;
        Time.timeScale = 1f;
        canvas.SetActive(true);
        song.UnPause();
    }

}
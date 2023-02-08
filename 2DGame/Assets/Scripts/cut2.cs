using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cut2 : MonoBehaviour

{
    public static bool iscuton;
    public static bool hasp = false;
    public GameObject videoPlayer;
    public AudioSource song;
    public GameObject canvas;

    public GameObject dispose;

    void OnTriggerEnter2D(Collider2D player)
    {
        
        //videoPlayer.SetActive(false);

        if (player.gameObject.tag == "Player" && hasp == false)
        {
            song.Pause();
            iscuton = true;
            canvas.SetActive(false);
            videoPlayer.SetActive(true);
           
            StartCoroutine(waiter());
            dispose.SetActive(false);
            Time.timeScale = 0f;
            
        }

    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(21f); //used to be 21 on dec 1
        Destroy(videoPlayer, 1f);
        hasp = true;
        Time.timeScale = 1f;
        canvas.SetActive(true);
        dispose.SetActive(true);
        song.UnPause();
    }

}
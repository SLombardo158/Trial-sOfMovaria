using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelup: MonoBehaviour
{
    public Enemy Enemy;

    public static int player;
    public PlayerAnimations play;

    public Image black;
    public Animator anim;
    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
        player = 1;
        Debug.Log("Alice");
        //    play.startalice();
    }

    public void ChangeSceneByName2(string name)
    {
        Debug.Log("Jordan");
        SceneManager.LoadScene(name);
        player = 2;
    }
    public void ChangeSceneByName3(string name)
    {
        SceneManager.LoadScene(name);
        player = 3;
    }
    public void ChangeSceneByName4(string name)
    {
        SceneManager.LoadScene(name);
        player = 4;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!!");
    }


    void OnTriggerEnter2D(Collider2D player)
    {

        if (player.gameObject.tag == "Player" && Enemy.haswon == true)
        {
            StartCoroutine(waiter());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    //  public void ChangeSceneByName(string name)
    //{
    //  SceneManager.LoadScene(name);
    //}
    IEnumerator waiter()
    {
        anim.SetBool("fade", true);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}

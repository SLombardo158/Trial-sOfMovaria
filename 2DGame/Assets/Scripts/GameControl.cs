using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public cut cut;
    public static bool hasw = false;
    public GameObject Objecttodisable;
    public GameObject Objecttodisable1;
    void Update()
    {

        if (cut.iscuton == true && hasw == false)
        {
            StartCoroutine(waiter());
            Objecttodisable.SetActive(false);
            Objecttodisable1.SetActive(false);
        }
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(1f);
        hasw = true;
        Objecttodisable.SetActive(true);
        Objecttodisable1.SetActive(true);



    }
}
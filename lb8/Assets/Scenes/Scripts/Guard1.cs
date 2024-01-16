using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard1 : MonoBehaviour
{
    public GameObject panel;

    Animator anim;
    public int state = 0;

    public GameObject txt;
    public GameObject txt1;

    public GameObject finish;
    public GameObject finish1;

    void Start()
    {
        anim = GetComponent<Animator>();
        txt1.SetActive(false);
        //anim.SetInteger("state", 0);

    }

    public void interact()
    {
        anim.SetInteger("state", 1);
    }


    public void hello()
    {
        panel.SetActive(true);

        if (ScoreboardScript.i < 10)
        {
            txt.SetActive(true);
            txt1.SetActive(false);

            //ScoreboardScript.i = 5;
            ScoreboardScript.i += 1;
            Debug.Log(ScoreboardScript.i);
        }
        else if (ScoreboardScript.i >= 10)
        {
            txt.SetActive(false);
            txt1.SetActive(true);

            finish.SetActive(false);
            finish1.SetActive(true);
        }

    }

    public void closePanel()
    {
        panel.SetActive(false);
        anim.SetInteger("state", 0);

    }


    /*public override action getAction()
    {
        return action.Talk;
    }*/

}

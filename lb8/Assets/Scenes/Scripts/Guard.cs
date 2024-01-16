using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GameObject panel;
    public GameObject start;
    public GameObject start1;

    Animator anim;
    public int state = 0;
    //float points;

    public GameObject txt;
    public GameObject txt1;
    public GameObject txt2;

    public GameObject finish;
    public GameObject finish1;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //txt.SetActive(false);
        txt1.SetActive(false);
        txt2.SetActive(false);
        anim.SetInteger("state", 0);
    }

    public void interact()
    {
        anim.SetInteger("state", 1);
        //anim.SetTrigger("isTalking");
    }


    public void hello()
    {
        panel.SetActive(true);

        if (ScoreboardScript.i == 0)
        {
            txt.SetActive(true);
            txt1.SetActive(false);
            txt2.SetActive(false);
            //ScoreboardScript.i = 5;
            ScoreboardScript.i += 1;
            Debug.Log(ScoreboardScript.i);
        }
        else if (ScoreboardScript.i != 5 && ScoreboardScript.i > 0)
        {
            txt.SetActive(false);
            txt1.SetActive(true);
            txt2.SetActive(false);
        }
        else if (ScoreboardScript.i == 5)
        {
            txt.SetActive(false);
            txt1.SetActive(false);
            txt2.SetActive(true);

            finish.SetActive(false);
            finish1.SetActive(true);
        }
        //txt1.SetActive(false);
    }

    public void closePanel()
    {
        panel.SetActive(false);
        start.SetActive(false);
        start1.SetActive(true);
        anim.SetInteger("state", 0);

    }


    /*public override action getAction()
    {
        return action.Talk;
    }*/

}

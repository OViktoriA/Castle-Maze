using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat1 : MonoBehaviour
{
    //int state = 0;
    //bool dead = false;
    public float hp = 5f;
    //float points = 10;

    public GameObject finish;
    public GameObject panel;

    public void Start()
    {
        finish.SetActive(false);
    }

    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            takeDamage();
        }
        //Debug.Log(hp);
    }

    public void takeDamage()
    {
        /*if (GameObject.FindWithTag("T1"))
            points = 5;
        if (GameObject.FindWithTag("T2"))
            points = 10;

        GameObject.FindWithTag("ScoreBoard").GetComponent<ScoreboardScript>().score_up(points);*/

        //dead = true;
        Destroy(this.gameObject, 2);

        ScoreboardScript.i += 1;

        if (ScoreboardScript.i == 3)
        {
            finish.SetActive(true);
            panel.SetActive(true);
        }

        Debug.Log(ScoreboardScript.i);

    }

    public void closePanel()
    {
        panel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat : MonoBehaviour
{
    //int state = 0;
    //bool dead = false;
    public float hp = 10f;
    float points = 5;

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
        if (GameObject.FindWithTag("T1"))
            points = 10;

        GameObject.FindWithTag("ScoreBoard").GetComponent<ScoreboardScript>().score_up(points);

        //dead = true;
        Destroy(this.gameObject, 2);

        ScoreboardScript.i += 1;

    }
}

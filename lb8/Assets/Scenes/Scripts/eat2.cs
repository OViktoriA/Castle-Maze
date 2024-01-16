using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class eat2 : MonoBehaviour
{
    //int state = 0;
    //bool dead = false;
    public float hp = 10f;
    float points;

    public TMP_Text point;

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
        //dead = true;
        Destroy(this.gameObject, 2);

        ScoreboardScript.i += 1;

        if (GameObject.FindWithTag("Target1"))
            points = ScoreboardScript.i;

        Debug.Log(ScoreboardScript.i);

        GameObject.FindWithTag("ScoreBoard").GetComponent<Board>().score_up(points);
    }
}

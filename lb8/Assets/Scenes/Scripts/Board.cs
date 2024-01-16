using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreboardScript : MonoBehaviour
{
    public TMP_Text score_text;
    public TMP_Text hp_text;

    //public TMP_Text txt;
    

    float scores = 0;
    float scores1 = 0;

    public static int i;

    public void Start()
    {

    }

    
    public void score_up(float points)
    {
        scores += points;

        score_text.text = scores.ToString("0");
    }

    public void hp_up(float hp)
    {
        scores1 = hp;
        hp_text.text = "HP: " + scores1.ToString("0");
    }

}

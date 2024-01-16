using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Board : MonoBehaviour
{
    public TMP_Text point;
    float scores = 0;

    public void score_up(float points)
    {
        scores = points;
        point.text = scores.ToString("0");
    }

}

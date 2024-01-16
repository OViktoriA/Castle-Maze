using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void nextLVL(int lvl)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvl);

        ScoreboardScript.i = 0;

        //Debug.Log(ScoreboardScript.i);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject Panel;

    private void Start()
    {
        Panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void nextLVL(int lvl)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvl);

        ScoreboardScript.i = 0;

        //Debug.Log(ScoreboardScript.i);
    }
}

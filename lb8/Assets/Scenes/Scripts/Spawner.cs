
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    //public Camera cam;
    public GameObject mazeHandler;

    public Cell CellPrefab;

    public Vector2 CellSize = new Vector2(1, 1);

    public int Width = 10;
    public int Height = 10;

    public GameObject start;
    public GameObject restart;
    public GameObject menu;
    public GameObject player;
    public GameObject spheres;




    void Start()
    {
        start.SetActive(true);
    }

    public void GenerateMaze()
    {
        foreach (Transform child in mazeHandler.transform)
            GameObject.Destroy(child.gameObject);

        Generator generator = new Generator();
        Maze maze = generator.GenerateMaze(Width, Height);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.cells.GetLength(1); z++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, 0, z * CellSize.y), Quaternion.identity);

                if (maze.cells[x, z].Left == false)
                    Destroy(c.Left);
                if (maze.cells[x, z].Right == false)
                    Destroy(c.Right);
                if (maze.cells[x, z].Up == false)
                    Destroy(c.Up);
                if (maze.cells[x, z].Bottom == false)
                    Destroy(c.Bottom);


                c.transform.parent = mazeHandler.transform;

                c.distance.text = maze.cells[x, z].distance.ToString();

            }

        }

        //cam.transform.position = new Vector3((Width * CellSize.x) / 2, Mathf.Max(Width, Height) * 8, (Height * CellSize.y) / 2);

        restart.SetActive(true);
        menu.SetActive(true);
        player.SetActive(true);
        start.SetActive(false);
        spheres.SetActive(true);
    }

    public void nextLVL(int lvl)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(lvl);

        ScoreboardScript.i = 0;
    }

}

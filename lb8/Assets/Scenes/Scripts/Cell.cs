using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cell;



public class Cell : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Bottom;

    public TMP_Text distance;
}
public class Maze
{
    public MazeCell[,] cells;
}
public class MazeCell
{
    public int X;
    public int Y;

    public bool Left = true;
    public bool Right = true;
    public bool Up = true;
    public bool Bottom = true;

    public bool Visited = false;

    public int distance = -1;
    public int delete = 5;
}
public class Generator
{
    int Width = 10;
    int Height = 10;

    public Maze GenerateMaze(int Width, int Height)
    {
        this.Width = Width;
        this.Height = Height;

        MazeCell[,] cells = new MazeCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                cells[x, y] = new MazeCell { X = x, Y = y };
            }
        }
        //removeWalls(cells);
        removeWallsOB(cells);
        add_cycles(cells);
        calculate_distances(cells);
        
        Maze maze = new Maze();

        maze.cells = cells;

        PlaceMaze(cells);

        return maze;
    }
    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.Bottom = false;
                b.Up = false;
            }
            else
            {
                b.Bottom = false;
                a.Up = false;
            }
        }
        else
        {
            if (a.X > b.X)
            {
                a.Left = false;
                b.Right = false;
            }
            else
            {
                b.Left = false;
                a.Right = false;
            }
        }
    }

    private void removeWalls(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.Visited = true;
        current.distance = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 1 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 1 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;

                stack.Push(chosen);

                current = chosen;
                chosen.distance = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }


        } while (stack.Count > 0);
    }

    private void removeWallsOB(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.Visited = true;

        int all_cells = maze.Length - 1;
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 1) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 1) unvisitedNeighbours.Add(maze[x, y + 1]);

            MazeCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];

            if (chosen.Visited == false)
            {
                RemoveWall(current, chosen);

                chosen.Visited = true;
                all_cells--;
            }

            current = chosen;

        } while (all_cells > 0);
    }

    void add_cycles(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];

            int x = Random.Range(0, 5);
            int y = Random.Range(0, 5);
            current = maze[(int)x, (int)y];

            if (current.Up == true && y + 1 < Height - 1)
            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }
            else

            if (current.Left == true && x - 1 > 0)

            {
                RemoveWall(current, maze[(int)x - 1, (int)y]);
            }
            else

            if (current.Right == true && x + 1 > 0)

            {
                RemoveWall(current, maze[(int)x + 1, (int)y]);
            }
            else

            if (current.Up == true && y + 1 < Width - 1)

            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }

            x = Random.Range(5, 10);
            y = Random.Range(0, 5);
            current = maze[(int)x, (int)y];
            if (current.Up == true && y + 1 < Height - 1)
            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }
            else

           if (current.Left == true && x - 1 > 0)

            {
                RemoveWall(current, maze[(int)x - 1, (int)y]);
            }
            else

           if (current.Right == true && x + 1 > 0)

            {
                RemoveWall(current, maze[(int)x + 1, (int)y]);
            }
            else

           if (current.Up == true && y + 1 < Width - 1)

            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }

            x = Random.Range(5, 10);
            y = Random.Range(5, 10);
            current = maze[(int)x, (int)y];
            if (current.Up == true && y + 1 < Height - 1)
            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }
            else

           if (current.Left == true && x - 1 > 0)

            {
                RemoveWall(current, maze[(int)x - 1, (int)y]);
            }
            else

           if (current.Right == true && x + 1 < Width - 1)

            {
                RemoveWall(current, maze[(int)x + 1, (int)y]);
            }
            else

           if (current.Up == true && y + 1 < Height - 1)

            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }

            x = Random.Range(0, 5);
            y = Random.Range(5, 10);
            current = maze[(int)x, (int)y];
            if (current.Up == true && y + 1 < Height - 1)
            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }
            else

           if (current.Left == true && x - 1 > 0)

            {
                RemoveWall(current, maze[(int)x - 1, (int)y]);
            }
            else

           if (current.Right == true && x + 1 > 0)

            {
                RemoveWall(current, maze[(int)x + 1, (int)y]);
            }
            else

           if (current.Up == true && y + 1 < Width - 1)

            {
                RemoveWall(current, maze[(int)x, (int)y + 1]);
            }
        
    }
    private void calculate_distances(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.distance = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        stack.Push(current);
        do
        {
            current = stack.Pop();
            int x = current.X;
            int y = current.Y;

            if (current.Bottom == false)
                if (maze[x, y - 1].distance == -1 || maze[x, y - 1].distance > current.distance + 1)
                {
                    maze[x, y - 1].distance = current.distance + 1;
                    stack.Push(maze[x, y - 1]);
                }

            if (current.Right == false)
                if (maze[x + 1, y].distance == -1 || maze[x + 1, y].distance > current.distance + 1)
                {
                    maze[x + 1, y].distance = current.distance + 1;
                    stack.Push(maze[x + 1, y]);
                }

            if (current.Up == false)
                if (maze[x, y + 1].distance == -1 || maze[x, y + 1].distance > current.distance + 1)
                {
                    maze[x, y + 1].distance = current.distance + 1;
                    stack.Push(maze[x, y + 1]);
                }

            if (current.Left == false)
                if (maze[x - 1, y].distance == -1 || maze[x - 1, y].distance > current.distance + 1)
                {
                    maze[x - 1, y].distance = current.distance + 1;
                    stack.Push(maze[x - 1, y]);
                }


        } while (stack.Count > 0);
    }

    private void PlaceMaze(MazeCell[,] maze)
    {
        MazeCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].distance > furthest.distance) furthest = maze[x, Height - 2];
            if (maze[x, 0].distance > furthest.distance) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].distance > furthest.distance) furthest = maze[Width - 2, y];
            if (maze[0, y].distance > furthest.distance) furthest = maze[0, y];
        }


    }
}


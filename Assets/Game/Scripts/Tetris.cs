using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tetris : MonoBehaviour
{
    [SerializeField] private float fall_time = 0.8f;
    [SerializeField] private Vector3 rotationPoint;
    private float previous_time;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    //Audio
  
    bool Pause = true;
    // Update is called once per frame
    void Update()
    {
        Move();
        if (CheckIsAboveGrid())
        {
            GameOver();
        }
        if (Input.GetKeyDown("escape"))
        {
            Pause = !Pause;
        }
        Time.timeScale = (Pause) ? 1.00f : 0.00f;
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            Debug.Log(children.name);
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            //If one is bigger than the grid size
            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
            {
                return false;
            }
        }
        return true;
    }

    private void Move()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
            Sound.s_Sound.SoundMovement();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
            Sound.s_Sound.SoundMovement();
        }
        if (Time.time - previous_time > (Input.GetKey(KeyCode.DownArrow) ? fall_time / 10 : fall_time))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddGrid();
                CheckLine();

                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            Sound.s_Sound.SoundDrop();
            previous_time = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            Sound.s_Sound.SoundMovement();
        }


    }

    void AddGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundX, roundY] = children;
        }
    }

    void CheckLine()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        Sound.s_Sound.SoundClearLine();
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public bool CheckIsAboveGrid()
    {
        for (int x = 0; x < width; ++x)
        {
            foreach (Transform mino in transform)
            {
                Vector2 pos = mino.position;
                if (pos.y > height - 1.5)
                    return true;
            }
        }
        return false;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public Vector2 Round(Vector2 vt)
    {
        return new Vector2(Mathf.Round(vt.x), Mathf.Round(vt.y));
    }
}

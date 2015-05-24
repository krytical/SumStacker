using UnityEngine;
using System.Collections;

public class Boxes : MonoBehaviour
{

    float fall = 0;
    public static int gridWeight = 5;
    public static int gridHeight = 8;
    public static Transform[,] grid = new Transform[gridWeight, gridHeight];

    //private bool canMove = true;

    void Start()
    {
        // check if game over
        if (!isValidPosition())
        {
            Application.LoadLevel(0);
            Destroy(gameObject);
        }
        //canMove = true;
    }

    void Update()
    {
        //RIGHT KEY
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Vector2 vOld = round(transform.position);
            transform.position += new Vector3(1, 0, 0);
            if (isValidPosition())
            {
                UpdateBlock(vOld);
            }
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        //LEFT KEY
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Vector2 vOld = round(transform.position);
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPosition())
            {
                UpdateBlock(vOld);
            }
            else
                transform.position += new Vector3(1, 0, 0);
        }
        //DOWN KEY
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                Time.time - fall >= 1 || Input.GetKeyDown(KeyCode.S))
        {
            Vector2 vOld = round(transform.position);
            transform.position += new Vector3(0, -1, 0);
            if (isValidPosition())
            {
                UpdateBlock(vOld);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Grid.deleteCompleteRowsAndDrop();
                FindObjectOfType<GameController>().blockNum++;
                FindObjectOfType<SpawnBox>().SpawnNewBox();
                enabled = false;
            }

            fall = Time.time;
        }
    }

    // Moves a block to a new position
    public void UpdateBlock(Vector2 vOld)
    {
        grid[(int)vOld.x, (int)vOld.y] = null;
        Vector2 v = round(transform.position);
        grid[(int)v.x, (int)v.y] = transform;
    }

    // checks if spot to move to is valid
    bool isValidPosition()
    {
        Vector2 v = round(transform.position);
        if (!isInsideGrid(v) || grid[(int)v.x, (int)v.y] != null)
            return false;
        return true;
    }

    // Rounds a vector to the nearest int
    public static Vector2 round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // Checks if the position is in the grid
    public static bool isInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
         (int)pos.x < gridWeight &&
         (int)pos.y >= 0);
    }

}

using UnityEngine;
using System.Collections;

public class Boxes : MonoBehaviour
{

    float fall = 0;
    public static int gridWeight = 5;
    public static int gridHeight = 8;
    public static Transform[,] grid = new Transform[gridWeight, gridHeight];

    private bool canMove = true;

    void Start() 
    { 
        // check if game over
        if (!isValidPosition())
        {
            Application.LoadLevel(0);
            Destroy(gameObject);
        }
        canMove = true;
    }

    void Update()
    {
        //RIGHT KEY
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            //Vector2 vOld = round(transform.position);
            transform.position += new Vector3(1, 0, 0);
            if (isValidPosition())
            {
                UpdateBlock();
            }
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        //LEFT KEY
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //Vector2 vOld = round(transform.position);
            transform.position += new Vector3(-1, 0, 0);
            if (isValidPosition())
            {
                UpdateBlock();
            }
            else
                transform.position += new Vector3(1, 0, 0);
        }
        //DOWN KEY
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                Time.time - fall >= 1 || Input.GetKeyDown(KeyCode.S))
        {
            //Vector2 vOld = round(transform.position);
            transform.position += new Vector3(0, -1, 0);
            if (isValidPosition())
            {
                UpdateBlock();
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
    /*
        // Moves a block to a new position
        public static void UpdateBlock(Vector2 vOld) 
        {
            grid[(int)vOld.x, (int)vOld.y] = null;
            Vector2 v = round(transform.position);
            grid[(int)v.x, (int)v.y] = tansform;
        }
    */

    // Moves a block to a new position
    void UpdateBlock() // TODO there must be a better way to do this considering all blocks are only a single piece
    {
        for (int y = 0; y < gridHeight; ++y)
            for (int x = 0; x < gridWeight; ++x)
                if (grid[x, y] != null)
                    if (grid[x, y].parent == transform)
                        grid[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 v = round(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    // checks if spot to move to is valid
    bool isValidPosition() //TODO needs fixing
    {
        if (canMove)
        {
            foreach (Transform child in transform)
            {
                Vector2 v = round(child.position);
                if (!isInsideGrid(v))
                    return false;
                if (grid[(int)v.x, (int)v.y] != null &&
                      grid[(int)v.x, (int)v.y].parent != transform)
                    return false;
            }
            return true;
        }
        return false;
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

    // TODO remove these if possible or put collision fields back in 
    void OnCollisionEnter2D(Collision2D coll)
    {
        canMove = false;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        canMove = false;
    }

}

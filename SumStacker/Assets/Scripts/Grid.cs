using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public static int gridWeight = 5;
    public static int gridHeight = 10;
    public static int goaltotal = 10;
    public static Transform[,] grid = new Transform[gridWeight, gridHeight];

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update() { }

    // Deletes all objects in a row
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // Drops all objects in a row
    public static void RowDown(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // Drops all rows from y up
    public static void RowDownAll(int y)
    {
        for (int i = y; i < gridHeight; ++i)
            RowDown(i);
    }

    // Checks if a row is full
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < gridWeight; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // deletes complete rows and drops all boxes above row
    public static void deleteCompleteRowsAndDrop()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (isRowFull(y) && correctRowSum(y))    //TODO Does the row have to be full for it to be deleted?
            {
                DeleteRow(y);
                RowDownAll(y + 1);
                --y;
            }
        }
    }

    // Checks if row sums up to correct value
    public static bool correctRowSum(int y) //TODO make this work
    {

        int currenttotal = 0;
        for (int x = 0; x < gridWeight; ++x)
        {
            currenttotal++; //adds the total of the box values at grid[x, y]
        }
        if (currenttotal == goaltotal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

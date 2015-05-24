using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	
	public static int gridWeight = 5;
	public static int gridHeight = 10;
	public static int goaltotal = 10;
	public static Transform[,] grid = new Transform[gridWeight, gridHeight];
	
	private static int rowBoxValue = 0;
	
	// Use this for initialization
	void Start() { }
	
	// Update is called once per frame
	void Update() { }
	
	// Deletes all objects in a row
	public static void DeleteRow(int y)
	{
		for (int x = 0; x < gridWeight; ++x)
		{
			if (grid[x,y] != null) {
				Destroy(grid[x, y].gameObject);
				grid[x, y] = null;
			}
		}
	}

	public static void DeleteCol(int x)
	{
		for (int y = 0; y < gridWeight; ++y)
		{
			if (grid[x,y] != null) {
				Destroy(grid[x, y].gameObject);
				grid[x, y] = null;
			}
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
	
	// deletes complete rows and drops all boxes above row
	public static void deleteCompleteRowsAndDrop()
	{;
		for (int y = 0; y < gridHeight; ++y)
		{
			if (correctRowSum(y))
			{
				DeleteRow(y);
				RowDownAll(y + 1);
				--y;
			}
		}
	}

	public static void deleteCompleteColsAndDrop()
	{;
		for (int x = 0; x < gridWeight; ++x)
		{
			if (correctColSum(x))
			{
				DeleteCol(x);
				RowDownAll(x + 1);
				--x;
			}
		}
	}

	// Checks if row sums up to correct value
	public static bool correctRowSum(int y) //TODO make this work
	{		
		int currenttotal = 0;
		for (int x = 0; x < gridWeight; x++)
		{
			if (grid[x,y] != null){
				GameObject currbox = grid[x,y].gameObject;
				rowBoxValue = currbox.GetComponent<Boxes>().boxValues;
				currenttotal += rowBoxValue;
			}
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

	public static bool correctColSum(int x) //TODO make this work
		{		
			int currenttotal = 0;
			for (int y = 0; y < gridWeight; y++)
			{
				if (grid[x,y] != null){
					GameObject currbox = grid[x,y].gameObject;
					rowBoxValue = currbox.GetComponent<Boxes>().boxValues;
					currenttotal += rowBoxValue;
				}
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
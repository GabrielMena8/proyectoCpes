using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerSc : MonoBehaviour
{
    [SerializeField] private GridData gridData;
    [SerializeField] private List<GameObject> rows;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform[] goalPositions;
    private GameObject[,] gridPlatforms;

    [SerializeField] private int rowLenght = 10;
    
    public int[,] grid;

    private void Start()
    {
        SavePlatformsInArray();
        SelectGrid(0);
    }

    public bool[] SelectGrid(int index)
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPositions[i].gameObject.SetActive(false);
            goalPositions[i].gameObject.SetActive(false);
        }

        grid = gridData.grids[index];
        PlacePlatforms();
        return new bool[] {
            spawnPositions[0].gameObject.activeSelf,
            spawnPositions[1].gameObject.activeSelf,
            spawnPositions[2].gameObject.activeSelf,
        };
    }

    public void PlacePlatforms()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rowLenght; j++)
            {
                    gridPlatforms[i, j].SetActive(false);
            }
        }

        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rowLenght; j++)
            {
                int value = grid[i, j];
                if (value == 1)
                {
                    gridPlatforms[i, j].SetActive(true);
                }
                else if (value >= 2 && value <= 4)
                {
                    spawnPositions[value - 2].gameObject.SetActive(true);
                    spawnPositions[value - 2].position = gridPlatforms[i, j].transform.position;
                }
                else if(grid[i, j] <= -2 && grid[i, j] >= -4)
                {
                    value = Mathf.Abs(value);
                    goalPositions[value - 2].gameObject.SetActive(true);
                    goalPositions[value - 2].position = gridPlatforms[i, j].transform.position;
                }
            }
        }
    }

    //CREATE THE GRID AND PLACE THE PLATFORM GAMEOBJECTS INTO gridPlatforms
    private void SavePlatformsInArray()
    {
        gridPlatforms = new GameObject[rows.Count, rowLenght];

        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                gridPlatforms[i, j] = rows[i].transform.GetChild(j).gameObject;
                gridPlatforms[i, j].SetActive(false);
            }
        }
    }
}

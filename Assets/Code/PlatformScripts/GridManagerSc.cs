using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerSc : MonoBehaviour
{
    [SerializeField] private GridData gridData;

    [SerializeField] private int rowLenght = 10;
    [SerializeField] private List<GameObject> rows;
    private GameObject[,] gridPlatforms;
    
    public int[,] grid;

    private void Start()
    {
        SavePlatformsInArray();
    }

    public void SelectGrid(int index)
    {
        grid = gridData.grids[index];
        PlacePlatforms();
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
                if (grid[i, j] == 1)
                {
                    gridPlatforms[i, j].SetActive(true);
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

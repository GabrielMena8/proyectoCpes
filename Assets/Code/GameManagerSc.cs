using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSc : MonoBehaviour
{
    private BeansManagerSc beanManager;
    private GridManagerSc gridManager;

    public int currentLevel;
    private void Start()
    {
        beanManager = GetComponentInChildren<BeansManagerSc>();
        gridManager = GetComponentInChildren<GridManagerSc>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SelectLevel(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            SelectLevel(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            SelectLevel(2);
        }
    }

    public void SelectLevel(int index)
    {
        currentLevel = index;

        beanManager.HideBeans();
        bool[] activeBeans = gridManager.SelectGrid(index);
        beanManager.ActivateBeans(activeBeans);
    }

    public void LevelFailed()
    {
        
    }

    public void ResetLevel()
    {
        beanManager.HideBeans();
        bool[] activeBeans = gridManager.SelectGrid(currentLevel);
        beanManager.ActivateBeans(activeBeans);
    }
}

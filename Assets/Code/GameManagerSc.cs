using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSc : MonoBehaviour
{
    [SerializeField] private BeansManagerSc beanManager;
    [SerializeField] private GridManagerSc gridManager;

    private void Start()
    {
        beanManager = GetComponentInChildren<BeansManagerSc>();
        gridManager = GetComponentInChildren<GridManagerSc>();
    }

    public void SelectLevel(int index)
    {
        gridManager.SelectGrid(index);
    }

    public void LevelFailed()
    {

    }

    public void ResetLevel()
    {

        beanManager.ResetBeans();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanPanelSc : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel;
    private BeansManagerSc beansManagerSc;

    private void Start()
    {
        beansManagerSc = GetComponentInParent<BeansManagerSc>();
    }

    private void OnMouseDown()
    {
        if(beansManagerSc.isMoving)
            SendMessageUpwards("HidePanels", instructionPanel, SendMessageOptions.DontRequireReceiver);
    }

    public void ShowPanel()
    {
        instructionPanel.SetActive(true);
    }

    public void HidePanel()
    {
        instructionPanel.SetActive(false);
    }
}

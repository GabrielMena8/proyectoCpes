using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeansManagerSc : MonoBehaviour
{
    //FOR EVERYTHING THE ORDER IS
    //0 -> GREEN
    //1 -> RED
    //2 -> PURPLE
    [SerializeField] private GameObject[] beanGameObjects;
    private Dictionary<string, GameObject> beans = new Dictionary<string, GameObject>();

    public List<Instruction> instructionGreen = new List<Instruction>();
    public List<Instruction> instructionRed = new List<Instruction>();
    public List<Instruction> instructionPurple = new List<Instruction>();

    public bool isMoving;   

    private void Start()
    {
        beans.Add("green", beanGameObjects[0]);
        beans.Add("red", beanGameObjects[1]);
        beans.Add("purple", beanGameObjects[2]);

        StartMovement();
    }

    public void HideBeans()
    {
        BroadcastMessage("ResetBean", SendMessageOptions.DontRequireReceiver);

        beanGameObjects[0].SetActive(false);
        beanGameObjects[1].SetActive(false);
        beanGameObjects[2].SetActive(false);
    }

    public void ActivateBeans(bool[] beanStatus)
    {
        for (int i = 0; i < beanStatus.Length; i++)
        {
            beanGameObjects[i].SetActive(beanStatus[i]);
        }
    }

    public void StartMovement()
    {
        isMoving = true;
        StartCoroutine(BeansMovement());
    }

    public void PlaceSpawns(int[,] grid)
    {
        for(int i = 0; i < grid.Length; i++)
        {

        }
    }

    public void ResetBeans()
    {
        isMoving = false;
        BroadcastMessage("ResetBean", SendMessageOptions.DontRequireReceiver);
    }

    private void BeanDead()
    {
        SendMessageUpwards("LevelFailed", SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator BeansMovement()
    {
        int greenCounter = 0;
        int redCounter = 0;
        int purpleCounter = 0;

        BeanMovementSc greenSc = beans["green"].GetComponent<BeanMovementSc>();
        BeanMovementSc redSc = beans["red"].GetComponent<BeanMovementSc>();
        BeanMovementSc purpleSc = beans["purple"].GetComponent<BeanMovementSc>();

        while (isMoving)
        {
            if(greenSc.isDead || redSc.isDead || purpleSc.isDead)
            {
                BeanDead();
                break;
            }
            else if (greenCounter == instructionGreen.Count && redCounter == instructionRed.Count && purpleCounter == instructionPurple.Count)
            {
                break;
            }

            if (greenCounter < instructionGreen.Count && !greenSc.isWaiting)
            {
                Instruction currentInstruction = instructionGreen[greenCounter];
                greenSc.GetInstruction(currentInstruction.instruction);
                greenCounter++;
            }

            if (redCounter < instructionRed.Count && !redSc.isWaiting)
            {
                Instruction currentInstruction = instructionRed[redCounter];
                beans["red"].GetComponent<BeanMovementSc>().GetInstruction(currentInstruction.instruction);
                redCounter++;
            }

            if (purpleCounter < instructionPurple.Count && !purpleSc.isWaiting)
            {
                Instruction currentInstruction = instructionPurple[purpleCounter];
                beans["purple"].GetComponent<BeanMovementSc>().GetInstruction(currentInstruction.instruction);
                purpleCounter++;
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }
}

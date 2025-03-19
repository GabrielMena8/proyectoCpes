using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeansManagerSc : MonoBehaviour
{
    // FOR EVERYTHING THE ORDER IS:
    // 0 -> GREEN
    // 1 -> RED
    // 2 -> PURPLE
    [SerializeField] private GameObject[] beanGameObjects;
    private Dictionary<string, GameObject> beans = new Dictionary<string, GameObject>();

    // (Opcional) Referencias a paneles de UI que contienen las instrucciones arrastradas
    // Comenta o elimina si no usas InstructionPanel
    public InstructionPanel instructionPanelGreen;
    public InstructionPanel instructionPanelRed;
    public InstructionPanel instructionPanelPurple;

    // Listas de instrucciones para cada bean
    public List<Instruction> instructionGreen = new List<Instruction>();
    public List<Instruction> instructionRed = new List<Instruction>();
    public List<Instruction> instructionPurple = new List<Instruction>();

    public bool isMoving;

    private void Start()
    {
        // Se agregan los beans al diccionario para un acceso f�cil por nombre
        beans.Add("green", beanGameObjects[0]);
        beans.Add("red", beanGameObjects[1]);
        beans.Add("purple", beanGameObjects[2]);

        // Si quieres que arranque autom�ticamente (sin usar paneles UI), 
        // podr�as descomentar la siguiente l�nea:
        // StartMovement();
    }

    /// <summary>
    /// M�todo para leer las instrucciones desde los paneles de UI y comenzar el movimiento.
    /// �salo, por ejemplo, desde un bot�n "Ejecutar" en la UI.
    /// </summary>
    public void ExecuteAllInstructions()
    {
        // Limpia las listas anteriores (opcional)
        instructionGreen.Clear();
        instructionRed.Clear();
        instructionPurple.Clear();

        // Si est�s usando paneles, obt�n las instrucciones de cada uno
        if (instructionPanelGreen != null)
            instructionGreen.AddRange(instructionPanelGreen.GetInstructions());
        if (instructionPanelRed != null)
            instructionRed.AddRange(instructionPanelRed.GetInstructions());
        if (instructionPanelPurple != null)
            instructionPurple.AddRange(instructionPanelPurple.GetInstructions());

        // Ahora inicia el movimiento de los beans con las nuevas listas
        StartMovement();
    }

    /// <summary>
    /// Oculta los beans (y los reinicia) 
    /// </summary>
    public void HideBeans()
    {
        BroadcastMessage("ResetBean", SendMessageOptions.DontRequireReceiver);

        beanGameObjects[0].SetActive(false);
        beanGameObjects[1].SetActive(false);
        beanGameObjects[2].SetActive(false);
    }

    /// <summary>
    /// Activa o desactiva los beans seg�n un array de bool
    /// </summary>
    public void ActivateBeans(bool[] beanStatus)
    {
        for (int i = 0; i < beanStatus.Length; i++)
        {
            beanGameObjects[i].SetActive(beanStatus[i]);
        }
    }

    /// <summary>
    /// Inicia la ejecuci�n de las instrucciones 
    /// </summary>
    public void StartMovement()
    {
        isMoving = true;
        StartCoroutine(BeansMovement());
    }

    /// <summary>
    /// Detiene la ejecuci�n y reinicia los beans
    /// </summary>
    public void ResetBeans()
    {
        isMoving = false;
        BroadcastMessage("ResetBean", SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Llamado cuando un bean muere (para manejar la l�gica de fallo)
    /// </summary>
    private void BeanDead()
    {
        SendMessageUpwards("LevelFailed", SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Corrutina principal que procesa las listas de instrucciones de cada bean
    /// </summary>
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
            // Si alg�n bean muere, se notifica el fallo y se detiene
            if (greenSc.isDead || redSc.isDead || purpleSc.isDead)
            {
                BeanDead();
                break;
            }
            // Si ya se ejecutaron todas las instrucciones de cada bean, se termina la ejecuci�n
            else if (greenCounter == instructionGreen.Count &&
                     redCounter == instructionRed.Count &&
                     purpleCounter == instructionPurple.Count)
            {
                break;
            }


            // Bean Verde
            if (greenCounter < instructionGreen.Count && !greenSc.isWaiting)
            {
                Instruction currentInstruction = instructionGreen[greenCounter];
                greenSc.GetInstruction(currentInstruction);
                greenCounter++;
            }

            // Bean Rojo
            if (redCounter < instructionRed.Count && !redSc.isWaiting)
            {
                Instruction currentInstruction = instructionRed[redCounter];
                redSc.GetInstruction(currentInstruction);
                redCounter++;
            }

            // Bean P�rpura
            if (purpleCounter < instructionPurple.Count && !purpleSc.isWaiting)
            {
                Instruction currentInstruction = instructionPurple[purpleCounter];
                purpleSc.GetInstruction(currentInstruction);
                purpleCounter++;
            }

            // Espera 1 segundo antes de procesar la siguiente instrucci�n
            yield return new WaitForSeconds(1f);
        }


        yield return null;
    }

    public void HidePanels()
    {

    }
}

using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    // Recorre todos los bloques hijos y extrae la instrucción de cada uno
    public List<Instruction> GetInstructions()
    {
        List<Instruction> instructions = new List<Instruction>();
        foreach (Transform child in transform)
        {
            InstructionHolder holder = child.GetComponent<InstructionHolder>();
            if (holder != null)
            {
                instructions.Add(holder.instruction);
            }
        }

        return instructions;
    }
}

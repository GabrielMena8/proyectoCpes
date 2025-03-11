using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Instruction
{
    public InstructionType.Instruction instruction;
    public int number;

    public Instruction(InstructionType.Instruction instruction, int number)
    {
        this.instruction = instruction;
        this.number = number;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeanMovementSc : MonoBehaviour
{
    public Vector3 gridPos;
    public BeanType.Color color;
    private Rigidbody rb;

    public bool isWaiting;
    public bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetBean()
    {
        isWaiting = false;
        isDead = false;
        rb.isKinematic = true;

        transform.localPosition = Vector3.zero;
    }

    public void GetInstruction(InstructionType.Instruction instruction)
    {
        switch (instruction)
        {
            case InstructionType.Instruction.STEP:
                Step();
                break;
            case InstructionType.Instruction.ROTRIGHT:
                RotateRight();
                break;
            case InstructionType.Instruction.ROTLEFT:
                RotateLeft();
                break;
            case InstructionType.Instruction.PUNCH:
                Punch();
                break;
            default:
                break;
        }
    }

    public void GetTeleported(Vector3 destination)
    {

    }

    private void Step()
    {
        transform.position += transform.forward * 2;
        //print(gameObject.name + "STEP");
    }

    private void RotateRight()
    {
        transform.Rotate(0, 90, 0);
        //print(gameObject.name + "ROTATE RIGHT");
    }

    private void RotateLeft()
    {
        transform.Rotate(0, -90, 0);
        //print(gameObject.name + "ROTATE LEFT");
    }

    private void Punch()
    {
        //print(gameObject.name + "PUNCH");
    }

    private void Teleport()
    {

    }

    public void Die()
    {
        isDead = true;
        rb.isKinematic = false;
    }

    //IEnumerator Step()
    //{
    //    yield return null;
    //}
    //IEnumerator RotateRight()
    //{
    //    yield return null;
    //}
    //IEnumerator RotateLeft()
    //{
    //    yield return null;
    //}
    //IEnumerator Hit()
    //{
    //    yield return null;
    //}
}

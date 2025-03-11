using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeanMovementSc : MonoBehaviour
{
    RaycastHit hit;
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
        transform.rotation = Quaternion.identity;
    }

    public void GetInstruction(InstructionType.Instruction instruction)
    {
        if (isDead) return;

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

        LayerMask mask = LayerMask.GetMask("Platform");
        if (!Physics.Raycast(transform.position, Vector3.down, 10, mask))
        {
            Die();
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bean"))
        {
            Die();
        }
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

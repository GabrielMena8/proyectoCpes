using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationSc : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        float hAxis = -Input.GetAxis("Horizontal");

        transform.Rotate(0, hAxis * rotationSpeed * Time.deltaTime, 0);
    }
}

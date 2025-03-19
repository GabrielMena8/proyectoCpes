using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, offset + player.transform.position, 5 * Time.deltaTime);
    }

}

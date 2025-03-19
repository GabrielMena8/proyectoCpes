using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource gameOveSound;
    public AudioSource coinSound;
    public TextMeshProUGUI coinText;

    public int stepLenght;
    private int coins;
    void Update()
    {
        Vector3 nextPos = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.A))
        {
            nextPos = Vector3.left * stepLenght;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextPos = Vector3.right * stepLenght;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextPos = Vector3.forward * stepLenght;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            nextPos = Vector3.back * stepLenght;
        }

        if (Input.anyKeyDown)
        {
            LayerMask treesLayer = LayerMask.GetMask("Trees");
            RaycastHit hit;

            if(Physics.Raycast(transform.position, nextPos.normalized, out hit, 2, treesLayer) == false)
            {
                transform.LookAt(transform.position + nextPos);
                transform.position += nextPos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            gameOveSound.Play();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            coinSound.Play();
            coins += 1;
            coinText.text = "COINS: " + coins;
            Destroy(other.gameObject);
        }
    }
}

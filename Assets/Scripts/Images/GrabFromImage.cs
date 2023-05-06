using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFromImage : MonoBehaviour
{
    public GameObject obje;
    public GameObject[] destroy;
    public GameObject player;
    Vector3 playerPos;
    Vector3 playerDirection;
    float spawnDistance = 10;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Mouse0))
        {
            playerPos = player.transform.position + player.transform.forward * spawnDistance;

            Instantiate(obje, playerPos, Quaternion.identity);
            for (int i = 0; i < destroy.Length; i++)
            {
                Destroy(destroy[i]);
            }
        }
    }
}


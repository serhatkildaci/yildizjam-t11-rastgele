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
    public float spawnDistance = 10;
    private Camera mainCamera;
    private bool objeCreated = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private bool playerInsideCollider = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideCollider = false;
        }
    }

    private void Update()
    {
        if (playerInsideCollider && Input.GetMouseButtonDown(0) && !objeCreated)
        {
            AudioManager.instance.Play("Grab");
            Debug.Log("Mouse click");
            playerPos = player.transform.position + player.transform.forward * spawnDistance;

            Instantiate(obje, playerPos, Quaternion.identity);
            objeCreated = true;

            for (int i = 0; i < destroy.Length; i++)
            {
                Destroy(destroy[i]);
            }
        }
    }
}

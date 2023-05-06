using System.Collections;
using UnityEngine;

public class noteKill : MonoBehaviour
{
    public float shakeDuration = 0.5f; // duration of camera shake
    public float shakeIntensity = 0.1f; // intensity of camera shake
    public AudioClip soundClip; // sound to play on trigger
    public float soundDelay = 1.0f; // delay before playing sound
    public GameObject blockToDelete; // block to delete on trigger
    public float deleteDelay = 2.0f; // delay before deleting block
    public GameObject noteToDelete;
    private bool triggered = false; // flag to prevent multiple triggers
    private float triggerTime = 0.0f; // time of trigger

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            MeshRenderer meshRenderer = noteToDelete.GetComponent<MeshRenderer>();
    MeshFilter meshFilter = noteToDelete.GetComponent<MeshFilter>();
    Destroy(meshRenderer);
    Destroy(meshFilter);


            triggered = true;
            triggerTime = Time.time;
        }
    }

    private void Update()
    {
        if (triggered && Time.time >= triggerTime + 3.0f)
        {
            triggered = false;

            // shake camera
            StartCoroutine(ShakeCamera());

            // play sound after delay
            Invoke("PlaySound", soundDelay);

            // delete block after delay
            Invoke("DeleteBlock", deleteDelay);
        }
    }

    private IEnumerator ShakeCamera()
    {
        Vector3 originalPosition = Camera.main.transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            Camera.main.transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPosition;
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(soundClip, transform.position);
    }

    private void DeleteBlock()
    {
        Destroy(blockToDelete);
    }

 
}

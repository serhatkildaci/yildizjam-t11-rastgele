using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    public string sceneName; // the name of the scene to load

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

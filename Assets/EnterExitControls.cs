using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterExitControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc key pressed");
            SceneManager.LoadScene("StartEkranı");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed");
            SceneManager.LoadScene("Serhat");
        }
    }
}

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
        Cursor.visible = true;  // make the cursor visible
        Cursor.lockState = CursorLockMode.None; // unlock the cursor so it can move freely
    }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed");
            SceneManager.LoadScene("Serhat");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEkrani : MonoBehaviour
{ 
    public void Play()
    {
        SceneManager.LoadScene("Serhat");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Oyuncu oyundan cikti");
    }
}

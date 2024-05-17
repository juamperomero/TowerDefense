using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Niveles(){
        SceneManager.LoadScene("MenuNiveles");
    }

    public void Salir(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

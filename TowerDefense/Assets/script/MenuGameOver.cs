using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre){
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }

    public void Salir(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

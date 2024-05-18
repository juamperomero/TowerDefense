using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSiguienteNivel : MonoBehaviour
{
    public GameObject nextLevelPanel;
    
    public void ShowNextLevelPanel()
    {
        nextLevelPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
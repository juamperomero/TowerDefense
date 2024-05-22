using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSiguienteNivel : MonoBehaviour
{
    public GameObject nextLevelPanel;
    public int[] escenasConMuñeca = { 1, 6, 10, 14 }; // Números de índice de las escenas con la muñeca hablando

    public void ShowNextLevelPanel()
    {
        nextLevelPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (System.Array.Exists(escenasConMuñeca, element => element == nextSceneIndex))
        {
            SceneManager.LoadScene(nextSceneIndex); // Carga la escena con la muñeca hablando
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex); // Carga el siguiente nivel normal
        }
    }

    public void MenuInicial(string nombre)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}

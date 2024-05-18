using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionNivel : MonoBehaviour
{
    private const string NivelesDesbloqueadosKey = "NivelesDesbloqueados";
    public Button[] levelButtons;  // Array de botones de niveles en la escena

    void Start()
    {
        // Si no existe la clave, inicialízala con el primer nivel desbloqueado
        if (!PlayerPrefs.HasKey(NivelesDesbloqueadosKey))
        {
            Debug.Log("Inicializando NivelesDesbloqueados en PlayerPrefs.");
            PlayerPrefs.SetInt(NivelesDesbloqueadosKey, 1);
        }

        // Llama a la función para actualizar los botones de niveles
        UpdateLevelButtons();
    }

    public void CambiarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void CambiarNivel(int numNivel)
    {
        int nivelesDesbloqueados = PlayerPrefs.GetInt(NivelesDesbloqueadosKey);
        if (numNivel <= nivelesDesbloqueados)
        {
            SceneManager.LoadScene(numNivel);
        }
        else
        {
            Debug.Log("Nivel bloqueado.");
        }
    }

    public void MenuInicial()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void NivelCompletado(int nivel)
    {
        int nivelesDesbloqueados = PlayerPrefs.GetInt(NivelesDesbloqueadosKey);
        if (nivel >= nivelesDesbloqueados)
        {
            PlayerPrefs.SetInt(NivelesDesbloqueadosKey, nivel + 1);
            Debug.Log("Nivel completado: " + nivel);
        }
    }

    private void UpdateLevelButtons()
    {
        if (levelButtons == null || levelButtons.Length == 0)
        {
            return;
        }

        int nivelesDesbloqueados = PlayerPrefs.GetInt(NivelesDesbloqueadosKey);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= nivelesDesbloqueados)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
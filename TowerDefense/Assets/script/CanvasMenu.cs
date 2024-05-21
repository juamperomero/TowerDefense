using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public Button buttonMenuNiveles; // Botón para ir al "MenuNiveles"
    public Button buttonSalir;       // Botón para salir del juego

    private void Start()
    {
        // Verificar si los botones están asignados en el Inspector
        if (buttonMenuNiveles == null || buttonSalir == null)
        {
            Debug.LogError("Por favor, asigna todos los botones en el Inspector.");
            return;
        }

        // Agregar listeners a los botones
        buttonMenuNiveles.onClick.AddListener(IrAMenuNiveles);
        buttonSalir.onClick.AddListener(SalirDelJuego);
    }

    // Método para cargar la escena "MenuNiveles"
    private void IrAMenuNiveles()
    {
        SceneManager.LoadScene("MenuNiveles");
    }

    // Método para salir del juego
    private void SalirDelJuego()
    {
        Application.Quit();
        // Esto solo es necesario para cuando estás probando el juego en el editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
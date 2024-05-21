using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MuñecaHablando : MonoBehaviour
{
    public float tiempoHablando = 10f; // Duración de la animación de hablar
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>(); // Obtener el componente Animator
        if (anim == null)
        {
            Debug.LogError("No se encontró el componente Animator en la muñeca.");
            return;
        }

        // Iniciar la animación de hablar
        anim.SetTrigger("Hablar");
        Invoke("CargarSiguienteEscena", tiempoHablando); // Llama al método CargarSiguienteEscena después de tiempoHablando segundos
    }

    private void Update()
    {
        // Verificar si el jugador ha interactuado para cancelar el temporizador
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke("CargarSiguienteEscena");
            CargarSiguienteEscena(); // Cargar inmediatamente el siguiente nivel
        }
    }
    private void GoToLast()
    {
        SceneManager.LoadScene("TheLast");
    }
    private void CargarSiguienteEscena()
    {
        if(SceneManager.GetActiveScene().name == "ULip Final")  GoToLast();
        else{   
            int siguienteIndiceEscena = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(siguienteIndiceEscena);
        }
    }
}


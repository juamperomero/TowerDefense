using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public List<WaveObject> waves = new List<WaveObject>();
    public bool isWaitingForNextWave;
    public bool wavesFinish;
    public int currentWave;
    public Transform initPosition;

    public TextMeshProUGUI counterText;
    public GameObject buttonNextWave;
    public GameObject nextLevelCanvas;  // Referencia al Canvas del siguiente nivel
    private SeleccionNivel seleccionNivel;  // Referencia al script de selección de nivel

    private List<Enemy> currentEnemies = new List<Enemy>();

    void Start()
    {
        seleccionNivel = FindObjectOfType<SeleccionNivel>();  // Buscar y asignar la referencia del script de selección de nivel
        if (seleccionNivel == null)
        {
            Debug.LogError("No se pudo encontrar el script SeleccionNivel. Asegúrate de que está en la escena.");
        }
        StartCoroutine(ProcesWave());
    }

    void Update()
    {
        CheckCounterAndShowButton();
        checkCounterForNextWave();
        CheckAllEnemiesDefeated();
    }

    private void checkCounterForNextWave()
    {
        if (isWaitingForNextWave && !wavesFinish)
        {
            waves[currentWave].counterToNextWave -= 1 * Time.deltaTime;
            counterText.text = waves[currentWave].counterToNextWave.ToString("00");
            if (waves[currentWave].counterToNextWave <= 0)
            {
                ChangeWave();
                Debug.Log("Set Next Wave");
            }
        }
    }

    public void ChangeWave()
    {
        if (wavesFinish)
            return;
        currentWave++;
        StartCoroutine(ProcesWave());
    }

    private IEnumerator ProcesWave()
    {
        if (wavesFinish)
        {
            yield break;
        }
        isWaitingForNextWave = false;
        waves[currentWave].counterToNextWave = waves[currentWave].timeForNextWave;
        for (int i = 0; i < waves[currentWave].enemys.Count; i++)
        {
            var enemyGo = Instantiate(waves[currentWave].enemys[i], initPosition.position, initPosition.rotation);
            currentEnemies.Add(enemyGo.GetComponent<Enemy>());  // Añadir el enemigo a la lista
            yield return new WaitForSeconds(waves[currentWave].timerPerCreation);
        }
        isWaitingForNextWave = true;
        if (currentWave >= waves.Count - 1)
        {
            Debug.Log("Nivel terminado");
            wavesFinish = true;
        }
    }

    private void CheckCounterAndShowButton()
    {
        if (!wavesFinish)
        {
            buttonNextWave.SetActive(isWaitingForNextWave);
            counterText.gameObject.SetActive(isWaitingForNextWave);
        }
    }

    private void CheckAllEnemiesDefeated()
    {
        // Verifica si todas las oleadas han terminado y no hay enemigos en la lista
        if (wavesFinish && currentEnemies.Count == 0)
        {
            // Marca el nivel como completado
            int nivelActual = SceneManager.GetActiveScene().buildIndex;
            if (seleccionNivel != null)
            {
                seleccionNivel.NivelCompletado(nivelActual);
            }
            else
            {
                Debug.LogError("SeleccionNivel es nulo.");
            }

            // Muestra el panel para saltar al siguiente nivel
            ShowNextLevelPanel();
        }
    }

    private void ShowNextLevelPanel()
{
    if (nextLevelCanvas != null)
    {
        nextLevelCanvas.SetActive(true);
    }
    else
    {
        Debug.LogError("El canvas de próximo nivel no está asignado en el Inspector.");
    }
}


    public void EnemyDefeated(Enemy enemy)
    {
        if (currentEnemies.Contains(enemy))
        {
            currentEnemies.Remove(enemy);
        }
    }
}
[System.Serializable]
public class WaveObject
{
    public float timerPerCreation = 1;
    public float timeForNextWave = 10;
    [HideInInspector] public float counterToNextWave = 0;
    public List<Enemy> enemys = new List<Enemy>();
}
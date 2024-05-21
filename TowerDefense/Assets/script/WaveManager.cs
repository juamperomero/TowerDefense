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
    public GameObject nextLevelCanvas;

    private List<Enemy> currentEnemies = new List<Enemy>();

    void Start()
    {
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
            currentEnemies.Add(enemyGo);
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
        if (wavesFinish && currentEnemies.Count == 0)
        {
            if(SceneManager.GetActiveScene().name == "Nivel10")    GoToFinal();
            else    ShowNextLevelPanel();
        }
    }

    private void ShowNextLevelPanel()
    {
        nextLevelCanvas.SetActive(true);
        FindObjectOfType<SeleccionNivel>().NivelCompletado(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToFinal()
    {
        SceneManager.LoadScene("ULip Final");
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
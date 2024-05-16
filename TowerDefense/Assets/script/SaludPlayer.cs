using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludPlayer : MonoBehaviour
{
    public float health = 1000;
    public float maxHealth = 1000;

    [Header("Interfaz")]
    public Image healthBar;
    public Text healthText;

    void Update()
    {
        UpdateInterface();
    }

    void UpdateInterface()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "Vida : " + health.ToString("f0");
    }

    public void TakeDmg(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            OnBaseDestroyed();
        }
    }

    private void OnBaseDestroyed()
    {
        Debug.Log("La base ha sido destruida.");
        // Implementar lógica adicional aquí, como terminar el juego o mostrar un mensaje.
    }
}

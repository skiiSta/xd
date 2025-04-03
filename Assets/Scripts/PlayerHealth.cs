using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead;

    private UIManager uiManager;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;

        // Obtener la referencia al UIManager
        uiManager = FindObjectOfType<UIManager>();
        
        // Actualizamos la barra de vida al iniciar
        if (uiManager != null)
        {
            uiManager.SetVida(currentHealth); // Asegúrate de que esto actualiza la UI
        }
    }

    private void Update()
    {
        // Revisamos si la salud del jugador es 0 o menos y si no está muerto
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    // Método para reducir la vida del jugador
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Asegurarnos que la salud no pase de 0
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Actualizamos la barra de vida en la UI
        if (uiManager != null)
        {
            uiManager.SetVida(currentHealth); // Actualiza la barra de vida
        }

        Debug.Log("Health: " + currentHealth);
    }

    // Método para reducir la vida cuando el oxígeno se acaba
    public void DecreaseHealth(float amount)
    {
        TakeDamage(amount);
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");
        // Aquí podrías activar animaciones de muerte, destruir el personaje, etc.
    }
}

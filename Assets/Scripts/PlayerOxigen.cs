using UnityEngine;

public class PlayerOxigen : MonoBehaviour
{
    // Variables de oxígeno
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDecayRate = 5f;  // Cuánto oxígeno pierde el jugador por segundo
    public float oxygenRechargeRate = 10f;  // Cuánto oxígeno se recarga por segundo si el jugador no está gastando oxígeno

    private bool isInteractingWithOxygenSource = false;  // Flag para saber si el jugador está interactuando con una fuente de oxígeno

    // Referencia al PlayerHealth (ya que lo tenemos en otro script)
    private PlayerHealth playerHealth;

    private void Start()
    {
        // Inicialización de oxígeno
        currentOxygen = maxOxygen;

        // Obtenemos la referencia al PlayerHealth
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Si el oxígeno es mayor que 0, reducimos el oxígeno con el tiempo
        if (currentOxygen > 0)
        {
            DecreaseOxygen(oxygenDecayRate * Time.deltaTime);
        }
        else
        {
            // Si el oxígeno llega a 0, la vida comienza a decrecer
            DecreaseHealthDueToLackOfOxygen();
        }

        // Solo recargar oxígeno si está interactuando con una fuente de oxígeno
        if (isInteractingWithOxygenSource && currentOxygen < maxOxygen)
        {
            RechargeOxygen(oxygenRechargeRate * Time.deltaTime);
        }

        // Aseguramos que el oxígeno no exceda los límites
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);
    }

    // Disminuir el oxígeno con el tiempo
    private void DecreaseOxygen(float amount)
    {
        currentOxygen -= amount;
    }

    // Disminuir la salud del jugador cuando el oxígeno se acaba
    private void DecreaseHealthDueToLackOfOxygen()
    {
        if (playerHealth != null)
        {
            // Disminuimos la vida por la falta de oxígeno
            playerHealth.DecreaseHealth(1f * Time.deltaTime);  // Ajusta la velocidad de la pérdida de salud según se necesite
        }
    }

    // Recargar oxígeno cuando el jugador está interactuando con una fuente de oxígeno
    private void RechargeOxygen(float amount)
    {
        currentOxygen += amount;
    }

    // Método público para obtener el oxígeno actual (opcional)
    public float GetCurrentOxygen()
    {
        return currentOxygen;
    }

    // Método para interactuar con la fuente de oxígeno (por ejemplo, al acercarse a un objeto)
    public void InteractWithOxygenSource()
    {
        isInteractingWithOxygenSource = true;
        currentOxygen = maxOxygen; // Recargamos el oxígeno al máximo
        Debug.Log("Oxígeno recargado al máximo.");
    }

    // Método para finalizar la interacción con la fuente de oxígeno
    public void StopInteractingWithOxygenSource()
    {
        isInteractingWithOxygenSource = false;
    }
}

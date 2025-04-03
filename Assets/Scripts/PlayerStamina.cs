using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRechargeRate = 5f;  // Velocidad de recarga de la stamina
    public float staminaDepletionRate = 10f;  // Tasa de agotamiento de stamina por segundo
    public float staminaDepletionMultiplier = 1f;  // Factor para hacer el agotamiento más gradual (ajustar según se desee)
    
    private float cooldownTime = 2f;  // Tiempo de cooldown cuando la stamina llega a 0
    private float cooldownTimer = 0f;  // Temporizador para el cooldown
    private bool isOnCooldown = false;  // Flag para saber si está en cooldown
    private UIManager uiManager;

    private void Start()
    {
        currentStamina = maxStamina;

        // Obtener referencia al UIManager para actualizar la barra de stamina
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        // Si no estamos en cooldown, tratamos de correr
        if (!isOnCooldown)
        {
            if (Input.GetKey(KeyCode.LeftShift))  // Si estamos corriendo
            {
                // Agotamos la stamina lentamente
                DecreaseStamina(staminaDepletionRate * Time.deltaTime * staminaDepletionMultiplier);
            }
            else
            {
                // Recargamos la stamina cuando no estamos corriendo
                RechargeStamina();
            }
        }
        else
        {
            // Si estamos en cooldown, la stamina no se agota pero se recarga
            RechargeStamina();

            // Reducimos el temporizador de cooldown
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                // Se terminó el cooldown, permitimos correr nuevamente
                isOnCooldown = false;
                cooldownTimer = 0f;  // Resetear el timer
                Debug.Log("Cooldown terminado. Puedes correr nuevamente.");
            }
        }

        // Si la stamina llega a 0 y no estamos en cooldown, activar el cooldown
        if (currentStamina <= 0f && !isOnCooldown)
        {
            EnterCooldown();
        }

        // Actualizar la barra de stamina en la UI
        if (uiManager != null)
        {
            uiManager.SetStamina(currentStamina);
        }
    }

    public bool CanRun() => currentStamina > 0 && !isOnCooldown;

    public void DecreaseStamina(float amount)
    {
        currentStamina = Mathf.Max(currentStamina - amount, 0);  // Evita que la stamina sea negativa
    }

    private void RechargeStamina()
    {
        // Solo recarga si la stamina no está completamente llena
        currentStamina = Mathf.Min(currentStamina + staminaRechargeRate * Time.deltaTime, maxStamina);
    }

    private void EnterCooldown()
    {
        isOnCooldown = true;  // Activar el cooldown
        cooldownTimer = cooldownTime;  // Iniciar el temporizador de cooldown
        Debug.Log("Stamina agotada. Cooldown activado.");
    }
}

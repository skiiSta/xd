using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Referencias a los Sliders en la UI
    public Slider vidaSlider;
    public Slider staminaSlider;
    public Slider oxygenSlider;

    private PlayerHealth playerHealth;
    private PlayerOxygen playerOxygen;
    private PlayerStamina playerStamina;

    private void Start()
    {
        // Obtener las referencias a los scripts del jugador
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerOxygen = FindObjectOfType<PlayerOxygen>();
        playerStamina = FindObjectOfType<PlayerStamina>();

        // Inicializar los sliders con los valores actuales
        if (vidaSlider != null && playerHealth != null)
            vidaSlider.value = playerHealth.currentHealth;

        if (staminaSlider != null && playerStamina != null)
            staminaSlider.value = playerStamina.currentStamina; // Asegúrate de que PlayerStamina tiene currentStamina

        if (oxygenSlider != null && playerOxygen != null)
            oxygenSlider.value = playerOxygen.currentOxygen; // Asegúrate de que PlayerOxygen tiene currentOxygen
    }

    // Método para actualizar la barra de vida
    public void SetVida(float newHealth)
    {
        if (vidaSlider != null)
        {
            vidaSlider.value = newHealth; // Actualizamos el Slider de vida
        }
    }

    // Método para actualizar la barra de stamina
    public void SetStamina(float newStamina)
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = newStamina; // Actualizamos el Slider de stamina
        }
    }

    // Método para actualizar la barra de oxígeno
    public void SetOxygen(float newOxygen)
    {
        if (oxygenSlider != null)
        {
            oxygenSlider.value = newOxygen; // Actualizamos el Slider de oxígeno
        }
    }
}

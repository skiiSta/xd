using UnityEngine;

public class OxygenSource : MonoBehaviour
{
    public string playerTag = "Player";  // El tag del jugador
    public float oxygenRechargeAmount = 100f; // Cantidad de oxígeno recargado al máximo

    private void OnCollisionEnter(Collision collision)
    {
        // Comprobamos si el objeto que colisiona es el jugador
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Obtén el componente PlayerOxigen del jugador y recarga el oxígeno
            PlayerOxigen playerOxigen = collision.gameObject.GetComponent<PlayerOxigen>();
            if (playerOxigen != null)
            {
                playerOxigen.InteractWithOxygenSource();
                Debug.Log("Oxígeno recargado al máximo.");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Finalizamos la interacción cuando el jugador deja de estar en contacto con el objeto
        if (collision.gameObject.CompareTag(playerTag))
        {
            PlayerOxigen playerOxigen = collision.gameObject.GetComponent<PlayerOxigen>();
            if (playerOxigen != null)
            {
                playerOxigen.StopInteractingWithOxygenSource();
                Debug.Log("Dejaste de interactuar con la fuente de oxígeno.");
            }
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStamina playerStamina;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStamina = GetComponent<PlayerStamina>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth.isDead) return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && playerStamina.CanRun())
        {
            playerMovement.StartRunning();
        }
        else
        {
            playerMovement.StopRunning();
        }
    }

    public void ApplyDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
    }
}

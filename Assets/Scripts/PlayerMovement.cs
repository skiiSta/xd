using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 7f;
    public float gravity = -9.81f;

    private float currentSpeed;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning;

    private CharacterController characterController;
    private PlayerStamina stamina;
    private CameraController cameraController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        stamina = GetComponent<PlayerStamina>();
        cameraController = GetComponentInChildren<CameraController>();  // Asumiendo que la cámara es hijo del jugador
    }

    private void Update()
    {
        if (cameraController == null) return;  // Asegúrate de que hay un CameraController

        MovePlayer();
        ApplyGravity();
    }

    private void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Obtener la dirección de movimiento horizontal respecto a la cámara
        Vector3 direction = (cameraController.playerBody.right * moveX) + (cameraController.playerBody.forward * moveZ);
        direction.y = 0;  // Asegurarse de que no haya movimiento vertical

        if (direction.magnitude > 1f)  // Normalizar la dirección si la velocidad es mayor que 1
        {
            direction.Normalize();
        }

        // Determina la velocidad según si está corriendo o caminando
        currentSpeed = isRunning && stamina.CanRun() ? runSpeed : walkSpeed;

        // Mover al jugador
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        // Saltar
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void StartRunning() => isRunning = true;
    public void StopRunning() => isRunning = false;
}

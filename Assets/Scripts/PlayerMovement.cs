using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 1.5f;  // Velocidad cuando está agachado
    public float jumpForce = 7f;
    public float gravity = -9.81f;
    public float crouchHeight = 0.5f;  // Altura cuando está agachado
    public float standHeight = 2f;     // Altura normal
    public float crouchYOffset = -0.75f;  // Ajuste de la posición Y del mesh al agacharse

    private float currentSpeed;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning;
    private bool isCrouching;  // Nuevo estado para saber si está agachado

    private CharacterController characterController;
    private PlayerStamina stamina;
    private CameraController cameraController;
    private Transform playerMesh;  // Referencia al mesh del jugador

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        stamina = GetComponent<PlayerStamina>();
        cameraController = GetComponentInChildren<CameraController>();  // Asumiendo que la cámara es hijo del jugador
        playerMesh = transform.Find("PlayerMesh");  // Encuentra el mesh del jugador, suponiendo que se llama "PlayerMesh"
    }

    private void Update()
    {
        if (cameraController == null) return;  // Asegúrate de que hay un CameraController

        MovePlayer();
        ApplyGravity();
        HandleCrouch();  // Llamamos a la función que maneja el agachado
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

        // Determina la velocidad según si está corriendo, caminando o agachado
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;  // Si está agachado, usar la velocidad de agachado
        }
        else
        {
            currentSpeed = isRunning && stamina.CanRun() ? runSpeed : walkSpeed;  // Si no está agachado, usa las velocidades normales
        }

        // Mover al jugador
        characterController.Move(direction * currentSpeed * Time.deltaTime);

        // Saltar
        if (isGrounded && Input.GetButtonDown("Jump") && !isCrouching)  // No se puede saltar mientras está agachado
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

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))  // Cambia "C" por cualquier tecla que desees usar
        {
            if (isCrouching)
            {
                StandUp();  // Si ya está agachado, se levanta
            }
            else
            {
                CrouchDown();  // Si no está agachado, se agacha
            }
        }
    }

    private void CrouchDown()
    {
        isCrouching = true;
        characterController.height = crouchHeight;  // Reducir la altura del CharacterController
        characterController.center = new Vector3(0, crouchHeight / 2, 0);  // Ajustar el centro del CharacterController
        playerMesh.localPosition = new Vector3(playerMesh.localPosition.x, crouchYOffset, playerMesh.localPosition.z);  // Mover el mesh para que coincida con la altura
    }

    private void StandUp()
    {
        isCrouching = false;
        characterController.height = standHeight;  // Restaurar la altura original del CharacterController
        characterController.center = new Vector3(0, standHeight / 2, 0);  // Ajustar el centro del CharacterController
        playerMesh.localPosition = new Vector3(playerMesh.localPosition.x, 0, playerMesh.localPosition.z);  // Restaurar la posición original del mesh
    }

    public void StartRunning() => isRunning = true;
    public void StopRunning() => isRunning = false;
}

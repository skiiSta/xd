using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // El jugador a seguir
    public float mouseSensitivity = 100f;  // Sensibilidad del ratón
    public float verticalClamp = 80f;  // Límite para la rotación vertical de la cámara
    public Transform playerBody;  // El cuerpo del jugador para girar

    private float xRotation = 0f;

    private void Start()
    {
        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Obtener el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal de la cámara
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotación vertical (clamp para evitar que gire demasiado hacia arriba o hacia abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        // Aplicar la rotación vertical a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}

using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Прячем курсор и фиксируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Считываем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Ограничиваем вертикальный поворот
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Вращаем камеру вверх-вниз
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращаем игрока влево-вправо
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

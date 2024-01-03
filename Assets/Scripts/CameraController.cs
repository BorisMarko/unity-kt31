using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f; // Чувствительность мыши
    [SerializeField] private float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали

    private float rotationX = 0.0f;

    private void Update()
    {
        HandleMouseInput();
    }
    private void HandleMouseInput()
    {
        // Получаем ввод от мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        RotateHorizontally(mouseX);
        RotateVertically(mouseY);
    }
    private void RotateHorizontally(float mouseX)
    {
        // Вращаем камеру по горизонтальной плоскости
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);
    }
    private void RotateVertically(float mouseY)
    {
        // Вращаем камеру в вертикальной плоскости
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}

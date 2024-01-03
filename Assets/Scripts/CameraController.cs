using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f; // ���������������� ����
    [SerializeField] private float maxYAngle = 80.0f; // ������������ ���� �������� �� ���������

    private float rotationX = 0.0f;

    private void Update()
    {
        HandleMouseInput();
    }
    private void HandleMouseInput()
    {
        // �������� ���� �� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        RotateHorizontally(mouseX);
        RotateVertically(mouseY);
    }
    private void RotateHorizontally(float mouseX)
    {
        // ������� ������ �� �������������� ���������
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);
    }
    private void RotateVertically(float mouseY)
    {
        // ������� ������ � ������������ ���������
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}

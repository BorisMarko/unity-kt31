using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float sensitivy = 2.0f; //���������������� ����
    public float maxYAngle = 80.0f; //������������ ���� �������� �� ���������

    private float rotationX = 0.0f;

    private void Update()
    {
        //�������� ���� �� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //������� ������ �� �������������� ���������
        transform.parent.Rotate(Vector3.up * mouseX * sensitivy);

        //������� ������ � ������������ ���������
        rotationX -= mouseY * sensitivy;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}
using UnityEngine;

public class MotionFreeze : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraObject;
    private bool isUsed = false;

    void Update()
    {
        // ���������, ������ �� ������� ��� �������������
        if (Input.GetKeyDown(KeyCode.E) && !isUsed)
        {
            // �������� ����� ��� ��������� �������� ��������� � ������
            FreezeMovement(true);
            isUsed = true;
        }

        // ���������, ������ �� ������� Q
        if (Input.GetKeyDown(KeyCode.Q) && isUsed)
        {
            // �������� ����� ��� �������������� �������� ��������� � ������
            FreezeMovement(false);
            isUsed = false;
        }
    }

    // ����� ��� ��������� ��� �������������� �������� ��������� � ������
    private void FreezeMovement(bool freeze)
    {
        // ���� ��������� CharacterController ������������ � ���������
        if (player != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                // ������������ ��� ������������� �������� ���������
                characterController.enabled = !freeze;
            }
        }

        // ���� ��������� CameraController ������������ � ������ � � ��� ������ ���
        if (cameraObject != null && cameraObject.CompareTag("Camera"))
        {
            CameraController cameraController = cameraObject.GetComponent<CameraController>();
            if (cameraController != null)
            {
                // ������������ ��� ������������� �������� ������
                cameraController.enabled = !freeze;
            }
        }
    }
}
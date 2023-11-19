using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform driverSeat;
    public Transform exitPoint;
    public float interactionDistance = 2f;
    Rigidbody rb;
    private bool isPlayerInside = false;
    private GameObject player;
    private Quaternion playerInitialRotation;

    public bool IsPlayerInside()
    {
        return isPlayerInside;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlayerInside)
            {
                TryEnterCar();
            }
            else
            {
                TryExitCar();
            }
        }

        if (isPlayerInside)
        {
            // ��� ��� ���������� �������

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // ������ ���������� �������
            Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
            float moveSpeed = 10.0f; // ���� �������� ��������

            // ���������� ��������
            rb.velocity = transform.TransformDirection(moveDirection) * moveSpeed;

            // ���������� ��������
            float rotationSpeed = 100.0f; // ���� �������� ��������
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }


    void TryEnterCar()
    {
        Collider[] colliders = Physics.OverlapSphere(driverSeat.position, interactionDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.gameObject;

                // ���������� ��������� ���������� ������
                playerInitialRotation = player.transform.localRotation;

                // ����� ��������� ����� � ������������ ������
                player.transform.parent = driverSeat; // ������� ������ �������� �������� ������������� �����
                player.transform.localPosition = Vector3.zero; // �������� ��������� ����������
                player.transform.localRotation = Quaternion.identity; // �������� ��������� ����������
                isPlayerInside = true;

                // ��������� CharacterController, ����� ����� �� �������� ��� �� ����
                player.GetComponent<CharacterController>().enabled = false;

                return;
            }
        }
    }

    void TryExitCar()
    {
        Collider[] colliders = Physics.OverlapSphere(exitPoint.position, interactionDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.gameObject;

                // ����� ��������� ����� � ������ ������
                player.transform.parent = null; // ��������� ������ �� ��������
                player.transform.position = exitPoint.position;

                // �������������� ��������� ���������� ������
                player.transform.localRotation = playerInitialRotation;

                // �������� CharacterController ����� ������ �� ������
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = true;
                }
                else
                {
                    Debug.LogError("CharacterController �� ������ �� ������.");
                }

                isPlayerInside = false;
                return;
            }
        }
    }
}

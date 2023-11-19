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
            // Ваш код управления машиной

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Пример управления машиной
            Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
            float moveSpeed = 10.0f; // Ваша скорость движения

            // Применение движения
            rb.velocity = transform.TransformDirection(moveDirection) * moveSpeed;

            // Применение поворота
            float rotationSpeed = 100.0f; // Ваша скорость поворота
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

                // Сохранение начальной ориентации игрока
                playerInitialRotation = player.transform.localRotation;

                // Игрок находится рядом с водительским местом
                player.transform.parent = driverSeat; // Сделать игрока дочерним объектом водительского места
                player.transform.localPosition = Vector3.zero; // Обнулить локальные координаты
                player.transform.localRotation = Quaternion.identity; // Обнулить локальную ориентацию
                isPlayerInside = true;

                // Отключить CharacterController, чтобы игрок не двигался сам по себе
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

                // Игрок находится рядом с точкой выхода
                player.transform.parent = null; // Открепить игрока от родителя
                player.transform.position = exitPoint.position;

                // Восстановление начальной ориентации игрока
                player.transform.localRotation = playerInitialRotation;

                // Включить CharacterController после выхода из машины
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = true;
                }
                else
                {
                    Debug.LogError("CharacterController не найден на игроке.");
                }

                isPlayerInside = false;
                return;
            }
        }
    }
}

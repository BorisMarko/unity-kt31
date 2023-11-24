using UnityEngine;

public class MotionFreeze : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraObject;
    private bool isUsed = false;

    void Update()
    {
        // Проверяем, нажата ли клавиша для использования
        if (Input.GetKeyDown(KeyCode.E) && !isUsed)
        {
            // Вызываем метод для заморозки движения персонажа и камеры
            FreezeMovement(true);
            isUsed = true;
        }

        // Проверяем, нажата ли клавиша Q
        if (Input.GetKeyDown(KeyCode.Q) && isUsed)
        {
            // Вызываем метод для размораживания движения персонажа и камеры
            FreezeMovement(false);
            isUsed = false;
        }
    }

    // Метод для заморозки или размораживания движения персонажа и камеры
    private void FreezeMovement(bool freeze)
    {
        // Если компонент CharacterController присутствует у персонажа
        if (player != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                // Замораживаем или размораживаем движение персонажа
                characterController.enabled = !freeze;
            }
        }

        // Если компонент CameraController присутствует у камеры и у нее нужный тэг
        if (cameraObject != null && cameraObject.CompareTag("Camera"))
        {
            CameraController cameraController = cameraObject.GetComponent<CameraController>();
            if (cameraController != null)
            {
                // Замораживаем или размораживаем движение камеры
                cameraController.enabled = !freeze;
            }
        }
    }
}
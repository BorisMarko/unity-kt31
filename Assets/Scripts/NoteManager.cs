using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteManager : MonoBehaviour
{
    private List<Note> notes = new List<Note>();
    private bool isUsed = false;
    private bool isInTriggerZone = false; // Флаг, определяющий, находится ли игрок в зоне триггера
    private bool isNoteVisible = false; // Флаг, определяющий, видна ли записка в данный момент

    private GameObject player;
    private GameObject cameraObject;

    [SerializeField]
    private Image _noteImage; // Ссылка на компонент изображения записки

    void Start()
    {
        // Присваиваем player и cameraObject в соответствии с вашей логикой
        // Например:
        player = GameObject.FindWithTag("Player");
        cameraObject = GameObject.FindWithTag("Camera");
    }
    private void Update()
    {
        // Если находится в зоне триггера
        if (isInTriggerZone)
        {
            // Если нажата клавиша E и записка не видна, показываем ее
            if (Input.GetKeyDown(KeyCode.E) && !isNoteVisible)
            {
                isNoteVisible = true;
                UpdateNoteVisibility();
            }
            // Если нажата клавиша Q и записка видна, скрываем ее
            if (Input.GetKeyDown(KeyCode.Q) && isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, находимся ли в триггере
        if (other.isTrigger)
        {
            isInTriggerZone = true; // Игрок вошел в зону триггера
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, находимся ли в триггере
        if (other.isTrigger)
        {
            isInTriggerZone = false; // Игрок вышел из зоны триггера

            // Если записка видна при выходе из триггера, скрываем ее
            if (isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }

    // Метод для заморозки или размораживания движения персонажа и камеры
    private void FreezeMovement(bool freeze)
    {
        if (isInTriggerZone)
        {

            // Если компонент CharacterController присутствует у персонажа
            if (player != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    // Останавливаем движение перед выключением контроллера
                    if (freeze)
                    {
                        characterController.SimpleMove(Vector3.zero);
                    }

                    // Замораживаем или размораживаем движение персонажа
                    characterController.enabled = !freeze;
                }
            }

            // Если компонент CameraController присутствует у камеры
            if (cameraObject != null)
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

    // Метод для проверки наличия записок рядом с игроком
    private void CheckForNotes()
    {
        // Пройтись по всем запискам в коллекции
        foreach (Note note in notes)
        {
            // Проверить расстояние между запиской и игроком
            float distance = Vector3.Distance(note.transform.position, transform.position);

            // Если расстояние меньше заданного значения, выполнить действия, связанные с запиской
            if (distance < 2f)
            {
                // Вызвать метод взаимодействия с запиской
                note.Interact();
            }
        }
    }

    // Метод для обновления видимости записки в соответствии с флагом
    private void UpdateNoteVisibility()
    {
        if (_noteImage != null)
        {
            // Активируем или деактивируем объект записки в зависимости от флага
            _noteImage.enabled = isNoteVisible;
        }
    }
}

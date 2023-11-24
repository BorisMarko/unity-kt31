using System;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage; // Ссылка на компонент изображения, который представляет записку
    private bool isNoteVisible = false; // Флаг, определяющий, видна ли записка в данный момент
    private bool isInTriggerZone = false; // Флаг, определяющий, находится ли игрок в зоне триггера
    private CharacterController characterController; // Ссылка на компонент CharacterController для управления движением персонажа

    void Start()
    {
        // Получаем компонент CharacterController при запуске
        characterController = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true; // Игрок вошел в зону триггера
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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

    void Update()
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

    // Метод для обновления видимости записки в соответствии с флагом
    private void UpdateNoteVisibility()
    {
        _noteImage.enabled = isNoteVisible;
    }

    // Метод для взаимодействия с запиской
    public void Interact()
    {
        // Логика, связанная с запиской при взаимодействии
        // Здесь можно добавить дополнительные действия, связанные с взаимодействием с запиской
    }
}

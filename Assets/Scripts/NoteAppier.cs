using UnityEngine;
using UnityEngine.UI;

public class NoteAppier : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage;

    [SerializeField]
    private Text _openNoteText; // Связать это с вашим объектом Text в инспекторе

    private bool isNoteVisible = false;
    private bool isInTriggerZone = false;

    void Update()
    {
        // Проверяем, находимся ли в зоне триггера
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

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, вошел ли игрок в триггер
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            // Показываем текст при входе в зону триггера
            _openNoteText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Проверяем, вышел ли игрок из триггера
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            // Скрываем текст при выходе из зоны триггера
            _openNoteText.enabled = false;

            // Если записка видна при выходе из триггера, скрываем ее
            if (isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }
}

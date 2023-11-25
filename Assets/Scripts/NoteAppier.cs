using UnityEngine;
using UnityEngine.UI;

public class NoteAppier : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage;

    [SerializeField]
    private Text _openNoteText; // ������� ��� � ����� �������� Text � ����������

    private bool isNoteVisible = false;
    private bool isInTriggerZone = false;

    void Update()
    {
        // ���������, ��������� �� � ���� ��������
        if (isInTriggerZone)
        {
            // ���� ������ ������� E � ������� �� �����, ���������� ��
            if (Input.GetKeyDown(KeyCode.E) && !isNoteVisible)
            {
                isNoteVisible = true;
                UpdateNoteVisibility();
            }

            // ���� ������ ������� Q � ������� �����, �������� ��
            if (Input.GetKeyDown(KeyCode.Q) && isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }

    // ����� ��� ���������� ��������� ������� � ������������ � ������
    private void UpdateNoteVisibility()
    {
        _noteImage.enabled = isNoteVisible;
    }

    void OnTriggerEnter(Collider other)
    {
        // ���������, ����� �� ����� � �������
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true;
            // ���������� ����� ��� ����� � ���� ��������
            _openNoteText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ���������, ����� �� ����� �� ��������
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false;
            // �������� ����� ��� ������ �� ���� ��������
            _openNoteText.enabled = false;

            // ���� ������� ����� ��� ������ �� ��������, �������� ��
            if (isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }
}

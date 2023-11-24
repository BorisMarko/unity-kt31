using System;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage; // ������ �� ��������� �����������, ������� ������������ �������
    private bool isNoteVisible = false; // ����, ������������, ����� �� ������� � ������ ������
    private bool isInTriggerZone = false; // ����, ������������, ��������� �� ����� � ���� ��������
    private CharacterController characterController; // ������ �� ��������� CharacterController ��� ���������� ��������� ���������

    void Start()
    {
        // �������� ��������� CharacterController ��� �������
        characterController = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = true; // ����� ����� � ���� ��������
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTriggerZone = false; // ����� ����� �� ���� ��������

            // ���� ������� ����� ��� ������ �� ��������, �������� ��
            if (isNoteVisible)
            {
                isNoteVisible = false;
                UpdateNoteVisibility();
            }
        }
    }

    void Update()
    {
        // ���� ��������� � ���� ��������
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

    // ����� ��� �������������� � ��������
    public void Interact()
    {
        // ������, ��������� � �������� ��� ��������������
        // ����� ����� �������� �������������� ��������, ��������� � ��������������� � ��������
    }
}

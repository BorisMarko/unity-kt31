using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteManager : MonoBehaviour
{
    private List<Note> notes = new List<Note>();
    private bool isUsed = false;
    private bool isInTriggerZone = false; // ����, ������������, ��������� �� ����� � ���� ��������
    private bool isNoteVisible = false; // ����, ������������, ����� �� ������� � ������ ������

    private GameObject player;
    private GameObject cameraObject;

    [SerializeField]
    private Image _noteImage; // ������ �� ��������� ����������� �������

    void Start()
    {
        // ����������� player � cameraObject � ������������ � ����� �������
        // ��������:
        player = GameObject.FindWithTag("Player");
        cameraObject = GameObject.FindWithTag("Camera");
    }
    private void Update()
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
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��������� �� � ��������
        if (other.isTrigger)
        {
            isInTriggerZone = true; // ����� ����� � ���� ��������
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, ��������� �� � ��������
        if (other.isTrigger)
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

    // ����� ��� ��������� ��� �������������� �������� ��������� � ������
    private void FreezeMovement(bool freeze)
    {
        if (isInTriggerZone)
        {

            // ���� ��������� CharacterController ������������ � ���������
            if (player != null)
            {
                CharacterController characterController = player.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    // ������������� �������� ����� ����������� �����������
                    if (freeze)
                    {
                        characterController.SimpleMove(Vector3.zero);
                    }

                    // ������������ ��� ������������� �������� ���������
                    characterController.enabled = !freeze;
                }
            }

            // ���� ��������� CameraController ������������ � ������
            if (cameraObject != null)
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

    // ����� ��� �������� ������� ������� ����� � �������
    private void CheckForNotes()
    {
        // �������� �� ���� �������� � ���������
        foreach (Note note in notes)
        {
            // ��������� ���������� ����� �������� � �������
            float distance = Vector3.Distance(note.transform.position, transform.position);

            // ���� ���������� ������ ��������� ��������, ��������� ��������, ��������� � ��������
            if (distance < 2f)
            {
                // ������� ����� �������������� � ��������
                note.Interact();
            }
        }
    }

    // ����� ��� ���������� ��������� ������� � ������������ � ������
    private void UpdateNoteVisibility()
    {
        if (_noteImage != null)
        {
            // ���������� ��� ������������ ������ ������� � ����������� �� �����
            _noteImage.enabled = isNoteVisible;
        }
    }
}

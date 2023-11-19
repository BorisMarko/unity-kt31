using UnityEngine;

public class ScreamTrigger : MonoBehaviour
{
    public AudioSource scream;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // ������������� ����, ���� ������� ��� �� �����������
            scream.Play();

            // �������� ������� ��� ��������������
            hasTriggered = true;
        }
    }
}

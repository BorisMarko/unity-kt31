using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime; // ����� ����� �������

    void Start()
    {
        StartCoroutine(DestructAfterTime());
    }

    IEnumerator DestructAfterTime()
    {
        // ���� ��������� ����� � ����� ���������� ������
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

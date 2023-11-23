using System.Collections;
using UnityEngine;

public class SubDestroy : MonoBehaviour
{
    public float lifetime; // ����� ����� �������

    void Start()
    {
        StartCoroutine(DestructAfterTimeCoroutine());
    }

    IEnumerator DestructAfterTimeCoroutine()
    {
        // ���� ��������� ����� � ����� ���������� ������
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

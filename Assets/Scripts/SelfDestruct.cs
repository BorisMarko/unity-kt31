using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float lifetime; // ����� ����� �������

    private void Start()
    {
        StartCoroutine(DestructAfterTime());
    }

    public IEnumerator DestructAfterTime()
    {
        // ���� ��������� ����� � ����� ���������� ������
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
using System.Collections;
using UnityEngine;

public class SubDestroy : MonoBehaviour
{
    public float lifetime; // Время жизни объекта

    void Start()
    {
        StartCoroutine(DestructAfterTimeCoroutine());
    }

    IEnumerator DestructAfterTimeCoroutine()
    {
        // Ждем указанное время и затем уничтожаем объект
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

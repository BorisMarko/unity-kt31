using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float lifetime; // Время жизни объекта

    private void Start()
    {
        StartCoroutine(DestructAfterTime());
    }

    public IEnumerator DestructAfterTime()
    {
        // Ждем указанное время и затем уничтожаем объект
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
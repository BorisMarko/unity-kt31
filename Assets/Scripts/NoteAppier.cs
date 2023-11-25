using UnityEngine;
using UnityEngine.UI;

public class NoteAppier : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _noteImage.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _noteImage.enabled = false;
    }
}
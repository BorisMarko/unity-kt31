using UnityEngine;

public class ScreamTrigger : MonoBehaviour
{
    public AudioSource scream;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Воспроизводим звук, если триггер еще не активирован
            scream.Play();

            // Помечаем триггер как активированный
            hasTriggered = true;
        }
    }
}

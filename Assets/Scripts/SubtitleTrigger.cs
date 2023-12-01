using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SubtitleTrigger : MonoBehaviour
{
    public Text SubtitleText;
    public List<string> subtitleTexts = new List<string>();
    public Font customFont;
    public float letterDelay = 0.1f;

    public delegate void SubtitleEventHandler();
    public event SubtitleEventHandler OnSubtitleComplete;

    private void Start()
    {
        if (customFont != null && SubtitleText != null)
        {
            SubtitleText.font = customFont;
        }

        SubtitleText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisplayNextSubtitle());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideSubtitle();
        }
    }

    private IEnumerator DisplayNextSubtitle()
    {
        for (int i = 0; i < subtitleTexts.Count; i++)
        {
            string currentSubtitle = subtitleTexts[i];
            yield return DisplaySubtitle(currentSubtitle);
        }

        // Вызываем событие о завершении субтитров
        OnSubtitleComplete?.Invoke();

        // Уничтожаем объект после завершения субтитров
        Destroy(gameObject);
    }

    private IEnumerator DisplaySubtitle(string sentence)
    {
        SubtitleText.text = "";
        SubtitleText.gameObject.SetActive(true);

        foreach (char letter in sentence)
        {
            SubtitleText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(letterDelay);
        HideSubtitle();
    }

    private void HideSubtitle()
    {
        SubtitleText.gameObject.SetActive(false);
    }
}

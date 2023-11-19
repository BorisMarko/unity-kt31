using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SequentialText : MonoBehaviour
{
    public Text textElement;
    public string[] texts;
    public float delayBetweenTexts = 2f;
    public string nextSceneName;  // Имя следующей сцены

    private void Start()
    {
        StartCoroutine(DisplaySequentialTexts());
    }

    IEnumerator DisplaySequentialTexts()
    {
        foreach (string text in texts)
        {
            textElement.text = text;
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // По окончании отображения всех текстов переходим на следующую сцену
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Не указано имя следующей сцены!");
        }
    }
}

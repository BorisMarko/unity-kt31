using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SequentialText : MonoBehaviour
{
    public Text textElement;
    public string[] texts;
    public float delayBetweenTexts = 2f;
    public string nextSceneName;  // ��� ��������� �����

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

        // �� ��������� ����������� ���� ������� ��������� �� ��������� �����
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("�� ������� ��� ��������� �����!");
        }
    }
}

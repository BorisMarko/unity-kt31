using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubText : MonoBehaviour
{
    public string[] textSubtitle;
    public Text subtext;
    public Font customFont;
    public float nextTextDelay;
    private int currentIndex;

    private void Start()
    {
        if (customFont != null && subtext != null)
        {
            subtext.font = customFont;
        }

        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        foreach (string sentence in textSubtitle)
        {
            for (int i = 0; i <= sentence.Length; i++)
            {
                subtext.text = sentence.Substring(0, i);
                yield return new WaitForSeconds(nextTextDelay);
            }

            // Подождите некоторое время перед отображением следующего текста
            yield return new WaitForSeconds(nextTextDelay);
        }
    }
}

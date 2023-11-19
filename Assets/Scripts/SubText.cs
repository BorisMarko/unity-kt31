using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubText : MonoBehaviour
{
    public string[] textSubtitle;

    public Text subtext;
    public Font customFont; // Add a public variable for the custom font
    public float NextText;
    public int endVoice;
    private float timer;
    private int i;

    void Start()
    {
        // Check if a custom font is provided and set it
        if (customFont != null && subtext != null)
        {
            subtext.font = customFont;
        }
        else
        {
            Debug.LogWarning("Custom font or subtext component is null.");
        }
    }

    void FixedUpdate()
    {
        // Проверяем, что массив textSubtitle не является null и i находится в пределах массива
        if (textSubtitle != null && i < textSubtitle.Length)
        {
            // Проверяем, что subtext не является null
            if (subtext != null)
            {
                subtext.text = textSubtitle[i];
            }
            else
            {
                Debug.LogWarning("Компонент subtext равен null.");
            }

            timer += 1 * Time.deltaTime;

            // Проверяем условие для перехода к следующему элементу массива
            if (timer >= NextText)
            {
                i += 1;
                timer = 0;
            }
        }
        else
        {
            // Логика обработки, если условия не выполнены
            Debug.LogWarning("Массив textSubtitle равен null или индекс i выходит за пределы массива.");
        }
    }
}

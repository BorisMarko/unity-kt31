using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubText : MonoBehaviour
{
    public string[] textSubtitle;

    public Text subtext;
    public float NextText;
    public int endVoice;
    private float timer;
    private int i;

void FixedUpdate()
{
    // ���������, ��� ������ textSubtitle �� �������� null � i ��������� � �������� �������
    if (textSubtitle != null && i < textSubtitle.Length)
    {
        // ���������, ��� subtext �� �������� null
        if (subtext != null)
        {
            subtext.text = textSubtitle[i];
        }
        else
        {
            Debug.LogWarning("��������� subtext ����� null.");
        }

        timer += 1 * Time.deltaTime;

        // ��������� ������� ��� �������� � ���������� �������� �������
        if (timer >= NextText)
        {
            i += 1;
            timer = 0;
        }
    }
    else
    {
        // ������ ���������, ���� ������� �� ���������
        Debug.LogWarning("������ textSubtitle ����� null ��� ������ i ������� �� ������� �������.");
    }
}


}
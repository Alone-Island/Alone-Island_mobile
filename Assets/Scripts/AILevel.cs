using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AILevel : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float currValue; // N : ���� ����
    private string currText; // N : ���� �ؽ�Ʈ
    public float maxValue { get; set; } // N : �ִ� ����
    public float fCurrValue
    {
        get
        {
            return currValue;
        }
        set
        {
            if (value > maxValue) currValue = maxValue; // N : �ִ� ������ �ʰ��ϴ� ���
            else if (value < 0) currValue = 0; // N : 0 �̸��� ���
            else currValue = value;

            // N : ���� ������ ���� �ؽ�Ʈ ����
            if (currValue == maxValue) currText = "Lv.M"; // N : ���� ������ �ִ� �����̸� M���� ǥ��
            else currText = "Lv." + currValue.ToString();
        }
    }
    public void initLv(float curr, float max)
    {
        maxValue = max;
        fCurrValue = curr;
    }

    public bool IsMax()
    {
        bool isMax = false;
        if (currValue >= maxValue) isMax = true;
        return isMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currText != text.text)
        {
            text.text = currText;
        }
    }
}

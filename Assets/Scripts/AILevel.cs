using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AILevel : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float currValue; // N : 현재 레벨
    private string currText; // N : 현재 텍스트
    public float maxValue { get; set; } // N : 최대 레벨
    public float fCurrValue
    {
        get
        {
            return currValue;
        }
        set
        {
            if (value > maxValue) currValue = maxValue; // N : 최대 레벨을 초과하는 경우
            else if (value < 0) currValue = 0; // N : 0 미만일 경우
            else currValue = value;

            // N : 현재 레벨에 따른 텍스트 수정
            if (currValue == maxValue) currText = "Lv.M"; // N : 현재 레벨이 최대 레벨이면 M으로 표시
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

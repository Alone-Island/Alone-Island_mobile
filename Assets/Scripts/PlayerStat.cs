using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    private Image filledImage;
    private float currValue; // N : 현재 스탯
    private float currFill; // N : 현재 이미지 fill (Max : 1)
    public float maxValue { get; set; } // N : 최대 스탯
    public float fCurrValue
    {
        get
        {
            return currValue;
        }
        set
        {
            if (value > maxValue) currValue = maxValue; // N : 최대 스탯을 초과하는 경우
            else if (value < 0) currValue = 0; // N : 0 미만일 경우
            else currValue = value;

            currFill = 0.08f + (currValue / maxValue) * 0.92f; // N : 현재 스탯에 따른 이미지 fill 수정
            // N : 스탯 이미지가 0.08부터 채워지므로 값 보정
        }
    }

    // N : 스탯 초기화
    public void initStat(float curr, float max)
    {
        maxValue = max;
        fCurrValue = curr;
    }

    [SerializeField]
    private float speed; // N : 애니메이션 속도

    // Start is called before the first frame update
    void Start()
    {
        filledImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currFill != filledImage.fillAmount)
        {
            filledImage.fillAmount = Mathf.Lerp(filledImage.fillAmount, currFill, Time.deltaTime * speed);
        }
    }
}

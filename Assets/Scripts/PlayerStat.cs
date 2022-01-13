using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    private Image filledImage;
    private float currValue; // N : ���� ����
    private float currFill; // N : ���� �̹��� fill (Max : 1)
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

            currFill = 0.08f + (currValue / maxValue) * 0.92f; // N : ���� ���ȿ� ���� �̹��� fill ����
            // N : ���� �̹����� 0.08���� ä�����Ƿ� �� ����
        }
    }

    // N : ���� �ʱ�ȭ
    public void initStat(float curr, float max)
    {
        maxValue = max;
        fCurrValue = curr;
    }

    [SerializeField]
    private float speed; // N : �ִϸ��̼� �ӵ�

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

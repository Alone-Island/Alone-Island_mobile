using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image panel;
    private System.Action endFadeOutFunc;

    private void Awake()
    {
        panel = this.GetComponent<Image>(); // J : panel�� �̹��� component ��������
    }
    private void Start()
    {
        StartCoroutine("FadeIn", panel);   // J : ���� ���� �� ���̵���
    }

    public IEnumerator FadeIn(Image img)
    {
        float fadeCount = 1;    // J : �ʱ� ���İ�(���� ȭ��)
        while (fadeCount > 0)    // J : ���İ��� �ּ�(0)�� �� ������ �ݺ�
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01�ʸ��� �������->1�� �� ������ �����
            img.color = new Color(0, 0, 0, fadeCount);    // J : ���İ� ����
        }
        img.gameObject.SetActive(false);  // J : ���̵��� ������ ��Ȱ��ȭ
    }

    public void GameFadeOut(System.Action func)
    {
        panel.gameObject.SetActive(true);   // J : ���� �� ��Ȱ��ȭ �����̹Ƿ� Ȱ��ȭ
        endFadeOutFunc = func;
        StartCoroutine("FadeOut", panel);    // J : ���̵�ƿ� ����
    }

    private IEnumerator FadeOut(Image img)
    {
        float fadeCount = 0;    // J : �ʱ� ���İ�(���� ȭ��)
        while (true)    // J : ���İ��� �ִ�(1)�� �� ������ �ݺ�
        {
            if (fadeCount >= 1) // J : ���̵�ƿ��� ������ �Լ� ����
            {
                if (endFadeOutFunc != null)
                {
                    endFadeOutFunc();
                    endFadeOutFunc = null;
                }
                break;
            }
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01�ʸ��� ��ο�����->1�� �� ������ ��ο���
            img.color = new Color(0, 0, 0, fadeCount);    // J : ���İ� ����
        }
    }
}

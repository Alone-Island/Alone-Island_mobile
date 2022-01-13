using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    public float fadeCount = 0;        // J : �ʱ� ���İ�
    private float fadeInterval = 0.0001f;// J : ���̵� �ð� ����(0.01�̸� 1�� �ҿ�)
    
    private IEnumerator FadeIn()
    {
        while (true)
        {
            if (fadeCount >= 1) // J : ���İ��� �ִ�(1)�� �� ������ �ݺ�
                break;

            fadeCount += 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval �ʸ��� �������� -> fadeInterval*100�� �� ������ ����
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : ���İ� ����
        }
    }

    public IEnumerator FadeOut()
    {
        while (fadeCount > 0)    // J : ���İ��� �ּ�(0)�� �� ������ �ݺ�
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval �ʸ��� �帴���� -> fadeInterval*100�� �� ������ �Ⱥ���
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : ���İ� ����
        }
    }
}

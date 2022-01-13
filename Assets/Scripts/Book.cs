using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// J : Book Prefab Script
public class Book : MonoBehaviour
{
    public float fadeCount = 0;        // J : 초기 알파값
    private float fadeInterval = 0.0001f;// J : 페이드 시간 간격(0.01이면 1초 소요)
    
    private IEnumerator FadeIn()
    {
        while (true)
        {
            if (fadeCount >= 1) // J : 알파값이 최대(1)가 될 때까지 반복
                break;

            fadeCount += 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval 초마다 선명해짐 -> fadeInterval*100초 후 완전히 보임
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : 알파값 조정
        }
    }

    public IEnumerator FadeOut()
    {
        while (fadeCount > 0)    // J : 알파값이 최소(0)가 될 때까지 반복
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(fadeInterval); // J : fadeInterval 초마다 흐릿해짐 -> fadeInterval*100초 후 완전히 안보임
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, fadeCount);    // J : 알파값 조정
        }
    }
}

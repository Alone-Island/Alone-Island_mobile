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
        panel = this.GetComponent<Image>(); // J : panel의 이미지 component 가져오기
    }
    private void Start()
    {
        StartCoroutine("FadeIn", panel);   // J : 게임 시작 시 페이드인
    }

    public IEnumerator FadeIn(Image img)
    {
        float fadeCount = 1;    // J : 초기 알파값(검은 화면)
        while (fadeCount > 0)    // J : 알파값이 최소(0)가 될 때까지 반복
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01초마다 밝아지게->1초 후 완전히 밝아짐
            img.color = new Color(0, 0, 0, fadeCount);    // J : 알파값 조정
        }
        img.gameObject.SetActive(false);  // J : 페이드인 끝나면 비활성화
    }

    public void GameFadeOut(System.Action func)
    {
        panel.gameObject.SetActive(true);   // J : 게임 중 비활성화 상태이므로 활성화
        endFadeOutFunc = func;
        StartCoroutine("FadeOut", panel);    // J : 페이드아웃 시작
    }

    private IEnumerator FadeOut(Image img)
    {
        float fadeCount = 0;    // J : 초기 알파값(검은 화면)
        while (true)    // J : 알파값이 최대(1)가 될 때까지 반복
        {
            if (fadeCount >= 1) // J : 페이드아웃이 끝나면 함수 실행
            {
                if (endFadeOutFunc != null)
                {
                    endFadeOutFunc();
                    endFadeOutFunc = null;
                }
                break;
            }
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // J : 0.01초마다 어두워지게->1초 후 완전히 어두워짐
            img.color = new Color(0, 0, 0, fadeCount);    // J : 알파값 조정
        }
    }
}

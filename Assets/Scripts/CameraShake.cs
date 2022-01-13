using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// J : https://ncube-studio.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%B9%B4%EB%A9%94%EB%9D%BC-%ED%9D%94%EB%93%A4%EA%B8%B0%EC%89%90%EC%9D%B4%ED%81%AC-%ED%9A%A8%EA%B3%BC-%EA%B5%AC%ED%98%84-%EC%A7%80%EC%A7%84-%ED%8F%AD%EB%B0%9C-%EC%8A%88%ED%8C%85%EC%8B%9C-%EC%9C%A0%EC%9A%A9%ED%95%9C-%ED%9A%A8%EA%B3%BC-Unity-C-ScriptCamera-Shake-Invoke-InvokeRepeating
public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;   // J : 카메라
    Vector3 originCameraPos;    // J : 흔들기 전 카메라 위치
    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f; // J : 카메라 흔들리는 범위, 정도
    [SerializeField] [Range(0.1f, 5f)] float duration = 3f;   // J : 카메라 흔드는 시간
    private System.Action shakeEndFunction;     // J : 카메라 흔들기가 끝나고 실행할 함수

    public void Shake(System.Action func)
    {
        shakeEndFunction = func;    // J : 카메라 흔들기가 끝나고 실행할 함수
        originCameraPos = mainCamera.transform.position;    // J : 기존 카메라 위치 저장
        InvokeRepeating("StartShake", 0f, 0.005f);  // J : 0.005초마다 StartShake함수 호출
        Invoke("StopShake", duration);  // J : duration 이후 카메라 흔들기 중지
    }

    void StartShake()
    {
        // J : 랜덤값을 현재 카메라의 위치에 더함
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;  // J : 카메라의 위치를 변경
    }

    void StopShake()
    {
        CancelInvoke("StartShake"); // J : StartShake 함수 중지
        mainCamera.transform.position = originCameraPos;    // J : 카메라의 위치를 기존 위치로 되돌림
        shakeEndFunction(); // J : 카메라 흔들기가 끝나고 실행해야 할 함수를 호출
    }
}

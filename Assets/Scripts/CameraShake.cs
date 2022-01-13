using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// J : https://ncube-studio.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%B9%B4%EB%A9%94%EB%9D%BC-%ED%9D%94%EB%93%A4%EA%B8%B0%EC%89%90%EC%9D%B4%ED%81%AC-%ED%9A%A8%EA%B3%BC-%EA%B5%AC%ED%98%84-%EC%A7%80%EC%A7%84-%ED%8F%AD%EB%B0%9C-%EC%8A%88%ED%8C%85%EC%8B%9C-%EC%9C%A0%EC%9A%A9%ED%95%9C-%ED%9A%A8%EA%B3%BC-Unity-C-ScriptCamera-Shake-Invoke-InvokeRepeating
public class CameraShake : MonoBehaviour
{
    public Camera mainCamera;   // J : ī�޶�
    Vector3 originCameraPos;    // J : ���� �� ī�޶� ��ġ
    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f; // J : ī�޶� ��鸮�� ����, ����
    [SerializeField] [Range(0.1f, 5f)] float duration = 3f;   // J : ī�޶� ���� �ð�
    private System.Action shakeEndFunction;     // J : ī�޶� ���Ⱑ ������ ������ �Լ�

    public void Shake(System.Action func)
    {
        shakeEndFunction = func;    // J : ī�޶� ���Ⱑ ������ ������ �Լ�
        originCameraPos = mainCamera.transform.position;    // J : ���� ī�޶� ��ġ ����
        InvokeRepeating("StartShake", 0f, 0.005f);  // J : 0.005�ʸ��� StartShake�Լ� ȣ��
        Invoke("StopShake", duration);  // J : duration ���� ī�޶� ���� ����
    }

    void StartShake()
    {
        // J : �������� ���� ī�޶��� ��ġ�� ����
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;  // J : ī�޶��� ��ġ�� ����
    }

    void StopShake()
    {
        CancelInvoke("StartShake"); // J : StartShake �Լ� ����
        mainCamera.transform.position = originCameraPos;    // J : ī�޶��� ��ġ�� ���� ��ġ�� �ǵ���
        shakeEndFunction(); // J : ī�޶� ���Ⱑ ������ �����ؾ� �� �Լ��� ȣ��
    }
}

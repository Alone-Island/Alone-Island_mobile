using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// K : ���� ũ���� �ؽ�Ʈ�� ���� Ŭ���� �Դϴ�
public class EndingCreditsManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += 0.05f; // K : ���� ũ������ �ö󰡴� �ӵ� �Դϴ�.
        transform.position = pos;

        if(transform.position.y > 45) // K : ���� ũ������ �������� 15���� Ŀ����, 
                                      // �̶� 15�� ���������� ���� �����̹Ƿ� ������ ������ ��ü�� �ʿ䰡 ����
        {
            SceneManager.LoadScene("GameMenu"); // K : ���� ũ������ ��� �ö󰡸� ���� �޴��� ���ư��� �ڵ� �Դϴ�.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// K : 엔딩 크레딧 텍스트를 위한 클래스 입니다
public class EndingCreditsManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += 0.05f; // K : 엔딩 크레딧이 올라가는 속도 입니다.
        transform.position = pos;

        if(transform.position.y > 45) // K : 엔디 크레딧의 포지션이 15보다 커지면, 
                                      // 이때 15는 눈대중으로 맞춘 숫자이므로 적절한 변수로 대체할 필요가 있음
        {
            SceneManager.LoadScene("GameMenu"); // K : 엔딩 크레딧이 모두 올라가면 게임 메뉴로 돌아가는 코드 입니다.
        }
    }
}

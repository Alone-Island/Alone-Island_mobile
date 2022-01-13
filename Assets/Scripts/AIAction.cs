using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script 추가
public class AIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;                          // C : 애니메이션 제어
    SpecialEventManager specialManager;     // K : SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수 생성
    public LearningManager learningManager;
    public GameManager gameManager;
    public int vel = 1;                     // K : ai 이동 속도 조절
    public int nextAIMoveX = 0;             // K : ai의 다음 X축 방향 변후
    public int nextAIMoveY = 0;             // K : ai의 다음 Y축 방향 변후
    public bool isAICollisionToPlayer = false;     // K : ai가 player와 충돌

    void NextAiMoveDirection()              // K : ai가 랜덤하게 움직이도록 랜덤한 방향을 결정해주는 함수
    {
        int random = Random.Range(2, 6);    // K : ai가 움직이 방향 랜덤 설정

        // K : AI가 가면 안되는 방향 제어

        switch (random)
        {
            case 1:                         // K : 정지
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
            case 2:                         // K : 왼쪽
                nextAIMoveX = -1 * vel;
                nextAIMoveY = 0;
                break;
            case 3:                         // K : 위
                nextAIMoveX = 0;
                nextAIMoveY = vel;
                break;
            case 4:                         // K : 오른쪽
                nextAIMoveX = vel;
                nextAIMoveY = 0;
                break;
            case 5:                         // K : 아래
                nextAIMoveX = 0;
                nextAIMoveY = -1 * vel;
                break;
            default:
                nextAIMoveX = 0;            // K : 예외처리 - 정지
                nextAIMoveY = 0;
                break;
        }

        Invoke("NextAiMoveDirection", 5);   // K : 재귀함수, 5초 후 자기 자신을 재실행 
    }

    public void GoToLearningPlace(int x, int y) // K : AI가 학습 장소로 순간이동 하게 하는 함수
                                                // (x,y)좌표를 파라미터로 받는다.
    {
        transform.position = new Vector3(x, y, 0);
    }

    void Awake()
    {
        // C : Animator component instance 생성
        anim = GetComponent<Animator>();
        // K : SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수를 불러오기 위해 호출
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("NextAiMoveDirection", 5);   // K : 5초 후 ai가 움직일 방향 결정 함수 실행
    }

    void Update()
    {
        // C : ai 이동(NextAiMoveDirection의 결과)값에 따라 애니메이션 적용
        if (!isAICollisionToPlayer)
        {
            if (anim.GetInteger("hAxisRaw") != nextAIMoveX)         // C : ai 좌우 이동 시 적절한 애니메이션 실행
            {
                anim.SetInteger("hAxisRaw", (int)nextAIMoveX);
            }
            else if (anim.GetInteger("vAxisRaw") != nextAIMoveY)    // C : ai 상하 이동 시 적절한 애니메이션 실행
            {
                anim.SetInteger("vAxisRaw", (int)nextAIMoveY);
            }
        }
        else
        {
            anim.SetInteger("hAxisRaw", 0);
            anim.SetInteger("vAxisRaw", 0);
        }
    }

    void FixedUpdate()
    {   
        if (rigid.bodyType == RigidbodyType2D.Dynamic)
        {
            if (specialManager.special || learningManager.isAILearning || gameManager.isEndingShow || gameManager.playerTalk || isAICollisionToPlayer)
            // K : 스페셜 이벤트, 플레이어가 AI와 대화하는,  AI가 학습중일때 정지, 엔딩카드가 보여졌을 때, AI와 플레이어가 충돌중일때
            {
                rigid.velocity = new Vector2(0, 0); // K : AI 정지
            }
            else
            {
                rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : AI 이동
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)   // K : AI 충돌 감지 함수
    {
        if (coll.gameObject.name == "Dr.Kim")
        {
            isAICollisionToPlayer = true;   // K : AI가 플레이어와 충돌을 확인하기 위한 코드

            
            // C : 충돌 방향 감지
            Vector2 collisionDir = transform.position - coll.gameObject.transform.position;
            
            /*Debug.Log("ai direction : " + transform.position);
            Debug.Log("박사 direction : " + coll.gameObject.transform.position);
            Debug.Log("direction : " + collisionDir);

            int collisionDirX = Mathf.RoundToInt(collisionDir.x);
            int collisionDirY = Mathf.RoundToInt(collisionDir.y);
            Debug.Log("x절댓값 : " + Mathf.Abs(collisionDir.x));
            Debug.Log("y절댓값 : " + Mathf.Abs(collisionDir.y));

            Debug.Log("x : " + collisionDir.x + "dX : " + collisionDirX + "절댓값 : " + Mathf.Abs(collisionDir.x));
            Debug.Log("y : " + collisionDir.y + "dY : " + collisionDirY + "절댓값 : " + Mathf.Abs(collisionDir.y));
            

            //Debug.Log("x  : " + collisionDirX + ", 절대값 x : " + Mathf.Abs(collisionDirX));
            //Debug.Log("y  : " + collisionDirY + ", 절대값 y : " + Mathf.Abs(collisionDirY));

            Debug.Log("x 절대값 : " + Math.Abs((int)collisionDir.x));
            Debug.Log("y 절대값 : " + Math.Abs((int)collisionDir.y));*/

            if (Mathf.Abs(collisionDir.x) > 1)
            {
                if (collisionDir.x - collisionDir.y < 0)
                {
                    Debug.Log("박사 방향 : 오른쪽");
                    anim.SetBool("right", true);
                }
                else if (collisionDir.x - collisionDir.y > 0)
                {
                    Debug.Log("박사 방향 : 왼쪽");
                    anim.SetBool("left", true);
                }
            }
            else if(Mathf.Abs(collisionDir.y) > 1)
            {
                if (collisionDir.x - collisionDir.y < 0)
                {
                    Debug.Log("박사 방향 : 아래쪽");
                    anim.SetBool("down", true);
                }
                else if (collisionDir.x - collisionDir.y > 0)
                {
                    Debug.Log("박사 방향 : 위쪽");
                    anim.SetBool("up", true);
                }
            }


            // C : AI 머리 위에 Talk(spacebar) 오브젝트 띄우기
            GameObject talkObj = transform.Find("Talk").gameObject;
            talkObj.SetActive(true);

            // K : 충돌시 밀림현상 제거를 위해 body type을 dynamic에서 static으로 변경
            rigid.bodyType = RigidbodyType2D.Static;
        }        
    }

    void OnCollisionStay2D(Collision2D coll)  // Ai 충돌 유지 감지 함수
    {
        
    }

    void OnCollisionExit2D(Collision2D coll)   // K : AI 충돌 제거 감지 함수
    {
        if (coll.gameObject.name == "Dr.Kim")
        {
            isAICollisionToPlayer = false; // K : AI가 플레이어와 충돌이 제거됨을 확인하기 위한 코드
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            // C : AI 머리 위의 Talk(spacebar) 오브젝트 제거하기
            GameObject talkObj = transform.Find("Talk").gameObject;
            talkObj.SetActive(false);

            // K : 충돌시 밀림현상 제거를 위해 변경했던 body type을 다시 dynamic으로 변경
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}
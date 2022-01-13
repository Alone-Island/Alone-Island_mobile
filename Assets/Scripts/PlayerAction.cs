using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// C : Dr.Kim 오브젝트의 모든 Action과 관련된 기능들이 들어있는 스크립트
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim 이동 속력
    public GameManager manager;         // C : player에서 GameManager의 함수를 호출할 수 있도록 manager 변수 생성
    public ScreenManager screenManager;         // J : 책을 주웠을 때 책 개수 증가를 위해 ScreenManager 변수 생성
    public SpecialEventManager specialManager;  // J : player에서 SpecialEventManager의 함수를 호출할 수 있도록 specialManager 변수 생성
    public EndingManager endingManager;         // J : EndingTalk 호출할 수 있도록 endingManager 변수 생성
    public SettingManager settingManager;       // J : 설정창 활성화 중에는 플레이어가 움직일 수 없게 settingManager 변수 생성

    private AIAction aiAction;

    float h;    // C : horizontal (수평 이동)
    float v;    // C : vertical (수직 이동)
    bool isHorizonMove;     // C : 수평 이동이면 true, 수직 이동이면 false
    Vector3 dirVec;     // C : 현재 바라보고 있는 방향 값
    GameObject scanObject;  // C : 스캔된 game object

    //N : 학습하기 안내 아이콘들
    public GameObject farmIcon;
    public GameObject houseIcon;
    public GameObject craftIcon;
    public GameObject engineerIcon;

    // C : 책을 찾았을 때 'book + 1' 애니메이션을 주기 위한 변수들
    public GameObject addBook;      // C : 'book + 1' object 변수
    //private float time = 0;       // C :
    public GameObject player;       // C : 플레이어 object 변수
    private List<GameObject> addBookListG = new List<GameObject>();      // C : addBook object를 담을 리스트
    private List<float> addBookListT = new List<float>();                // C : addBook의 애니메이션 시간을 담을 리스트
    public EffectPlay effect; // K: 효과음 이벤트 발생 오브젝트

    // C : 스페셜이벤트 발생 시 '!' 오브젝트를 띄워주기 위한 변수들
    public GameObject alarm;        // C : '!' object 변수
    //private float alarmEffectTime = 0;
    //int i = 1;

    Rigidbody2D rigid;  // C : 물리 제어
    Animator anim;      // C : 애니메이션 제어

    void Awake()
    {
        // C : component instance 생성
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aiAction = GameObject.Find("AI").GetComponent<AIAction>();
    }

    void Update()
    {
        // J : 설정창 활성화 상태면 action X
        if (settingManager.nowSetting)
            return;
        // C : 입력된 수평/수직 이동을 대입 (-1, 0, 1)
        // C : GameManager의 isTPShow를 사용하여 talkPanel이 보여지고 있을 때
        // J : or SpecialEventManager의 special을 사용하여 스페셜 이벤트 진행 중인 경우 플레이어의 이동을 제한
        // N :
        h = manager.isTPShow || specialManager.special || manager.isEndingShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isTPShow || specialManager.special || manager.isEndingShow ? 0 : Input.GetAxisRaw("Vertical");

        // C : 키보드 입력(down, up)이 horizontal인지 vertical인지 확인
        // C : GameManager의 isTPShow를 사용하여 talkPanel이 보여지고 있을 때
        // J : or SpecialEventManager의 special을 사용하여 스페셜 이벤트 진행 중인 경우 플레이어의 이동을 제한
        // N :
        bool hDown = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonDown("Horizontal");
        bool hUp = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonUp("Horizontal");
        bool vDown = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonDown("Vertical");
        bool vUp = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonUp("Vertical");

        // C : isHorizonMove 값 설정
        if (hDown)           // C : 수평 키를 누르면 isHorizonMove는 true
            isHorizonMove = true;
        else if (vDown)      // C : 수직 키를 누르면 isHorizonMove는 false
            isHorizonMove = false;
        else if (hUp || vUp)        // C : 수평 키나 수직 키의 양 쪽(e.g.(<- && ->))을 둘 다 눌렀다 뗐을 때도 고려
            isHorizonMove = h != 0;

        // C : Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // C : "hAxisRaw" 값이 현재 h 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // C : animation "hAxisRaw" parameter 값 설정
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // C : "vAxisRaw" 값이 현재 v 값과 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // C : animation "vAxisRaw" parameter 값 설정
        }
        else
            anim.SetBool("isChange", false);        // C : 방향 변화를 위한 animation parameter 값을 false로 설정

        // C : dirVec(현재 바라보고 있는 방향) 값 설정
        if (vDown && v == 1)                // C : 수직 키를 눌렀고, 입력된 수직 값이 1이면
            dirVec = Vector3.up;            // C : dirVec 값은 up
        else if (vDown && v == -1)          // C : 수직 키를 눌렀고, 입력된 수직 값이 -1이면
            dirVec = Vector3.down;          // C : dirVec 값은 down
        if (hDown && h == -1)               // C : 수평 키를 눌렀고, 입력된 수평 값이 -1이면
            dirVec = Vector3.left;          // C : dirVec 값은 left
        if (hDown && h == 1)                // C : 수평 키를 눌렀고, 입력된 수평 값이 1이면
            dirVec = Vector3.right;         // C : dirVec 값은 right

        // J : 스페이스바 누름
        if (Input.GetButtonDown("Jump"))
        {
            if (specialManager.special)     // J : 스페셜 이벤트 진행 중
            {
                if (specialManager.specialTalk)  // J : 선택지가 뜨기 전이라면
                    specialManager.Talk();  // J : specialManager의 Talk 함수 호출
                else if (specialManager.resultTalk)     // J : 선택지 클릭한 후 (스페셜 이벤트 진행중)
                    specialManager.ResultTalk();    // J : 결과 텍스트 보여주기
            }
            else if (manager.isEndingShow)  // J : 엔딩 보여주는 중
                endingManager.BadEndingTalk();  // J : 엔딩 대화 보여주기
            else if (scanObject != null)        // J : 스페셜 이벤트 진행 중이 아니고 scanObject가 있으면
                manager.Action(scanObject);     // C : 맵의 대화창에 적절한 메세지가 뜰 수 있도록 Action()함수 실행
            else    // J : 아무 상태도 아니거나 책 찾았다는 대화창이 뜬 상태..
                manager.talkPanel.SetActive(false); // J : 대화창 끄기

            // N : 엔딩 크레딧으로 연결
            // N : 나중에 버튼 만들어서 클릭으로 처리하면 좋을 것 같음.
            if (manager.isTheEnd)
            {
                SceneManager.LoadScene("GameMenu"); // K : 배드엔딩이 끝나고 바로 게임 메뉴로 돌아갑니다., 해피엔딩은 textmanager에서 처리함
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (scanObject != null)
            {
                ObjectData objData = scanObject.GetComponent<ObjectData>();
                if (objData.id >= 100 && objData.id <= 400)
                {
                    manager.isSelectedAILearning = false;
                    manager.Action(scanObject);
                }                    
            }
        }
       
        // C : 책을 찾았을 때 책 추가 애니메이션 시간 조정을 위해
        // C : 책 추가 애니메이션을 띄울 addBook object가 존재할 경우
        for (int i = 0; i < addBookListG.Count; i++)
        {
            // C : 해당 addBook object의 active 상태가 true인 경우
            if (addBookListG[i].activeSelf == true)
            {
                addBookListT[i] += Time.deltaTime;      // C : 해당 addBook object 애니메이션 타임에 흐른 시간 추가
                if (addBookListT[i] > 2f)               // C : 해당 addBook object 애니메이션 타임이 2초가 지났을 때
                {
                    addBookListT[i] = 0;                // C : 해당 addBook object 애니메이션 타임을 0으로 초기화
                    addBookListG[i].SetActive(false);   // C : 해당 addBook object의 active 상태를 false로 변환
                    Destroy(addBookListG[i]);           // C : 해당 addBook object를 삭제
                    addBookListG.RemoveAt(i);           // C : addBook을 담는 리스트에서 해당 addBook object 삭제
                    addBookListT.RemoveAt(i);           // C : addBook 애니메이션 시간을 담는 리스트에서 해당 addBook 애니메이션 시간 변수 삭제
                }
            }
        }


        
        /*IEnumerator coroutine = OnAlarm();
        if(i == 1)
        {
            StartCoroutine(coroutine);
            i--;
        }*/
        /*if (i == 1)
        {
            OnAlarm();
            i--;
        }*/
        /*
        // C : alarm 애니메이션의 화면 실행 시간을 조정
        if (alarm.activeSelf == true)                 // C : object의 active 상태가 true이면
        {
            alarmEffectTime += Time.deltaTime;        // C : 애니메이션 타임에 흐른 시간 추가
            if (alarmEffectTime > 3.3f)                 // C : 애니메이션 타임이 3.3초가 지났을 때
            {
                alarm.SetActive(false);               // C : object의 active 상태를 false로 변환
                alarmEffectTime = 0;                  // C : 다음 애니메이션을 위해 애니메이션 타임을 0으로 초기화
            }
        }
        else                                            // C : object의 active 상태가 false이면
        {
            alarmEffectTime = 0;                      // C : 다음 애니메이션을 위해 애니메이션 타임을 0으로 초기화
        }
        */
    }

    void FixedUpdate()
    {
        // C : player moving
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // C : 수평 혹은 수직 이동만 가능하도록 moveVec 설정
        rigid.velocity = moveVec * speed;     // C : rigid의 속도(속력 + 방향) 설정

        // C : Ray
        // C : 시작 위치는 rigid의 위치, 방향은 dirVec, 길이는 0.7f, 색깔은 green인 디버그라인을 설정(ray를 시각화)
        // J : 위, 아래 방향에서 스캔 못해서 길이 1.0f로 변경
        Debug.DrawRay(rigid.position, dirVec * 1.0f, new Color(0, 1, 0));
        // C : Object 레이어를 스캔하는 실제 RayCast 구현
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.0f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // C : ray가 Object를 감지했을 때
        {
            scanObject = rayHit.collider.gameObject;    // C : RayCast된 오브젝트를 scanObject로 설정
        }
        else
            scanObject = null;
    }

    // J : 책을 찾았을 때
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Book(Clone)") {        // J : 부딪힌 오브젝트가 책인 경우
            coll.gameObject.SetActive(false);               // J : Book Object 비활성화
            // manager.talkPanel.SetActive(true);              // J : 대화창 활성화
            // manager.talkText.text = "책을 찾았습니다!";     // J : 대화창 텍스트 적용
            screenManager.getBook();                        // J : 책 개수 증가

            // 책 +1 효과음
            effect.Play("FindBookEffect");

            // C : 책 추가 애니메이션 실행하기
            // C : 책 추가 game object를 복사하여 새로운 책 추가 game object 생성
            GameObject bookInstance = Instantiate(addBook, player.transform.localPosition, Quaternion.identity);
            // C : 현재 player의 머리 위에 bookInstance가 위치하도록 bookInstance의 부모 object 변경
            bookInstance.transform.SetParent(player.transform);
            // C : player 머리 위의 책 object 보이기
            bookInstance.SetActive(true);
            // C : 애니메이션 시작과 끝을 관리하기 위해 bookInstance와 애니메이션 시간을 리스트에 추가
            addBookListG.Add(bookInstance);
            addBookListT.Add(0f);                       
        }
    }


    /*// C : 
    IEnumerator OnAlarm()
    {
        int count = 0;
        while (count < 3)
        {
            alarm.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            alarm.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }*/

    /*
    public void OnAlarm()
    {
        alarm.SetActive(true);
    }
    */

    /*private static DateTime Delay(int MS)
    {
        DateTime startMoment = DateTime.Now;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
        DateTime endMoment = startMoment.Add(duration);
        while (endMoment >= startMoment)
        {
            System.Windows.Forms.Application.DoEvents();
            startMoment = DateTime.Now;
        }

        return DateTime.Now;
    }*/

    // C : 스페셜이벤트 발생 전 플레이어 머리 위에 알림 띄우기
    IEnumerator OnAlarm()
    {
        alarm.SetActive(true);
        yield return new WaitForSeconds(2.2f);
        alarm.SetActive(false);
    }

    // N : 장소에 들어가면
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // J : 책 반경에 들어간 경우
        if (coll.gameObject.name == "BookArea(Clone)")
        {
            GameObject book = coll.gameObject.transform.parent.gameObject;
            Book bookScript = book.GetComponent<Book>();

            book.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, bookScript.fadeCount);    // J : 초기 알파값 적용
            book.GetComponent<Renderer>().enabled = true;    // J : 책이 보이도록

            bookScript.StopCoroutine("FadeOut");   // J : 페이드 아웃 중이었다면 중단
            bookScript.StartCoroutine("FadeIn");   // J : 페이드 인 시작
        }

        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(true);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(true);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(true);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (aiAction.isAICollisionToPlayer)
        {
            if (coll.gameObject.name == "FarmLearning")
            {
                farmIcon.SetActive(false);
            }
            if (coll.gameObject.name == "HouseLearning")
            {
                houseIcon.SetActive(false);
            }
            if (coll.gameObject.name == "CraftLearning")
            {
                craftIcon.SetActive(false);
            }
            if (coll.gameObject.name == "EngineerLearning")
            {
                engineerIcon.SetActive(false);
            }
        } else
        {
            if (coll.gameObject.name == "FarmLearning")
            {
                farmIcon.SetActive(true);
            }
            if (coll.gameObject.name == "HouseLearning")
            {
                houseIcon.SetActive(true);
            }
            if (coll.gameObject.name == "CraftLearning")
            {
                craftIcon.SetActive(true);
            }
            if (coll.gameObject.name == "EngineerLearning")
            {
                engineerIcon.SetActive(true);
            }
        }
    }

    // N : 장소에서 나오면
    private void OnTriggerExit2D(Collider2D coll)
    {
        // J : 책 반경에서 나온 경우
        if (coll.gameObject.name == "BookArea(Clone)")
        {
            GameObject book = coll.gameObject.transform.parent.gameObject;
            Book bookScript = book.GetComponent<Book>();

            if (book.activeSelf == true)    // J : 책 오브젝트가 활성화 상태
            {
                bookScript.StopCoroutine("FadeIn");    // J : 페이드 인 중이었다면 중단
                bookScript.StartCoroutine("FadeOut");  // J : 페이드 아웃 시작
            }
        }

        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(false);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(false);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(false);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(false);
        }
    }
}

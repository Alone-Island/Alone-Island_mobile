using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawnManager : MonoBehaviour
{
    // J : https://angliss.cc/random-gameobject-created/ 참조
    public GameObject bookPrefab;
    public GameObject bookAreaPrefab;

    // C : 각 xxxxObject는 xxxxLearningObject의 부모 obejct
    // C : 책이 스폰될 때 농사 학습하기 장소에 스폰되는 것을 방지하기 위해 필요한 퍼블릭 변수 설정
    public GameObject farmObject;
    public GameObject farmLearningObject;
    // C : 책이 스폰될 때 건축 학습하기 장소에 스폰되는 것을 방지하기 위해 필요한 퍼블릭 변수 설정
    public GameObject houseObject;
    public GameObject houseLearningObject;
    // C : 책이 스폰될 때 공예 학습하기 장소에 스폰되는 것을 방지하기 위해 필요한 퍼블릭 변수 설정
    public GameObject craftObject;
    public GameObject craftLearningObject;
    // C : 책이 스폰될 때 공학 학습하기 장소에 스폰되는 것을 방지하기 위해 필요한 퍼블릭 변수 설정
    public GameObject labObject;
    public GameObject labLearningObject;
    // C : 책이 스폰될 때 현재 플레이어의 위치에 스폰되는 것을 방지하기 위해 필요한 퍼블릭 변수 설정
    public GameObject playerObject;
    private GameObject tempObject;           // C : 책이 스폰될 때 현재 플레이어의 위치에 스폰되는 것을 방지하기 위해 필요한 temp 퍼블릭 변수 설정

    public GameManager gameManager;

    int count = 4;                  // J : 찍어낼 책 개수
    private BoxCollider2D area;     // J : 박스 콜라이더의 사이즈 가져오기 위한 변수
    private List<GameObject> bookList = new List<GameObject>();         // 생성된 book object를 관리하기 위한 리스트 변수

    // J : count만큼 책 스폰
    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        // C : 책이 스폰될 때 현재 플레이어의 위치에 스폰되는 것을 방지하기 위해 필요한 component instance(GameObject) 생성
        tempObject = new GameObject("tempObject");

        StartCoroutine("Spawn");
    }

    // J : 게임 오브젝트를 복제하여 scene에 추가
    private IEnumerator Spawn()
    {
        for (int i = 0; i < count; i++) // J : count만큼 책 생성
        {
            Vector3 spawnPos = GetRandomPosition(); // J :랜덤 위치 return

            // J : 원본, 위치, 회전값을 매개변수로 받아 책 오브젝트 복제
            // J : Quaternion.identity <- 회전값 0
            GameObject book = Instantiate(bookPrefab, spawnPos, Quaternion.identity);
            book.GetComponent<Renderer>().enabled = false;  // J : 오브젝트가 보이지 않도록
            bookList.Add(book); // J : 오브젝트 관리를 위해 리스트에 add
                        
            GameObject bookArea = Instantiate(bookAreaPrefab, spawnPos, Quaternion.identity);   // J : bookArea 오브젝트 복제
            bookArea.transform.SetParent(book.transform);   // J : book Object의 자식 object로 생성
            bookArea.GetComponent<Renderer>().enabled = false;  // J : 오브젝트가 보이지 않도록

        }
        area.enabled = false;       // J : BoxCollider2D 끄기
        yield return new WaitForSeconds(gameManager.day);   // J : 하루 지남

        for (int i = 0; i < count; i++) // J : 책 삭제
            Destroy(bookList[i].gameObject);

        bookList.Clear();           // J : bookList 비우기
        area.enabled = true;        // J : BoxCollider2D 켜기
        StartCoroutine("Spawn");    // J : 책 다시 스폰
    }

    // J : 맵 내의 랜덤한 위치를 return
    private Vector2 GetRandomPosition()
    {
        // C : 기본적인 랜덤한 위치 생성하는 과정
        Vector2 basePosition = transform.position;  // J : 오브젝트의 위치
        Vector2 size = area.size;                   // J : box colider2d, 즉 맵의 크기 벡터

        // J : x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, 0);



        // C : book이 생성되면 안되는 위치를 제한하는 과정
        // C : 현재 player 위치의 가까이에 book이 생성되지 않도록 설정
        if (isLimit(tempObject, playerObject, spawnPos, 1))                     // C : 생성된 spawnPos의 위치가 현재 player 위치의 가까이에 존재한다면
        {
            return GetRandomPosition();                                         // C : book이 생성될 랜덤한 위치(spawnPos) 재생성
        }
        // C : 농사 학습하기 장소에 book이 생성되지 않도록 설정
        else if (isLimit(farmObject, farmLearningObject, spawnPos, 0))          // C : 생성된 spawnPos의 위치가 농사 학습하기 장소에 존재한다면
        {
            return GetRandomPosition();                                         // C : book이 생성될 랜덤한 위치(spawnPos) 재생성
        }
        // C : 건축 학습하기 장소에 book이 생성되지 않도록 설정
        else if (isLimit(houseObject, houseLearningObject, spawnPos, 0))        // C : 생성된 spawnPos의 위치가 건축 학습하기 장소에 존재한다면
        {
            return GetRandomPosition();                                         // C : book이 생성될 랜덤한 위치(spawnPos) 재생성
        }
        // C : 공예 학습하기 장소에 book이 생성되지 않도록 설정
        else if (isLimit(craftObject, craftLearningObject, spawnPos, 0))        // C : 생성된 spawnPos의 위치가 공예 학습하기 장소에 존재한다면
        {
            return GetRandomPosition();                                         // C : book이 생성될 랜덤한 위치(spawnPos) 재생성
        }
        // C : 공학 학습하기 장소에 book이 생성되지 않도록 설정
        else if (isLimit(labObject, labLearningObject, spawnPos, 0))            // C : 생성된 spawnPos의 위치가 공학 학습하기 장소에 존재한다면
        {
            return GetRandomPosition();                                         // C : book이 생성될 랜덤한 위치(spawnPos) 재생성
        }

        return spawnPos;    // J : 랜덤 위치 return
    }


    // C : 생성한 book의 랜덤 위치가 제한 지역에 있으면 true, 아니면 false 반환하는 함수
    // C : 
    private bool isLimit(GameObject baseObject, GameObject learningObject, Vector3 spawnPos, double margin)
    {
        // C : book이 생성되면 안되는 범위 구하기
        Vector3 basePos = baseObject.transform.localPosition;                       // C : 학습하기 영역의 부모 object가 original에서 존재하는 위치 구하기
        Vector3 learningPos = learningObject.transform.localPosition;               // C : 학습하기 영역이 부모 object를 기준으로 존재하는 위치 구하기
        Vector2 learningSize = learningObject.GetComponent<BoxCollider2D>().size;   // C : 학습하기 영역의 크기 구하기

        // C : book이 생성되면 안되는 범위를 (시작하는/좌측 x좌표), (끝나는/우측 x좌표), (시작하는/상단 y좌표), (끝나는/하단 y좌표) 순서대로 리스트에 저장하기
        // C : 계산 방법) 학습하기 영역의 x/y 좌표에, 학습하기 영역의 x/y 크기를 빼거나 더하고, 부모 영역의 x/y 좌표를 빼거나 더하고, 학습하기 영역의 x/y 좌표는 중심에 존재하기 때문에 0.5를 빼거나 더한다.
        // C : 계산 방법) 추가로 마진이 필요한 경우 마진을 빼거나 더해준다.
        double[] learingArea = new double[] {learningPos.x - learningSize.x + basePos.x - 0.5 - margin,
                                           learningPos.x + learningSize.x + basePos.x + 0.5 + margin,
                                           learningPos.y - learningSize.y + basePos.y - 0.5 - margin,
                                           learningPos.y + learningSize.y + basePos.y + 0.5 + margin};

        // C : book이 생성되면 안되는 위치에 있는지 확인
        if (spawnPos.x >= learingArea[0] && spawnPos.x <= learingArea[1]
            && spawnPos.y >= learingArea[2] && spawnPos.y <= learingArea[3])
        {
            return true;        // C : 생성되면 안되는 위치에 존재한다면 true 반환
        }

        return false;           // C : 생성되면 안되는 위치에 존재하지 않는다면 false 반환
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

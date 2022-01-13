using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScript : MonoBehaviour
{
    public GameManager gameManager;     // J : 튜토리얼 중에 상호작용 X, 시간 X 설정을 위해
    public GameObject tutorialCanvas;   // J : 모든 튜토리얼의 부모 오브젝트
    public List<GameObject> stepList;   // J : 튜토리얼 단계별 캔버스 오브젝트 리스트

    private int stepCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // J : 첫게임이고 튜토리얼이 존재하면 튜토리얼 수행
        if (DataController.Instance.endingData.firstGame == 1 && stepList.Count != 0)
            StartTutorial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartTutorial()
    {
        tutorialCanvas.SetActive(true); // J : 튜토리얼 UI 활성화
        gameManager.tutorial = true;    // J : 튜토리얼 중에 상호작용 X, 시간 X
    }

    public void ScreenTouch()
    {
        // J : 버튼 클릭한 오브젝트의 부모 오브젝트가 현재 튜토리얼 오브젝트인 경우
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == stepList[stepCount])
        {
            stepList[stepCount].SetActive(false);   // J : 현재 튜토리얼 종료
            stepCount++;    // J : 다음 스텝

            if (stepCount == stepList.Count)    // J : 모든 튜토리얼 수행한 경우
                gameManager.tutorial = false;   // J : 튜토리얼 종료
            else
                stepList[stepCount].SetActive(true);    // J : 다음 튜토리얼 진행
        }
        
    }
}

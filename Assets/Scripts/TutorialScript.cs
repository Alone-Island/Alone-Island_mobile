using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScript : MonoBehaviour
{
    public GameManager gameManager;     // J : Ʃ�丮�� �߿� ��ȣ�ۿ� X, �ð� X ������ ����
    public GameObject tutorialCanvas;   // J : ��� Ʃ�丮���� �θ� ������Ʈ
    public List<GameObject> stepList;   // J : Ʃ�丮�� �ܰ躰 ĵ���� ������Ʈ ����Ʈ

    private int stepCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // J : ù�����̰� Ʃ�丮���� �����ϸ� Ʃ�丮�� ����
        if (DataController.Instance.endingData.firstGame == 1 && stepList.Count != 0)
            StartTutorial();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartTutorial()
    {
        tutorialCanvas.SetActive(true); // J : Ʃ�丮�� UI Ȱ��ȭ
        gameManager.tutorial = true;    // J : Ʃ�丮�� �߿� ��ȣ�ۿ� X, �ð� X
    }

    public void ScreenTouch()
    {
        // J : ��ư Ŭ���� ������Ʈ�� �θ� ������Ʈ�� ���� Ʃ�丮�� ������Ʈ�� ���
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == stepList[stepCount])
        {
            stepList[stepCount].SetActive(false);   // J : ���� Ʃ�丮�� ����
            stepCount++;    // J : ���� ����

            if (stepCount == stepList.Count)    // J : ��� Ʃ�丮�� ������ ���
                gameManager.tutorial = false;   // J : Ʃ�丮�� ����
            else
                stepList[stepCount].SetActive(true);    // J : ���� Ʃ�丮�� ����
        }
        
    }
}

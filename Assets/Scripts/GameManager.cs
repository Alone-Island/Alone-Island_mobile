using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // C : UI ���α׷����� ���� �߰� (Text ��)
using TMPro;            // J : TextMeshProUGUI�� ���� �߰�

// C : ��ü���� ���� ���� �� ������ �����ִ� ��ũ��Ʈ
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI talkText;           // C : ��ȭâ�� text
    public GameObject scanObject;   // C : ��ĵ��(������) game object
    public GameObject talkPanel;    // C : ��ȭâ
    public bool isTPShow;           // C : talkPanel�� ���� ���� (�����ֱ� or �����)
    public TalkManager talkManager; // C : GameManager���� TalkManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public int talkIndex;           // C : �ʿ��� talkIndex�� �����ϱ� ���� ���� ����
    public int day = 20;            // J : �Ϸ�� 20��
    public int specialEventCoolTimeDay;        // ����� �̺�Ʈ�� �߻��ϱ� ���� ��Ÿ�� �ϼ�

    public SpecialEventManager specialManager; // J : GameManager���� SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� talkManager ���� ����
    public LearningManager learningManager;     // C : �÷��̾ AI�� �н���Ű�� action�� �õ����� ��, ������ �н��� ��ų �� �ֵ��� learningManager ���� ����
    public ScreenManager screenManager; // N : å ���� �������� ����
    public AIAction aiAction;           // K : ai�� play�� �浹������ Ȯ���ϱ� ���ؼ� > ��ȭ ����

    public bool playerTalk = false;           // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ���� ����
    public bool isSelectedAILearning = true;         // K : AI�� �н��� �Ұ����� �н��� ���� ���� ������ Ȯ���ϴ� �÷���
    public bool isEndingShow = false;         // N : ���� ���� (���� ī�� ��Ÿ�� ���ĺ���)
    public bool isTheEnd = false;         // N : ���� ���� ���� (���� ī�� ��Ÿ���� 2�� �ں���)
    int randomNum = 0;                  // C : AI���� ��ȭ ��, ������ ��ȭ ������ ����ϱ� ���� ���� ����
    public int dayTalk = 0;        // N : AI���� ��ȭ Ƚ��

    public TextMeshProUGUI alertText;           // N : �˸�â�� text

    public ObjectData aiObjData;            // C : ���� �� ��ũ��Ʈ�� ���� GetComponent ��Ŀ� ������ �߻��ϱ� ������ (�ӽ÷�) AIObject�� �ۺ� ������ ����

    private string talkData;
    public int talkId;

    // C : �÷��̾ Object�� ���� ���� ��(�÷��̾��� �׼� �߻� ��) ������ ������ ������ ��ȭâ ����ֱ�
    public void Action(GameObject scanObj)
    {
        playerTalk = true;                  // J : �÷��̾ ��ȭ�ϴ� �߿��� special event�� �����ϵ��� ����
        scanObject = scanObj;               // C : parameter�� ���� ��ĵ�� game object�� public ������ scanObject�� ����
        ObjectData objData = scanObject.GetComponent<ObjectData>();     // C : scanObject�� ObjectData instance ��������

        if (talkId == 0)
        {
            talkIndex = 0;              // C : ���� Talk()�Լ� ����� ���� talkIndex�� 0���� �ʱ�ȭ
            randomNum = 0;              // C : ���� Talk()�Լ� ����� ���� randomNum�� 0���� �ʱ�ȭ
        }

        if (aiAction.isAICollisionToPlayer) // K : ai�� �浹���̶�� �н���ҿ����� ��ȭ�ϱ⸦ �켱���� �Ѵ�.
        {
            //objData = GameObject.Find("AI").GetComponent<ObjectData>(); // K : ai���� ��ȭ�ϱ⸦ �ϱ� ���� ������Ʈ�� AI�� �����´�.
            talkId = aiObjData.id; // K : ai���� ��ȭ�ϱ⸦ �ϱ� ���� ������Ʈ�� AI�� �����´�.
        } else
        {
            talkId = objData.id;
        }
                     // K : takl data�� id ���� ����, ����ó���� ���� �߰� ������
        if (talkId == 1000)      // C : objData�� AI  
        {
            // N :
            if (randomNum == 1000) talkId = 2000;
            else if (randomNum == 0) // C : ��ȭ ù ����
            {
                if (learningManager.isAILearning) // K : ��ȭ�� �õ� ������, AI �н����� ��� ����ó��
                {
                    talkId = 500;
                    randomNum = 0;
                } else
                {
                    if (dayTalk > 0)
                    {
                        talkId = 2000;                                  // N : �Ϸ翡 �� �� �̻� ��ȭ�� �õ��ϴ� ��� ���� ó��
                        randomNum = 1000;                               // N : ��ȭ �߿� �Ϸ簡 ������ ���ο� ��ȭ�� �Ͼ�� ���� ����
                    }
                    else
                    {
                        System.Random rand = new System.Random();
                        randomNum = rand.Next(1, 18);                  // C : 1~18������ ������ ����
                    }
                }
            }
        }
        else if (objData.id >= 100 && objData.id <= 400)     // C : �н��ϱ⸦ �õ����� ��
        {
            if (learningManager.isAILearning) // K : �н��ϱ� ���縦 ������, AI �н����� ��� ����ó��
            {
                talkId = 500;
            }
            else if (screenManager.currBookNum() < 1) // K : �н��ϱ� ���縦 ������, å�� ���� ��� ����ó��
            {
                talkId = 600;
            }

            // N : ���� MAX���� ���� ó��
            else if (objData.id == 100 && screenManager.farmLv.IsMax() == true) talkId = 2100;
            else if (objData.id == 200 && screenManager.houseLv.IsMax() == true) talkId = 2200;
            else if (objData.id == 300 && screenManager.craftLv.IsMax() == true) talkId = 2300;
            else if (objData.id == 400 && screenManager.engineerLv.IsMax() == true) talkId = 2400;
        }

        Talk(talkId);                   // C : �ʿ��� talkPanel text �� ��������, K : ����ó���� ���� objData.id > talkId�� ����

        if (talkId == 1000) {
            talkPanel.SetActive(true);      // C : talkPanel ����ų� �����ֱ�
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(false); // N : �˸�â �����
        }
        else
        {
            talkPanel.SetActive(false);      // C : talkPanel ����ų� �����ֱ�
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(true); // N : �˸�â ����ֱ�
        }

        if (talkData == null)
        {
            talkPanel.SetActive(false);      // C : talkPanel ����ų� �����ֱ�
            GameObject.Find("Alert").transform.Find("Alert Set").gameObject.SetActive(false); // N : �˸�â ����ֱ�
        }
    }

    // C : ��Ȳ�� ���� �����ϰ� �ʿ��� talkPanel text �� ��ȭâ�� ����
    void Talk(int id)
    {
        // C : ������ object�� �ش��ϴ� talkData �� talkIndex ��ġ�� string�� ��������
        talkData = talkManager.GetTalkData(id + randomNum, talkIndex);

        if (talkData == null || !isSelectedAILearning)           // C : �ش��ϴ� id�� talkData string���� ��� �����Դٸ�
        {
            if (id >= 100 && id <= 400)                          // C : �н��ϱ⿡ ���� talkData�� ��� ������ ���
            {
                if (isSelectedAILearning)                        // C : AI�� �н��� �ؾ��ϴ�/�Ҽ� �ִ� ��Ȳ�̶��
                {
                    learningManager.Learning(id);                // C : ������ �н��� �����Ѵ�.
                }
            }
            else if (id == 1000) screenManager.HeartStudy(0);    // C : AI�� ��ȭ�ϱ⿡ ���� talkData�� ��� ������ ���, ���� �ɷ��� 1 ��½�Ų��.

            isSelectedAILearning = true;
            playerTalk = false;         // J : ���������� special event�� �ߵ��ϵ��� ����
            isTPShow = false;           // C : talkPanel�� show ���� false�� ����
            talkIndex = 0;              // C : ���� Talk()�Լ� ����� ���� talkIndex�� 0���� �ʱ�ȭ
            randomNum = 0;              // C : ���� Talk()�Լ� ����� ���� randomNum�� 0���� �ʱ�ȭ
            if (id == 1000) dayTalk++;  // N : �Ϸ� ��ȭ Ƚ�� ����
            return;
        }

        if (id == 1000) talkText.text = talkData;       // C : talkPanel�� text�� talkData�� ����
        else alertText.text = talkData;                 // N : �˸�â�� text�� talkData�� ����

        isTPShow = true;                // C : talkPanel�� show ���� true�� ���� (�ش��ϴ� id�� talkData string�� ���� ��������)
        talkIndex++;                    // C : �ش��ϴ� id�� ���� talkData string�� �������� ����
    }

    void Start()
    {
        specialEventCoolTimeDay = 10;    // J : ����� �̺�Ʈ ��Ÿ�� ����
    }
}

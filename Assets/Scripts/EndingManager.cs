using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // J : UI ���α׷����� ���� �߰� (Text ��)
using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public GameManager manager;
    public TalkManager talkManager;     // J : ���� ��ȭ ������ �������� ����
    public FadeManager fadeManager;
    public CameraShake cameraShake;     // J : ī�޶� ���� ���� ȣ��

    public GameObject talkPanel;        // J : ��ȭâ
    public TextMeshProUGUI talkText;    // J : ��ȭâ�� text
    public GameObject panel;            // N : ȭ�� ��Ӱ�

    public EffectPlay effect; // K: ȿ���� �̺�Ʈ �߻� ������Ʈ
    private AudioSource bgm; // K: ȿ���� �̺�Ʈ �߻� ������Ʈ

    public int endingCode;              // J : Bad Ending Code
    public int talkIndex;               // J : talkIndex�� �����ϱ� ���� ����

    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // N : �����̽��� �Է� �޴� �ð��� �ֱ� ����
    public void TheEnd()
    {
        manager.isTheEnd = true;
        DataController.Instance.endingData.currentEndingCode = 0;
    }

    // J : ��� ����
    public void BadEnding(int code)
    {
        endingCode = code;
        manager.isEndingShow = true;    // J : ending �����ִ� ��
        DataController.Instance.endingData.firstGame = 0;
        bgm.Stop();     // K : ������� ���� 
        talkPanel.SetActive(true);  // J : ��ȭâ Ȱ��ȭ
        BadEndingTalk();    // J : ��忣�� ��ȭ ����
    }

    // J : ��忣�� ��ȭ
    public void BadEndingTalk()
    {
        string talkData = talkManager.GetTalkData(11000 + endingCode, talkIndex);   // J : TalkManager�κ��� talkData�� ��������
        if (talkData == null)   // J : �ش� talkID�� talkData�� ��� �����Դٸ�
        {
            talkPanel.SetActive(false); // J : ��ȭâ ��Ȱ��ȭ
            if (endingCode == 7 || endingCode == 8) // J : �糭 ������ ��� ī�޶� ����
            {
                cameraShake.Shake(BadEndingShow);
                return;
            }
            BadEndingShow();    // J : ī�� �����ֱ�
            return;
        }
        talkText.text = talkData;       // J : talkPanel�� text�� talkData�� ����
        talkIndex++;                    // J : �ش� talkID�� ���� talkData string�� �������� ����
    }

    // J : ��忣�� ī�� �����ֱ�
    public void BadEndingShow()
    {
        switch (endingCode)
        {
            case 0:
                Debug.Log("Hungry,,,");
                DataController.Instance.endingData.hungry = 1;
                panel.transform.Find("Bad-Hungry").gameObject.SetActive(true);
                break;
            case 1:
                Debug.Log("Lonely,,,");
                DataController.Instance.endingData.lonely = 1;
                panel.transform.Find("Bad-Lonely").gameObject.SetActive(true);
                break;
            case 2:
                Debug.Log("Cold,,,");
                DataController.Instance.endingData.cold = 1;
                panel.transform.Find("Bad-Frozen").gameObject.SetActive(true);
                break;
            case 3: // N : Bad Ending (������)
                Debug.Log("Poison Berry,,,");
                DataController.Instance.endingData.poisonBerry = 1;
                panel.transform.Find("Bad-Berry").gameObject.SetActive(true);
                break;
            case 4: // N : Bad Ending (AI�� �������� ����)
                Debug.Log("�ռҸ���,,,");
                DataController.Instance.endingData.error = 1;
                panel.transform.Find("Bad-Error").gameObject.SetActive(true);
                break;
            case 5: // J : Bad Ending (������)
                Debug.Log("������,,,");
                DataController.Instance.endingData.electric = 1;
                panel.transform.Find("Bad-Electric").gameObject.SetActive(true);
                break;
            case 6: // J : Bad Ending (�����)
                Debug.Log("�����");
                DataController.Instance.endingData.pig = 1;
                panel.transform.Find("Bad-Pig").gameObject.SetActive(true);
                break;
            case 7: // J : Bad Ending (������)
                Debug.Log("������");
                DataController.Instance.endingData.storm = 1;
                panel.transform.Find("Bad-Storm").gameObject.SetActive(true);
                break;
            case 8: // J : Bad Ending (��浹)
                Debug.Log("� �浹");
                DataController.Instance.endingData.space = 1;
                panel.transform.Find("Bad-Space").gameObject.SetActive(true);
                break;
        }
        effect.Play("BadEndingEffect");
        panel.SetActive(true);

        // J : ������ ���� ����
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();
        Invoke("TheEnd", 2.0f);
    }



    // K : ���� ����
    public void timeOutEnding()
    {
        Debug.Log("�׳� ���� ��ҽ��ϴ� ~~");
        DataController.Instance.endingData.timeOut = 1;
        DataController.Instance.endingData.currentEndingCode = 101;
        // J : ������ ���� ����
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : ���̵�ƿ�
    }

    public void successTwo()
    {
        Debug.Log("human�� ai�� �ܵ��� �ູ�ϰ� ��Ҵ�ϴ�");
        DataController.Instance.endingData.two = 1;
        DataController.Instance.endingData.currentEndingCode = 102;
        // J : ������ ���� ����
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : ���̵�ƿ�
    }

    public void successAI()
    {
        Debug.Log("�ٸ� ai�� ������ ai��� �Բ� ��� ��");
        DataController.Instance.endingData.AITown = 1;
        DataController.Instance.endingData.currentEndingCode = 103;
        // J : ������ ���� ����
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : ���̵�ƿ�
    }

    public void successPeople()
    {
        Debug.Log("��ű⸦ ���� �ٸ� �����ڵ��� ����");
        DataController.Instance.endingData.people = 1;
        DataController.Instance.endingData.currentEndingCode = 104;
        // J : ������ ���� ����
        DataController.Instance.SaveSettingData();
        DataController.Instance.SaveEndingData();

        fadeManager.GameFadeOut(LoadHappyScene); // J : ���̵�ƿ�
    }

    // J : ���̵�ƿ� ������ ȣ��
    public void LoadHappyScene()
    {
        SceneManager.LoadScene("HappyEnding");
    }
}


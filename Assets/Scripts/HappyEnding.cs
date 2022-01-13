using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HappyEnding : MonoBehaviour
{
    public GameObject panel;            // N : ȭ�� ��Ӱ�

    [SerializeField] public TextMeshProUGUI dialog; // K : text ������Ʈ�� �޾ƿ��� ���� �����Դϴ�. > using TMPro;          
    public string[] fullText;                       // K : Ÿ���� ȿ���� �� �ؽ�Ʈ�� ��� ���� �迭�Դϴ�

    private int endingCode;                         // K : ���� ���� �ڵ带 ���� ���� �Դϴ�.

    public EffectPlay effect; // K: ȿ���� �̺�Ʈ �߻� ������Ʈ


    // K : synopsys�� �ؽ�Ʈ��(���� ����)�� �迭�Դϴ�.

    private string[] happySosoLifeFullText = {
        "���� 90���� �������..",
        "�̷� �Ϸ��Ϸ絵 �������� �ʳ׿�..",
        "�����ε� �� ��������. �ڻ��.."
    };
    private string[] happyAIFullText = {
        "�ڻ��! �ڻ���� ����ĥ �κ����� �ܶ� ������..",
        "�� ���� �ɷ��� �̷��� �����߳׿�..",
        "������ ���� �ٰ��� �ڻ���� ����Կ�..",
        "�ڻ�ڻ��!!��!! �ڹڻ��!���! �ڻ��!!!"
    };
    private string[] happyPeopleFullText = {
        "�ڻ��!! \n���� ��ű⸦ ��������!!!!",
        "��� ���� ���ݾ� ����� �־��µ�... \n���� �ϼ��̿���!!",
        "���� ���� ��ű⸦ ����غ���!!!",
        "ġ...����....ġ����..��...�����....",
    };
    private string[] happyTwoFullText = {
        "�ڻ��! ���� ������ ���� �ɾ����� �츮 ���� ���������!!",
        "�ڻ�԰��� �Ϸ��Ϸ簡 ���� ��ſ���..",
        "�ڻ�Ե� �׷������� ���ھ��.."
    };


    string subText; // K : synopsys�� �ؽ�Ʈ(�� ����) �Ϻθ� �����ϱ� ���� �����Դϴ�.
    int currentPoint = 0; // K : synopsysFullText���� ���� �����Ͱ� ����ִ��� �����ϱ� ���� �����Դϴ�.

    public bool isTyping = true; // K : ���� ���ڰ� ȭ�鿡 Ÿ���εǰ� �ִ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isSkipPart = false;     // K : ���� ������ Ÿ���� ȿ�� ���� �ٷ� ȭ�鿡 ����� Ȯ���ϱ� ���� �����Դϴ�.
    bool isEnd = false;                 // K : ���ǿ��� ī�尡 �߰� �����ð��� ���� �� ���ǿ��� ���� �����ߴ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isEndingCardShow = false;      // K : ���ǿ��� ī�尡 ȭ�鿡 ��������� Ȯ���ϱ� ���� �����Դϴ�.

    void Start()
    {
        endingCode = DataController.Instance.endingData.currentEndingCode; // K : DataController �� eningData�� ����� ���� �����ڵ带 �����ɴϴ�.
        switch (endingCode) // K : �����ڵ忡 ���� �´� ���� ��ũ��Ʈ�� �����ɴϴ�.
        {
            case 101:
                fullText = happySosoLifeFullText;
                break;
            case 102:
                fullText = happyTwoFullText;
                break;
            case 103:
                fullText = happyAIFullText;
                break;
            case 104:
                fullText = happyPeopleFullText;
                break;
            default:
                Debug.Log("���ǿ��� ����");
                break;
        }
        StartCoroutine("TypingAction", 0);          // K : ��ũ��Ʈ�� ���۰� ���ÿ� �ó�ý��� Ÿ������ �����ϴ� �ڵ��Դϴ�.
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : �����̽��ٸ� ������ ��
        {
            if (!isEndingCardShow)  // K : ���� ���� ī�尡 �������� ���� �������� Ÿ���� ȿ���� �ֱ� ���� �ڵ��Դϴ�.
            {
                if (!isTyping)  // K : ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� ���� ��
                {
                    // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� �ʴ´ٸ�, ���� ���� Ÿ�����ϱ� ���� �ڵ��Դϴ�.
                    StartCoroutine("TypingAction", 0);
                }
                else
                {
                    // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� �ִٸ�, �κ� ��ŵ�� ���� isSkipPart true�� �����ϴ� �ڵ��Դϴ�.
                    isSkipPart = true;
                }
            } 
            if (isEnd) // K : ���ǿ����� ����Ǿ��� ���� ���� ũ���� ������ �Ѿ�� ���� �ڵ��Դϴ�.
            {
                SceneManager.LoadScene("EndingCredits");
            }
        }
    }

    IEnumerator TypingAction()
    {
        if (currentPoint >= fullText.Length)    // K : ��� �ؽ�Ʈ���� Ÿ���� ���� ��
        {           
            ShowHappyEndingCard();              // K : ��� �ؽ�Ʈ�� Ÿ������ �����ϸ� ���ǿ��� ī�带 ���� �Լ��� ȣ���մϴ�.
            StopCoroutine("TypingAction");
            yield return 0;
        }

        dialog.text = "";   // K : Text ������Ʈ�� text �ʱ�ȭ
        isTyping = true;    // K : �ؽ�Ʈ ȭ�鿡 Ÿ������ �����߱� ������, isTyping true

        for (int i = 0; i < fullText[currentPoint].Length; i++) // K : �ؽ�Ʈ �� ������ �� ���� �� ���ڸ� ȭ�鿡 ��Ÿ���� �ϱ� ���� �ݺ���
        {
            yield return new WaitForSeconds(0.07f); // K : �ؽ�Ʈ �� ���� �� ���� ������ ������

            subText += fullText[currentPoint].Substring(0, i);  // K : �ؽ�Ʈ�� �ε��� 0~i���� �ڸ�
            dialog.text = subText;                                      // K : Text ������Ʈ�� subText ���� 
            subText = "";                                               // K : subText �ʱ�ȭ

            if (isSkipPart)                                             // K : �κ� ��ŵ�� true��
            {
                dialog.text = fullText[currentPoint];           // K : Text ������Ʈ�� ��ü �ؽ�Ʈ ������ ����
                isSkipPart = false;                                     // K : isSkipPart �ʱ�ȭ
                isTyping = false;                                       // K : isTyping �ʱ�ȭ
                break;
            }
        }

        isTyping = false;   // isTyping �ʱ�ȭ
        currentPoint++;     // �ؽ�Ʈ �迭�� ������ �ű�
    }



    // N : �����̽��� �Է� �޴� �ð��� �ֱ� ����
    public void TheEnd()
    {
        isEnd = true;                                             // K : ���� ���� ���� �����
        DataController.Instance.endingData.currentEndingCode = 0; // K : DataController �� eningData�� ����� ���� �����ڵ带 �ʱ�ȭ �մϴ�.
    }

    // J : ��� ������ ���� �ڵ�
    private void ending()
    {
        effect.Play("HappyEndingEffect");
        Debug.Log("Game Happy Ending Credits");
        currentPoint = 0;               // K : Ÿ���� ȿ���� �� �迭�� �����͸� 0���� �ʱ�ȭ
        subText = "";                   // K : Ÿ���� ȿ���� �ֱ� ���� �ؽ�Ʈ�� �Ϻ� �� ��� ���� ���� �ʱ�ȭ
        isTyping = false;               // K : Ÿ������ ����
        isSkipPart = false;             // K : Ÿ���� ��ŵ �ʱ�ȭ

        isEndingCardShow = true;        // K : ���� ī�尡 �������

        panel.SetActive(true);                                  // K : ����� ��Ӱ� �ϱ����� �г��� ���ϴ�
        DataController.Instance.endingData.firstGame = 0;      // K : ù��° ������ �ƴ��� Ȯ���ϱ� ���� firstGame�� 0���� �����մϴ�.
    }


    // K : ���� ���� endingCode 101-104
    public void ShowHappyEndingCard()
    {        
        ending();                       // K : ���� ���� �ڵ� �Լ� ȣ��
        switch (endingCode)
        {
            case 101:
                panel.transform.Find("Happy-SosoLife").gameObject.SetActive(true);
                break;
            case 102:
                panel.transform.Find("Happy-Two").gameObject.SetActive(true);
                break;
            case 103:
                panel.transform.Find("Happy-AITown").gameObject.SetActive(true);
                break;
            case 104:
                panel.transform.Find("Happy-People").gameObject.SetActive(true);
                break;
            default:
                Debug.Log("���ǿ��� ī�� ����");
                break;
        }
        Invoke("TheEnd", 2.0f);           // K : ī�带 ��� �� 2���Ŀ� ���� ���� �� ���� �Լ� ȣ��
    }
}

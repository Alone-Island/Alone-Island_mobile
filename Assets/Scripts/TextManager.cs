using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// K : �ؽ�Ʈ�� ȿ���� �ֱ� ���� �Ŵ����Դϴ�.
public class TextManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialog; // K : text ������Ʈ�� �޾ƿ��� ���� �����Դϴ�. > using TMPro;          
    public GameManager manager;
    public EndingManager endingManager;
    private string[] fullText;                       // K : Ÿ���� ȿ���� �� �ؽ�Ʈ�� ��� ���� �迭�Դϴ�

    // K : synopsys�� �ؽ�Ʈ��(���� ����)�� �迭�Դϴ�.
    private string[] synopsysFullText = { 
        "���� �κ������� K...\nAI �κ��� �����ϱ� ����\n�����ǿ����� ���� �ð��� ����... ", 
        "����... �ϼ��ߴ�...\n���� ����, AI �κ� NJ-C!!!!\n���� ���� ������ ���� ��...",
        "�ƴ�...����??",
        "���� ��� �ִ� ������ ���� �Ǿ��־���...",
        "����� ��� �����ְ�..\n������� ���� ������ �ʴ´�!!\n���ڱ� ���ε��� ȥ�� ���� �Ǵٴ�?!?",
        "������ �����Դ� NJ-C�� �־�!\n���� ���뿡 �Ұ��ϴ� �� �κ��� �н���Ų�ٸ�\n������ �� �����ž�!!",
        "�̰����� '3������' ���ߺ���!"
    };

    string subText; // K : synopsys�� �ؽ�Ʈ(�� ����) �Ϻθ� �����ϱ� ���� �����Դϴ�.
    int currentPoint = 0; // K : synopsysFullText���� ���� �����Ͱ� ����ִ��� �����ϱ� ���� �����Դϴ�.

    public bool isTyping = true; // K : ���� ���ڰ� ȭ�鿡 Ÿ���εǰ� �ִ��� Ȯ���ϱ� ���� �����Դϴ�.
    bool isSkipPart = false;     // K : ���� ������ Ÿ���� ȿ�� ���� �ٷ� ȭ�鿡 ����� Ȯ���ϱ� ���� �����Դϴ�.

    void Start() {
        fullText = synopsysFullText;                // K : synopsys�� Ÿ���� ȿ���� �ֱ� ���� fullText�� ��� �ڵ��Դϴ�.

        StartCoroutine("TypingAction", 0);          // K : ��ũ��Ʈ�� ���۰� ���ÿ� �ó�ý��� Ÿ������ �����ϴ� �ڵ��Դϴ�.
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))    // K : �����̽��ٸ� ������ ��
        {

            if (!isTyping)  // K : ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� ���� ��
            {
                // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� �ʴ´ٸ�, ���� ���� Ÿ�����ϱ� ���� �ڵ�
                StartCoroutine("TypingAction", 0);
            }
            else
            {
                // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� �ִٸ�, �κ� ��ŵ�� ���� isSkipPart true�� �����ϴ� �ڵ�
                isSkipPart = true;
            }
        }
    }

    public void GoToGameScreen() // K : ���� ������ ���� �Լ��Դϴ�.
    {
        // K : ��� ���� �ʱ�ȭ
        StopCoroutine("TypingAction");  // K : Ÿ���� �ڷ�ƾ�� ���߱� ���� �ڵ�
        currentPoint = 0;               // K : Ÿ���� ȿ���� �� �迭�� �����͸� 0���� �ʱ�ȭ
        subText = "";                   // K : Ÿ���� ȿ���� �ֱ� ���� �ؽ�Ʈ�� �Ϻ� �� ��� ���� ���� �ʱ�ȭ
        isTyping = false;               // K : Ÿ������ ����
        isSkipPart = false;             // K : Ÿ���� ��ŵ �ʱ�ȭ

        SceneManager.LoadScene("MainGame"); // K : �ó�ý��� ������ ���� ������ ���� �ϴ� �Լ��Դϴ�. > using UnityEngine.SceneManagement
    }

    IEnumerator TypingAction() {
        if (currentPoint >= fullText.Length)    // K : ��� �ؽ�Ʈ���� Ÿ���� ���� ��
        {
            Debug.Log("Game Start"); // K : ��� �ؽ�Ʈ�� ��� �Ϸ�, ���� �÷��� ������ �̵�
            GoToGameScreen();
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

    // J : ȭ�� ��ġ�ϸ� ����
    public void ScreenTouch()
    {
        if (!isTyping)  // K : ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� ���� ��
        {
            // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� ���� �ʴ´ٸ�, ���� ���� Ÿ�����ϱ� ���� �ڵ�
            StartCoroutine("TypingAction", 0);
        }
        else
        {
            // K : �����̽��ٸ� ������ �� ���� ���ڰ� ȭ�鿡 Ÿ���� �ǰ� �ִٸ�, �κ� ��ŵ�� ���� isSkipPart true�� �����ϴ� �ڵ�
            isSkipPart = true;
        }
    }
}

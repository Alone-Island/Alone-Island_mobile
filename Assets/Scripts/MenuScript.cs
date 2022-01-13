using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;     // J : ���ӹ�� â
    public GameObject setting;  // J : ����â
    public GameObject scrollView;   // J : ����ī��â

    public GameObject BGM;          // J : ������� on/off�� ���� BGM object ������

    public GameObject CardButton;  // J : Windows->CardButton
    public GameObject scrollEndingCards;  // J : Scroll View->Viewport->Content
    public Sprite badCard;
    public Sprite happyCard;
    public Sprite unknownCard;

    private GameObject currentCard;

    public GameObject background;


    // J : �����ϱ� ��ư onclick
    public void SelectStart()
    {
        Debug.Log("�����ϱ�");
        if (DataController.Instance.endingData.firstGame == 1) // J : ù�����̸�
            SceneManager.LoadScene("Synopsis"); // J : Synopsis scene���� �̵�
        else
            SceneManager.LoadScene("MainGame"); // J : MainGame scene���� �̵�
    }

    // J : ���ӹ�� ��ư onclick
    public void SelectRule()
    {
        Debug.Log("���ӹ��");
        if (gameRule.activeSelf)
        {
            gameRule.SetActive(false);   // J : ���ӹ��â ��Ȱ��ȭ

            background.SetActive(false);
        } else
        {
            gameRule.SetActive(true);   // J : ���ӹ��â Ȱ��ȭ
            SelectEndingCardQuit();
            SelectSettingQuit();
            background.SetActive(true);
        }
    }

    // J : ���ӹ�� ������ ��ư onclick
    public void SelectRuleQuit()
    {
        Debug.Log("���ӹ�� ������");
        gameRule.SetActive(false);  // J : ���ӹ��â ��Ȱ��ȭ
        background.SetActive(false);
    }

    // J : ����â ������ ��ư onclick
    public void SelectSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);// J : ����â ��Ȱ��ȭ
            background.SetActive(false);
        }
        else
        {
            setting.SetActive(true);// J : ����â Ȱ��ȭ
            SelectEndingCardQuit();
            SelectRuleQuit();
            background.SetActive(true);
        }
    }

    // J : ����â ������ ��ư onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : ����â ��Ȱ��ȭ
        background.SetActive(false);

    }

    // J : �������� ��ư onclick
    public void SelectGameQuit()
    {
        Debug.Log("��������");
        Application.Quit(); // J : ���α׷� ����
    }

    // J : ����ī�� ��ư onclick
    public void SelectEndingCard()
    {
        Debug.Log("����ī��");
        scrollView.SetActive(true);
        SelectRuleQuit();
        SelectSettingQuit();
        background.SetActive(true);

        Image card;

        // J : BadLine0 ����ī��
        if (DataController.Instance.endingData.hungry == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("hungry").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("hungry").transform.Find("hungry").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.lonely == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("lonely").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("lonely").transform.Find("lonely").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.cold == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("cold").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine0").transform.Find("cold").transform.Find("cold").gameObject.SetActive(false);
        }

        // J : BadLine1 ����ī��
        if (DataController.Instance.endingData.poisonBerry == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("poisonBerry").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("poisonBerry").transform.Find("poisonBerry").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.error == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("error").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("error").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("error").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("error").transform.Find("error").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.electric == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("electric").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("electric").transform.Find("electric").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.pig == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("pig").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("pig").transform.Find("pig").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.storm == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("storm").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine1").transform.Find("storm").transform.Find("storm").gameObject.SetActive(false);
        }

        // J : BadLine2 ����ī��
        if (DataController.Instance.endingData.space == 1)
        {
            card = scrollEndingCards.transform.Find("BadLine2").transform.Find("space").GetComponent<Image>();
            card.sprite = badCard;
            scrollEndingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("space").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("BadLine2").transform.Find("space").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("BadLine2").transform.Find("space").transform.Find("space").gameObject.SetActive(false);
        }

        // J : HappyLine0 ����ī��
        if (DataController.Instance.endingData.timeOut == 1)
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").GetComponent<Image>();
            card.sprite = happyCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("timeOut").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("timeOut").transform.Find("timeOut").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.two == 1)
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").GetComponent<Image>();
            card.sprite = happyCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("two").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("two").transform.Find("two").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.AITown == 1)
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").GetComponent<Image>();
            card.sprite = happyCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("AITown").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("AITown").transform.Find("AITown").gameObject.SetActive(false);
        }

        if (DataController.Instance.endingData.people == 1)
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").GetComponent<Image>();
            card.sprite = happyCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("Image").gameObject.SetActive(false);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("people").gameObject.SetActive(true);
        }
        else
        {
            card = scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").GetComponent<Image>();
            card.sprite = unknownCard;
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("Image").gameObject.SetActive(true);
            scrollEndingCards.transform.Find("HappyLine0").transform.Find("people").transform.Find("people").gameObject.SetActive(false);
        }
    }

    // J : ��ũ�Ѻ信�� ����� ī�� Ŭ��
    public void BadLine00()
    {
        if (DataController.Instance.endingData.hungry == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Hungry").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� �ܷο� ī�� Ŭ��
    public void BadLine01()
    {
        if (DataController.Instance.endingData.lonely == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Lonely").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� �߿� ī�� Ŭ��
    public void BadLine02()
    {
        if (DataController.Instance.endingData.cold == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Frozen").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ������ ī�� Ŭ��
    public void BadLine10()
    {
        if (DataController.Instance.endingData.poisonBerry == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Berry").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ���� ī�� Ŭ��
    public void BadLine11()
    {
        if (DataController.Instance.endingData.error == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Error").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ������ ī�� Ŭ��
    public void BadLine12()
    {
        if (DataController.Instance.endingData.electric == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Electric").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ����� ī�� Ŭ��
    public void BadLine13()
    {
        if (DataController.Instance.endingData.pig == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Pig").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ��ǳ�� ī�� Ŭ��
    public void BadLine14()
    {
        if (DataController.Instance.endingData.storm == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Storm").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ��浹 ī�� Ŭ��
    public void BadLine20()
    {
        if (DataController.Instance.endingData.space == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Space").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� �׷����� ī�� Ŭ��
    public void HappyLine00()
    {
        if (DataController.Instance.endingData.timeOut == 1)
        {
            currentCard = CardButton.transform.Find("Happy-SosoLife").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ���̼� �ູ ī�� Ŭ��
    public void HappyLine01()
    {
        if (DataController.Instance.endingData.two == 1)
        {
            currentCard = CardButton.transform.Find("Happy-Two").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� AI Town ī�� Ŭ��
    public void HappyLine02()
    {
        if (DataController.Instance.endingData.AITown == 1)
        {
            currentCard = CardButton.transform.Find("Happy-AITown").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ��ũ�Ѻ信�� ��� ī�� Ŭ��
    public void HappyLine03()
    {
        if (DataController.Instance.endingData.people == 1)
        {
            currentCard = CardButton.transform.Find("Happy-People").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : ���� ���̴� ī�� Ŭ��
    public void SelectCurrentCard()
    {
        currentCard.SetActive(false);   // J : ī�� ��Ȱ��ȭ
    }

    // J : ����ī�� ������ ��ư onclick
    public void SelectEndingCardQuit()
    {
        Debug.Log("����ī�� ������");
        if (currentCard != null)
            currentCard.SetActive(false);
        background.SetActive(false);
        scrollView.SetActive(false);  // J : ���ӹ��â ��Ȱ��ȭ
    }

}

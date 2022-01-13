using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject gameRule;     // J : 게임방법 창
    public GameObject setting;  // J : 설정창
    public GameObject scrollView;   // J : 엔딩카드창

    public GameObject BGM;          // J : 배경음악 on/off를 위해 BGM object 가져옴

    public GameObject CardButton;  // J : Windows->CardButton
    public GameObject scrollEndingCards;  // J : Scroll View->Viewport->Content
    public Sprite badCard;
    public Sprite happyCard;
    public Sprite unknownCard;

    private GameObject currentCard;

    public GameObject background;


    // J : 시작하기 버튼 onclick
    public void SelectStart()
    {
        Debug.Log("시작하기");
        if (DataController.Instance.endingData.firstGame == 1) // J : 첫게임이면
            SceneManager.LoadScene("Synopsis"); // J : Synopsis scene으로 이동
        else
            SceneManager.LoadScene("MainGame"); // J : MainGame scene으로 이동
    }

    // J : 게임방법 버튼 onclick
    public void SelectRule()
    {
        Debug.Log("게임방법");
        if (gameRule.activeSelf)
        {
            gameRule.SetActive(false);   // J : 게임방법창 비활성화

            background.SetActive(false);
        } else
        {
            gameRule.SetActive(true);   // J : 게임방법창 활성화
            SelectEndingCardQuit();
            SelectSettingQuit();
            background.SetActive(true);
        }
    }

    // J : 게임방법 나가기 버튼 onclick
    public void SelectRuleQuit()
    {
        Debug.Log("게임방법 나가기");
        gameRule.SetActive(false);  // J : 게임방법창 비활성화
        background.SetActive(false);
    }

    // J : 설정창 나가기 버튼 onclick
    public void SelectSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);// J : 설정창 비활성화
            background.SetActive(false);
        }
        else
        {
            setting.SetActive(true);// J : 설정창 활성화
            SelectEndingCardQuit();
            SelectRuleQuit();
            background.SetActive(true);
        }
    }

    // J : 설정창 나가기 버튼 onclick
    public void SelectSettingQuit()
    {
        setting.SetActive(false);   // J : 설정창 비활성화
        background.SetActive(false);

    }

    // J : 게임종료 버튼 onclick
    public void SelectGameQuit()
    {
        Debug.Log("게임종료");
        Application.Quit(); // J : 프로그램 종료
    }

    // J : 엔딩카드 버튼 onclick
    public void SelectEndingCard()
    {
        Debug.Log("엔딩카드");
        scrollView.SetActive(true);
        SelectRuleQuit();
        SelectSettingQuit();
        background.SetActive(true);

        Image card;

        // J : BadLine0 엔딩카드
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

        // J : BadLine1 엔딩카드
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

        // J : BadLine2 엔딩카드
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

        // J : HappyLine0 엔딩카드
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

    // J : 스크롤뷰에서 배고픔 카드 클릭
    public void BadLine00()
    {
        if (DataController.Instance.endingData.hungry == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Hungry").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 외로움 카드 클릭
    public void BadLine01()
    {
        if (DataController.Instance.endingData.lonely == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Lonely").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 추움 카드 클릭
    public void BadLine02()
    {
        if (DataController.Instance.endingData.cold == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Frozen").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 독열매 카드 클릭
    public void BadLine10()
    {
        if (DataController.Instance.endingData.poisonBerry == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Berry").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 에러 카드 클릭
    public void BadLine11()
    {
        if (DataController.Instance.endingData.error == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Error").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 감전사 카드 클릭
    public void BadLine12()
    {
        if (DataController.Instance.endingData.electric == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Electric").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 멧돼지 카드 클릭
    public void BadLine13()
    {
        if (DataController.Instance.endingData.pig == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Pig").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 폭풍우 카드 클릭
    public void BadLine14()
    {
        if (DataController.Instance.endingData.storm == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Storm").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 운석충돌 카드 클릭
    public void BadLine20()
    {
        if (DataController.Instance.endingData.space == 1)
        {
            currentCard = CardButton.transform.Find("Bad-Space").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 그럭저럭 카드 클릭
    public void HappyLine00()
    {
        if (DataController.Instance.endingData.timeOut == 1)
        {
            currentCard = CardButton.transform.Find("Happy-SosoLife").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 둘이서 행복 카드 클릭
    public void HappyLine01()
    {
        if (DataController.Instance.endingData.two == 1)
        {
            currentCard = CardButton.transform.Find("Happy-Two").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 AI Town 카드 클릭
    public void HappyLine02()
    {
        if (DataController.Instance.endingData.AITown == 1)
        {
            currentCard = CardButton.transform.Find("Happy-AITown").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 스크롤뷰에서 통신 카드 클릭
    public void HappyLine03()
    {
        if (DataController.Instance.endingData.people == 1)
        {
            currentCard = CardButton.transform.Find("Happy-People").gameObject;
            currentCard.SetActive(true);
        }
    }

    // J : 현재 보이는 카드 클릭
    public void SelectCurrentCard()
    {
        currentCard.SetActive(false);   // J : 카드 비활성화
    }

    // J : 엔딩카드 나가기 버튼 onclick
    public void SelectEndingCardQuit()
    {
        Debug.Log("엔딩카드 나가기");
        if (currentCard != null)
            currentCard.SetActive(false);
        background.SetActive(false);
        scrollView.SetActive(false);  // J : 게임방법창 비활성화
    }

}

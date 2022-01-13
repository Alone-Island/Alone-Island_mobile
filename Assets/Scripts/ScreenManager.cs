using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public PlayerStat hungerStat; // N : ����� ����
    public PlayerStat happyStat; // N : �ູ ����
    public PlayerStat temperatureStat; // N : ü�� ����
    public PlayerStat dangerStat; // N : ���� ����

    public AILevel farmLv; // N : ��� ����
    public AILevel houseLv; // N : ���� ����
    public AILevel craftLv; // N : ���� ����
    public AILevel engineerLv; // N : ���� ����
    public AILevel heartLv; // N : ���� ����

    public LearningManager learningManager;
    public TextMeshProUGUI learningTime;
    public TextMeshProUGUI learningTitle;

    private int day = 0; // N : ��¥
    public int dayTime = 0; // N : �Ϸ��� �ð�

    //private int bookNum = 0; // N : å ����
    [SerializeField] private TextMeshProUGUI calender; // N : ��¥ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI book; // N : å ���� �ؽ�Ʈ
    [SerializeField] private int bookNum;

    public EndingManager endingManager;
    public GameManager gameManager;     // J : GameManager���� �Ϸ簡 �������� ������
    public SpecialEventManager specialManager;
    public SettingManager settingManager;

    // C : ���� �ɷ� ��� �� ������ �ִϸ��̼��� ����
    public GameObject heartTextObject;      // C : ���� ���� ���� +1 �ִϸ��̼��� �����ϱ� ���� �ʿ��� ����
    public GameObject levelUp;              // C : AI�� ��ȭ�ϱ� �Ϸ� ��, ���� ���� ���� ��� ������ �ִϸ��̼��� ������ game object ����
    public GameObject levelDown;            // C : ����� �̺�Ʈ���� �������� �������� ����� ��, ���� ���� ���� ��� �����ٿ� �ִϸ��̼��� ������ game object ����
    //private float time = 0;               // C :
    private float downTime = 0;             // C : �����ٿ� �ִϸ��̼��� ����� �ð��� �����ϱ� ���� ����

    private Color currColor = new Color(1f, 1f, 1f, 0f);        // N :
    Transform nextMap;                                          // N :
    Transform preMap;                                           // N :

    // Start is called before the first frame update
    void Start()
    {
        // N : ���� �ʱ�ȭ
        hungerStat.initStat(100, 100);
        happyStat.initStat(50, 100);
        temperatureStat.initStat(100, 100);
        //dangerStat.initStat(100, 100);

        // N : ���� �ʱ�ȭ
        farmLv.initLv(1, 10);
        houseLv.initLv(1, 10);
        craftLv.initLv(1, 10);
        engineerLv.initLv(1, 15);
        heartLv.initLv(1, 20);

        // N : Ķ���� �ʱ�ȭ
        calender.text = "day 01";
        // N : å ���� �ʱ�ȭ
        book.text = "0 books";

        // N : ��� �� �ʱ�ȭ
        GameObject.Find("Farm").transform.Find("Lv3-4").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv5-6").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv7-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Farm").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv3-5").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv6-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("House").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Craft_Room").transform.Find("Lv4-7").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Craft_Room").transform.Find("Lv8-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv3-5").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv6-8").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("Lab").transform.Find("Lv9-10").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("plant").transform.Find("Lv8-14").GetComponent<Renderer>().material.color = currColor;
        GameObject.Find("plant").transform.Find("Lv15-20").GetComponent<Renderer>().material.color = currColor;

        dayAfter();
        //Invoke("dayAfter", gameManager.day);
    }

    // Update is called once per frame
    void Update()
    {
        // N :learningTime 
        if(learningManager.isAILearning)
        {
            learningTime.text = learningManager.learningTime.ToString();
            learningTitle.alpha = 1;
        } else
        {
            learningTime.text = "";
            learningTitle.alpha = 0;
        }


        /*
        // C :
        if (levelUp.activeSelf == true)     // C :
        {
            time += Time.deltaTime;
            if (time > 2f)                      // C : 
            {
                levelUp.SetActive(false);
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
        */

        // C : �����ٿ� �ִϸ��̼��� ȭ�� ���� �ð��� ����
        if (levelDown.activeSelf == true)           // C : �����ٿ� object�� active ���°� true�̸�
        {
            downTime += Time.deltaTime;             // C : �����ٿ� �ִϸ��̼� Ÿ�ӿ� �帥 �ð� �߰�
            if (downTime > 2f)                      // C : �����ٿ� �ִϸ��̼� Ÿ���� 2�ʰ� ������ ��
            {
                levelDown.SetActive(false);         // C : �����ٿ� object�� active ���¸� false�� ��ȯ
                downTime = 0;                       // C : ���� �ִϸ��̼��� ���� �����ٿ� �ִϸ��̼� Ÿ���� 0���� �ʱ�ȭ
            }
        }
        else                                        // C : �����ٿ� object�� active ���°� false�̸�
        {
            downTime = 0;                           // C : ���� �ִϸ��̼��� ���� �����ٿ� �ִϸ��̼� Ÿ���� 0���� �ʱ�ȭ
        }
    }

    // N : ��¥ ��ȭ
    public void dayAfter()
    {
        if (gameManager.isEndingShow) return;
        // N : Ķ���� ����

        day++;
        if (day < 10) calender.text = "day " + "0" + day.ToString();
        else calender.text = "day " + day.ToString();

        // N : 90�� ����
        if (day >= 90)
        {
            endingManager.timeOutEnding();
            return;
        }

        // J : ����� �̺�Ʈ �ֱ⸶�� ����� �̺�Ʈ �ߵ�
        if (day % gameManager.specialEventCoolTimeDay == 0)
            specialManager.StartCoroutine("Check");

        if (day > 1)
        {
            // N : ���� ����
            hungerStat.fCurrValue = hungerStat.fCurrValue + farmLv.fCurrValue - 10;
            happyStat.fCurrValue = happyStat.fCurrValue + heartLv.fCurrValue - 5;
            temperatureStat.fCurrValue = temperatureStat.fCurrValue + craftLv.fCurrValue - 10;
        }        

        // N : ���� ó��
        if (hungerStat.fCurrValue <= 0) endingManager.BadEnding(0);
        else if (happyStat.fCurrValue <= 0) endingManager.BadEnding(1);
        else if (temperatureStat.fCurrValue <= 0) endingManager.BadEnding(2);

        //N : AI�� ��ȭ Ƚ�� �ʱ�ȭ
        gameManager.dayTalk = 0;

        timeFly();
    }

    public void timeFly()
    {
        // J : �����ϴ� ���̳� ����� �̺�Ʈ �߿��� �ð��� �帣�� ����
        if(!settingManager.nowSetting && !specialManager.special) dayTime++;

        if (dayTime >= gameManager.day) { dayTime = 0; dayAfter(); }
        else Invoke("timeFly", 1);
    }

    public int currBookNum()
    {
        return bookNum;
    }

    // N : å �ݱ�
    public void getBook()
    {
        bookNum++;
        book.text = bookNum.ToString() + " books";
    }

    // N : å ����
    public void useBook()
    {
        bookNum--;
        book.text = bookNum.ToString() + " books";
    }

    // N : ��� ����
    public void FarmStudy()
    {
        farmLv.fCurrValue++;
        hungerStat.fCurrValue += 50;
    }

    // N : ���� ����
    public void HouseStudy()
    {
        houseLv.fCurrValue++;
        //dangerStat.fCurrValue += 50;
    }

    // N : ���� ����
    public void CraftStudy()
    {
        craftLv.fCurrValue++;
        temperatureStat.fCurrValue += 50;

        
    }

    // N : ���� ����
    public void EngineerStudy()
    {
        engineerLv.fCurrValue++;
               

        // N : ���� ó��
        if (engineerLv.fCurrValue >= engineerLv.maxValue)
        {
            if (heartLv.fCurrValue > 17.0f) endingManager.successPeople(); // N : ���� �ɷ��� ���� ���
            else endingManager.successAI(); // N : ���� �ɷ��� ���� ���
        }
    }

    // N : ���� ���� (���ɱ� n = 0)
    public void HeartStudy(int n)
    {
        if (n == 0)
        {
            //useBook();
            heartLv.fCurrValue++;
            happyStat.fCurrValue += 5;
        }
        else
        {
            heartLv.fCurrValue += n;
            happyStat.fCurrValue += (5 * n);
        }

        // C : levelUp animation �����ϱ�
        if (n >= 0)
        {
            levelUp.transform.SetParent(heartTextObject.transform);
            levelUp.SetActive(true);
        }
        else
        {
            levelDown.SetActive(true);
        }

        // C : ���� �ɷ� ������ ���� �� ��ȭ
        HeartLevelUpAnimation();

        // N : ���� ó��
        if (happyStat.fCurrValue < 0 || heartLv.fCurrValue < 0) endingManager.BadEnding(1);
        else if (heartLv.fCurrValue >= heartLv.maxValue)
        {
            if (engineerLv.fCurrValue > 13.0f) endingManager.successPeople(); // N : ���� �ɷ��� ���� ���
            else endingManager.successTwo(); // N : ���� �ɷ��� ���� ���
        }
    }

    // N, K : ��� �н��� ������ �ִϸ��̼�
    public void FarmLevelUpAnimation()
    {
        if (farmLv.fCurrValue == 8)
        {
            // N : ���� 9-10
            preMap = GameObject.Find("Farm").transform.Find("Lv7-8");
            nextMap = GameObject.Find("Farm").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 6)
        {
            // N : ���� 7-8
            preMap = GameObject.Find("Farm").transform.Find("Lv5-6");
            nextMap = GameObject.Find("Farm").transform.Find("Lv7-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 4)
        {
            // N : ���� 5-6
            preMap = GameObject.Find("Farm").transform.Find("Lv3-4");
            nextMap = GameObject.Find("Farm").transform.Find("Lv5-6");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (farmLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("Farm").transform.Find("Lv3-4");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
        
    }

    // N, K : ���� �н��� ������ �ִϸ��̼�
    public void HouseLevelUpAnimation()
    {
        if (houseLv.fCurrValue == 8)
        {
            // N : ���� 9-10
            preMap = GameObject.Find("House").transform.Find("Lv6-8");
            nextMap = GameObject.Find("House").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (houseLv.fCurrValue == 5)
        {
            // N : ���� 6-8
            preMap = GameObject.Find("House").transform.Find("Lv3-5");
            nextMap = GameObject.Find("House").transform.Find("Lv6-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (houseLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("House").transform.Find("Lv3-5");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }
    // N, K : ���� �н��� ������ �ִϸ��̼�
    public void CraftLevelUpAnimation()
    {
        if (craftLv.fCurrValue == 7)
        {
            // N : ���� 8-10
            preMap = GameObject.Find("Craft_Room").transform.Find("Lv4-7");
            nextMap = GameObject.Find("Craft_Room").transform.Find("Lv8-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (craftLv.fCurrValue == 3)
        {
            preMap = GameObject.Find("Craft_Room").transform.Find("Lv1-3");
            nextMap = GameObject.Find("Craft_Room").transform.Find("Lv4-7");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
    }
    // N, K : ���� �н��� ������ �ִϸ��̼�
    public void EngineerLevelUpAnimation()
    {
        if (engineerLv.fCurrValue == 8)
        {
            // N : ���� 9-10
            preMap = GameObject.Find("Lab").transform.Find("Lv6-8");
            nextMap = GameObject.Find("Lab").transform.Find("Lv9-10");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (engineerLv.fCurrValue == 5)
        {
            // N : ���� 6-8
            preMap = GameObject.Find("Lab").transform.Find("Lv3-5");
            nextMap = GameObject.Find("Lab").transform.Find("Lv6-8");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (engineerLv.fCurrValue == 2)
        {
            nextMap = GameObject.Find("Lab").transform.Find("Lv3-5");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }
    // N, K : ���� �н��� ������ �ִϸ��̼�
    public void HeartLevelUpAnimation()
    {
        if (heartLv.fCurrValue == 14)
        {
            // N : ���� 8-14
            preMap = GameObject.Find("plant").transform.Find("Lv8-14");
            nextMap = GameObject.Find("plant").transform.Find("Lv15-20");
            currColor.a = 1;
            StartCoroutine("fadeOut");
        }
        else if (heartLv.fCurrValue == 7)
        {
            nextMap = GameObject.Find("plant").transform.Find("Lv8-14");
            currColor.a = 0;
            StartCoroutine("fadeIn");
        }
    }

    IEnumerator fadeIn()
    {
        nextMap.gameObject.SetActive(true); // J : ���� ������ �� Ȱ��ȭ
        while (currColor.a < 1f)
        {
            currColor.a += 0.1f;
            nextMap.GetComponent<Renderer>().material.color = currColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator fadeOut()
    {
        while (currColor.a > 0f)
        {
            currColor.a -= 0.1f;
            preMap.GetComponent<Renderer>().material.color = currColor;
            yield return new WaitForSeconds(0.1f);
        }
        preMap.gameObject.SetActive(false); // J : ���� ������ �� ��Ȱ��ȭ
        StartCoroutine("fadeIn");
    }
}

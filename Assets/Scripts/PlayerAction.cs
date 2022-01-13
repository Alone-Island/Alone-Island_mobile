using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

// C : Dr.Kim ������Ʈ�� ��� Action�� ���õ� ��ɵ��� ����ִ� ��ũ��Ʈ
public class PlayerAction : MonoBehaviour
{
    public float speed;     // C : Dr.Kim �̵� �ӷ�
    public GameManager manager;         // C : player���� GameManager�� �Լ��� ȣ���� �� �ֵ��� manager ���� ����
    public ScreenManager screenManager;         // J : å�� �ֿ��� �� å ���� ������ ���� ScreenManager ���� ����
    public SpecialEventManager specialManager;  // J : player���� SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����
    public EndingManager endingManager;         // J : EndingTalk ȣ���� �� �ֵ��� endingManager ���� ����
    public SettingManager settingManager;       // J : ����â Ȱ��ȭ �߿��� �÷��̾ ������ �� ���� settingManager ���� ����

    private AIAction aiAction;

    float h;    // C : horizontal (���� �̵�)
    float v;    // C : vertical (���� �̵�)
    bool isHorizonMove;     // C : ���� �̵��̸� true, ���� �̵��̸� false
    Vector3 dirVec;     // C : ���� �ٶ󺸰� �ִ� ���� ��
    GameObject scanObject;  // C : ��ĵ�� game object

    //N : �н��ϱ� �ȳ� �����ܵ�
    public GameObject farmIcon;
    public GameObject houseIcon;
    public GameObject craftIcon;
    public GameObject engineerIcon;

    // C : å�� ã���� �� 'book + 1' �ִϸ��̼��� �ֱ� ���� ������
    public GameObject addBook;      // C : 'book + 1' object ����
    //private float time = 0;       // C :
    public GameObject player;       // C : �÷��̾� object ����
    private List<GameObject> addBookListG = new List<GameObject>();      // C : addBook object�� ���� ����Ʈ
    private List<float> addBookListT = new List<float>();                // C : addBook�� �ִϸ��̼� �ð��� ���� ����Ʈ
    public EffectPlay effect; // K: ȿ���� �̺�Ʈ �߻� ������Ʈ

    // C : ������̺�Ʈ �߻� �� '!' ������Ʈ�� ����ֱ� ���� ������
    public GameObject alarm;        // C : '!' object ����
    //private float alarmEffectTime = 0;
    //int i = 1;

    Rigidbody2D rigid;  // C : ���� ����
    Animator anim;      // C : �ִϸ��̼� ����

    void Awake()
    {
        // C : component instance ����
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        aiAction = GameObject.Find("AI").GetComponent<AIAction>();
    }

    void Update()
    {
        // J : ����â Ȱ��ȭ ���¸� action X
        if (settingManager.nowSetting)
            return;
        // C : �Էµ� ����/���� �̵��� ���� (-1, 0, 1)
        // C : GameManager�� isTPShow�� ����Ͽ� talkPanel�� �������� ���� ��
        // J : or SpecialEventManager�� special�� ����Ͽ� ����� �̺�Ʈ ���� ���� ��� �÷��̾��� �̵��� ����
        // N :
        h = manager.isTPShow || specialManager.special || manager.isEndingShow ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isTPShow || specialManager.special || manager.isEndingShow ? 0 : Input.GetAxisRaw("Vertical");

        // C : Ű���� �Է�(down, up)�� horizontal���� vertical���� Ȯ��
        // C : GameManager�� isTPShow�� ����Ͽ� talkPanel�� �������� ���� ��
        // J : or SpecialEventManager�� special�� ����Ͽ� ����� �̺�Ʈ ���� ���� ��� �÷��̾��� �̵��� ����
        // N :
        bool hDown = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonDown("Horizontal");
        bool hUp = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonUp("Horizontal");
        bool vDown = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonDown("Vertical");
        bool vUp = manager.isTPShow || specialManager.special || manager.isEndingShow ? false : Input.GetButtonUp("Vertical");

        // C : isHorizonMove �� ����
        if (hDown)           // C : ���� Ű�� ������ isHorizonMove�� true
            isHorizonMove = true;
        else if (vDown)      // C : ���� Ű�� ������ isHorizonMove�� false
            isHorizonMove = false;
        else if (hUp || vUp)        // C : ���� Ű�� ���� Ű�� �� ��(e.g.(<- && ->))�� �� �� ������ ���� ���� ���
            isHorizonMove = h != 0;

        // C : Animation - moving
        if (anim.GetInteger("hAxisRaw") != h)       // C : "hAxisRaw" ���� ���� h ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    // C : animation "hAxisRaw" parameter �� ����
        }
        else if (anim.GetInteger("vAxisRaw") != v)  // C : "vAxisRaw" ���� ���� v ���� �ٸ� ��
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);    // C : animation "vAxisRaw" parameter �� ����
        }
        else
            anim.SetBool("isChange", false);        // C : ���� ��ȭ�� ���� animation parameter ���� false�� ����

        // C : dirVec(���� �ٶ󺸰� �ִ� ����) �� ����
        if (vDown && v == 1)                // C : ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.up;            // C : dirVec ���� up
        else if (vDown && v == -1)          // C : ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.down;          // C : dirVec ���� down
        if (hDown && h == -1)               // C : ���� Ű�� ������, �Էµ� ���� ���� -1�̸�
            dirVec = Vector3.left;          // C : dirVec ���� left
        if (hDown && h == 1)                // C : ���� Ű�� ������, �Էµ� ���� ���� 1�̸�
            dirVec = Vector3.right;         // C : dirVec ���� right

        // J : �����̽��� ����
        if (Input.GetButtonDown("Jump"))
        {
            if (specialManager.special)     // J : ����� �̺�Ʈ ���� ��
            {
                if (specialManager.specialTalk)  // J : �������� �߱� ���̶��
                    specialManager.Talk();  // J : specialManager�� Talk �Լ� ȣ��
                else if (specialManager.resultTalk)     // J : ������ Ŭ���� �� (����� �̺�Ʈ ������)
                    specialManager.ResultTalk();    // J : ��� �ؽ�Ʈ �����ֱ�
            }
            else if (manager.isEndingShow)  // J : ���� �����ִ� ��
                endingManager.BadEndingTalk();  // J : ���� ��ȭ �����ֱ�
            else if (scanObject != null)        // J : ����� �̺�Ʈ ���� ���� �ƴϰ� scanObject�� ������
                manager.Action(scanObject);     // C : ���� ��ȭâ�� ������ �޼����� �� �� �ֵ��� Action()�Լ� ����
            else    // J : �ƹ� ���µ� �ƴϰų� å ã�Ҵٴ� ��ȭâ�� �� ����..
                manager.talkPanel.SetActive(false); // J : ��ȭâ ����

            // N : ���� ũ�������� ����
            // N : ���߿� ��ư ���� Ŭ������ ó���ϸ� ���� �� ����.
            if (manager.isTheEnd)
            {
                SceneManager.LoadScene("GameMenu"); // K : ��忣���� ������ �ٷ� ���� �޴��� ���ư��ϴ�., ���ǿ����� textmanager���� ó����
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (scanObject != null)
            {
                ObjectData objData = scanObject.GetComponent<ObjectData>();
                if (objData.id >= 100 && objData.id <= 400)
                {
                    manager.isSelectedAILearning = false;
                    manager.Action(scanObject);
                }                    
            }
        }
       
        // C : å�� ã���� �� å �߰� �ִϸ��̼� �ð� ������ ����
        // C : å �߰� �ִϸ��̼��� ��� addBook object�� ������ ���
        for (int i = 0; i < addBookListG.Count; i++)
        {
            // C : �ش� addBook object�� active ���°� true�� ���
            if (addBookListG[i].activeSelf == true)
            {
                addBookListT[i] += Time.deltaTime;      // C : �ش� addBook object �ִϸ��̼� Ÿ�ӿ� �帥 �ð� �߰�
                if (addBookListT[i] > 2f)               // C : �ش� addBook object �ִϸ��̼� Ÿ���� 2�ʰ� ������ ��
                {
                    addBookListT[i] = 0;                // C : �ش� addBook object �ִϸ��̼� Ÿ���� 0���� �ʱ�ȭ
                    addBookListG[i].SetActive(false);   // C : �ش� addBook object�� active ���¸� false�� ��ȯ
                    Destroy(addBookListG[i]);           // C : �ش� addBook object�� ����
                    addBookListG.RemoveAt(i);           // C : addBook�� ��� ����Ʈ���� �ش� addBook object ����
                    addBookListT.RemoveAt(i);           // C : addBook �ִϸ��̼� �ð��� ��� ����Ʈ���� �ش� addBook �ִϸ��̼� �ð� ���� ����
                }
            }
        }


        
        /*IEnumerator coroutine = OnAlarm();
        if(i == 1)
        {
            StartCoroutine(coroutine);
            i--;
        }*/
        /*if (i == 1)
        {
            OnAlarm();
            i--;
        }*/
        /*
        // C : alarm �ִϸ��̼��� ȭ�� ���� �ð��� ����
        if (alarm.activeSelf == true)                 // C : object�� active ���°� true�̸�
        {
            alarmEffectTime += Time.deltaTime;        // C : �ִϸ��̼� Ÿ�ӿ� �帥 �ð� �߰�
            if (alarmEffectTime > 3.3f)                 // C : �ִϸ��̼� Ÿ���� 3.3�ʰ� ������ ��
            {
                alarm.SetActive(false);               // C : object�� active ���¸� false�� ��ȯ
                alarmEffectTime = 0;                  // C : ���� �ִϸ��̼��� ���� �ִϸ��̼� Ÿ���� 0���� �ʱ�ȭ
            }
        }
        else                                            // C : object�� active ���°� false�̸�
        {
            alarmEffectTime = 0;                      // C : ���� �ִϸ��̼��� ���� �ִϸ��̼� Ÿ���� 0���� �ʱ�ȭ
        }
        */
    }

    void FixedUpdate()
    {
        // C : player moving
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);    // C : ���� Ȥ�� ���� �̵��� �����ϵ��� moveVec ����
        rigid.velocity = moveVec * speed;     // C : rigid�� �ӵ�(�ӷ� + ����) ����

        // C : Ray
        // C : ���� ��ġ�� rigid�� ��ġ, ������ dirVec, ���̴� 0.7f, ������ green�� ����׶����� ����(ray�� �ð�ȭ)
        // J : ��, �Ʒ� ���⿡�� ��ĵ ���ؼ� ���� 1.0f�� ����
        Debug.DrawRay(rigid.position, dirVec * 1.0f, new Color(0, 1, 0));
        // C : Object ���̾ ��ĵ�ϴ� ���� RayCast ����
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.0f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)    // C : ray�� Object�� �������� ��
        {
            scanObject = rayHit.collider.gameObject;    // C : RayCast�� ������Ʈ�� scanObject�� ����
        }
        else
            scanObject = null;
    }

    // J : å�� ã���� ��
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Book(Clone)") {        // J : �ε��� ������Ʈ�� å�� ���
            coll.gameObject.SetActive(false);               // J : Book Object ��Ȱ��ȭ
            // manager.talkPanel.SetActive(true);              // J : ��ȭâ Ȱ��ȭ
            // manager.talkText.text = "å�� ã�ҽ��ϴ�!";     // J : ��ȭâ �ؽ�Ʈ ����
            screenManager.getBook();                        // J : å ���� ����

            // å +1 ȿ����
            effect.Play("FindBookEffect");

            // C : å �߰� �ִϸ��̼� �����ϱ�
            // C : å �߰� game object�� �����Ͽ� ���ο� å �߰� game object ����
            GameObject bookInstance = Instantiate(addBook, player.transform.localPosition, Quaternion.identity);
            // C : ���� player�� �Ӹ� ���� bookInstance�� ��ġ�ϵ��� bookInstance�� �θ� object ����
            bookInstance.transform.SetParent(player.transform);
            // C : player �Ӹ� ���� å object ���̱�
            bookInstance.SetActive(true);
            // C : �ִϸ��̼� ���۰� ���� �����ϱ� ���� bookInstance�� �ִϸ��̼� �ð��� ����Ʈ�� �߰�
            addBookListG.Add(bookInstance);
            addBookListT.Add(0f);                       
        }
    }


    /*// C : 
    IEnumerator OnAlarm()
    {
        int count = 0;
        while (count < 3)
        {
            alarm.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            alarm.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }*/

    /*
    public void OnAlarm()
    {
        alarm.SetActive(true);
    }
    */

    /*private static DateTime Delay(int MS)
    {
        DateTime startMoment = DateTime.Now;
        TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
        DateTime endMoment = startMoment.Add(duration);
        while (endMoment >= startMoment)
        {
            System.Windows.Forms.Application.DoEvents();
            startMoment = DateTime.Now;
        }

        return DateTime.Now;
    }*/

    // C : ������̺�Ʈ �߻� �� �÷��̾� �Ӹ� ���� �˸� ����
    IEnumerator OnAlarm()
    {
        alarm.SetActive(true);
        yield return new WaitForSeconds(2.2f);
        alarm.SetActive(false);
    }

    // N : ��ҿ� ����
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // J : å �ݰ濡 �� ���
        if (coll.gameObject.name == "BookArea(Clone)")
        {
            GameObject book = coll.gameObject.transform.parent.gameObject;
            Book bookScript = book.GetComponent<Book>();

            book.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, bookScript.fadeCount);    // J : �ʱ� ���İ� ����
            book.GetComponent<Renderer>().enabled = true;    // J : å�� ���̵���

            bookScript.StopCoroutine("FadeOut");   // J : ���̵� �ƿ� ���̾��ٸ� �ߴ�
            bookScript.StartCoroutine("FadeIn");   // J : ���̵� �� ����
        }

        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(true);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(true);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(true);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (aiAction.isAICollisionToPlayer)
        {
            if (coll.gameObject.name == "FarmLearning")
            {
                farmIcon.SetActive(false);
            }
            if (coll.gameObject.name == "HouseLearning")
            {
                houseIcon.SetActive(false);
            }
            if (coll.gameObject.name == "CraftLearning")
            {
                craftIcon.SetActive(false);
            }
            if (coll.gameObject.name == "EngineerLearning")
            {
                engineerIcon.SetActive(false);
            }
        } else
        {
            if (coll.gameObject.name == "FarmLearning")
            {
                farmIcon.SetActive(true);
            }
            if (coll.gameObject.name == "HouseLearning")
            {
                houseIcon.SetActive(true);
            }
            if (coll.gameObject.name == "CraftLearning")
            {
                craftIcon.SetActive(true);
            }
            if (coll.gameObject.name == "EngineerLearning")
            {
                engineerIcon.SetActive(true);
            }
        }
    }

    // N : ��ҿ��� ������
    private void OnTriggerExit2D(Collider2D coll)
    {
        // J : å �ݰ濡�� ���� ���
        if (coll.gameObject.name == "BookArea(Clone)")
        {
            GameObject book = coll.gameObject.transform.parent.gameObject;
            Book bookScript = book.GetComponent<Book>();

            if (book.activeSelf == true)    // J : å ������Ʈ�� Ȱ��ȭ ����
            {
                bookScript.StopCoroutine("FadeIn");    // J : ���̵� �� ���̾��ٸ� �ߴ�
                bookScript.StartCoroutine("FadeOut");  // J : ���̵� �ƿ� ����
            }
        }

        if (coll.gameObject.name == "FarmLearning")
        {
            farmIcon.SetActive(false);
        }
        if (coll.gameObject.name == "HouseLearning")
        {
            houseIcon.SetActive(false);
        }
        if (coll.gameObject.name == "CraftLearning")
        {
            craftIcon.SetActive(false);
        }
        if (coll.gameObject.name == "EngineerLearning")
        {
            engineerIcon.SetActive(false);
        }
    }
}

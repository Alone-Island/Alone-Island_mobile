using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script �߰�
public class AIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;                          // C : �ִϸ��̼� ����
    SpecialEventManager specialManager;     // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ���� ����
    public LearningManager learningManager;
    public GameManager gameManager;
    public int vel = 1;                     // K : ai �̵� �ӵ� ����
    public int nextAIMoveX = 0;             // K : ai�� ���� X�� ���� ����
    public int nextAIMoveY = 0;             // K : ai�� ���� Y�� ���� ����
    public bool isAICollisionToPlayer = false;     // K : ai�� player�� �浹

    void NextAiMoveDirection()              // K : ai�� �����ϰ� �����̵��� ������ ������ �������ִ� �Լ�
    {
        int random = Random.Range(2, 6);    // K : ai�� ������ ���� ���� ����

        // K : AI�� ���� �ȵǴ� ���� ����

        switch (random)
        {
            case 1:                         // K : ����
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
            case 2:                         // K : ����
                nextAIMoveX = -1 * vel;
                nextAIMoveY = 0;
                break;
            case 3:                         // K : ��
                nextAIMoveX = 0;
                nextAIMoveY = vel;
                break;
            case 4:                         // K : ������
                nextAIMoveX = vel;
                nextAIMoveY = 0;
                break;
            case 5:                         // K : �Ʒ�
                nextAIMoveX = 0;
                nextAIMoveY = -1 * vel;
                break;
            default:
                nextAIMoveX = 0;            // K : ����ó�� - ����
                nextAIMoveY = 0;
                break;
        }

        Invoke("NextAiMoveDirection", 5);   // K : ����Լ�, 5�� �� �ڱ� �ڽ��� ����� 
    }

    public void GoToLearningPlace(int x, int y) // K : AI�� �н� ��ҷ� �����̵� �ϰ� �ϴ� �Լ�
                                                // (x,y)��ǥ�� �Ķ���ͷ� �޴´�.
    {
        transform.position = new Vector3(x, y, 0);
    }

    void Awake()
    {
        // C : Animator component instance ����
        anim = GetComponent<Animator>();
        // K : SpecialEventManager�� �Լ��� ȣ���� �� �ֵ��� specialManager ������ �ҷ����� ���� ȣ��
        specialManager = GameObject.Find("SpecialEventManager").GetComponent<SpecialEventManager>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("NextAiMoveDirection", 5);   // K : 5�� �� ai�� ������ ���� ���� �Լ� ����
    }

    void Update()
    {
        // C : ai �̵�(NextAiMoveDirection�� ���)���� ���� �ִϸ��̼� ����
        if (!isAICollisionToPlayer)
        {
            if (anim.GetInteger("hAxisRaw") != nextAIMoveX)         // C : ai �¿� �̵� �� ������ �ִϸ��̼� ����
            {
                anim.SetInteger("hAxisRaw", (int)nextAIMoveX);
            }
            else if (anim.GetInteger("vAxisRaw") != nextAIMoveY)    // C : ai ���� �̵� �� ������ �ִϸ��̼� ����
            {
                anim.SetInteger("vAxisRaw", (int)nextAIMoveY);
            }
        }
        else
        {
            anim.SetInteger("hAxisRaw", 0);
            anim.SetInteger("vAxisRaw", 0);
        }
    }

    void FixedUpdate()
    {   
        if (rigid.bodyType == RigidbodyType2D.Dynamic)
        {
            if (specialManager.special || learningManager.isAILearning || gameManager.isEndingShow || gameManager.playerTalk || isAICollisionToPlayer)
            // K : ����� �̺�Ʈ, �÷��̾ AI�� ��ȭ�ϴ�,  AI�� �н����϶� ����, ����ī�尡 �������� ��, AI�� �÷��̾ �浹���϶�
            {
                rigid.velocity = new Vector2(0, 0); // K : AI ����
            }
            else
            {
                rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : AI �̵�
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)   // K : AI �浹 ���� �Լ�
    {
        if (coll.gameObject.name == "Dr.Kim")
        {
            isAICollisionToPlayer = true;   // K : AI�� �÷��̾�� �浹�� Ȯ���ϱ� ���� �ڵ�

            
            // C : �浹 ���� ����
            Vector2 collisionDir = transform.position - coll.gameObject.transform.position;
            
            /*Debug.Log("ai direction : " + transform.position);
            Debug.Log("�ڻ� direction : " + coll.gameObject.transform.position);
            Debug.Log("direction : " + collisionDir);

            int collisionDirX = Mathf.RoundToInt(collisionDir.x);
            int collisionDirY = Mathf.RoundToInt(collisionDir.y);
            Debug.Log("x���� : " + Mathf.Abs(collisionDir.x));
            Debug.Log("y���� : " + Mathf.Abs(collisionDir.y));

            Debug.Log("x : " + collisionDir.x + "dX : " + collisionDirX + "���� : " + Mathf.Abs(collisionDir.x));
            Debug.Log("y : " + collisionDir.y + "dY : " + collisionDirY + "���� : " + Mathf.Abs(collisionDir.y));
            

            //Debug.Log("x  : " + collisionDirX + ", ���밪 x : " + Mathf.Abs(collisionDirX));
            //Debug.Log("y  : " + collisionDirY + ", ���밪 y : " + Mathf.Abs(collisionDirY));

            Debug.Log("x ���밪 : " + Math.Abs((int)collisionDir.x));
            Debug.Log("y ���밪 : " + Math.Abs((int)collisionDir.y));*/

            if (Mathf.Abs(collisionDir.x) > 1)
            {
                if (collisionDir.x - collisionDir.y < 0)
                {
                    Debug.Log("�ڻ� ���� : ������");
                    anim.SetBool("right", true);
                }
                else if (collisionDir.x - collisionDir.y > 0)
                {
                    Debug.Log("�ڻ� ���� : ����");
                    anim.SetBool("left", true);
                }
            }
            else if(Mathf.Abs(collisionDir.y) > 1)
            {
                if (collisionDir.x - collisionDir.y < 0)
                {
                    Debug.Log("�ڻ� ���� : �Ʒ���");
                    anim.SetBool("down", true);
                }
                else if (collisionDir.x - collisionDir.y > 0)
                {
                    Debug.Log("�ڻ� ���� : ����");
                    anim.SetBool("up", true);
                }
            }


            // C : AI �Ӹ� ���� Talk(spacebar) ������Ʈ ����
            GameObject talkObj = transform.Find("Talk").gameObject;
            talkObj.SetActive(true);

            // K : �浹�� �и����� ���Ÿ� ���� body type�� dynamic���� static���� ����
            rigid.bodyType = RigidbodyType2D.Static;
        }        
    }

    void OnCollisionStay2D(Collision2D coll)  // Ai �浹 ���� ���� �Լ�
    {
        
    }

    void OnCollisionExit2D(Collision2D coll)   // K : AI �浹 ���� ���� �Լ�
    {
        if (coll.gameObject.name == "Dr.Kim")
        {
            isAICollisionToPlayer = false; // K : AI�� �÷��̾�� �浹�� ���ŵ��� Ȯ���ϱ� ���� �ڵ�
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            // C : AI �Ӹ� ���� Talk(spacebar) ������Ʈ �����ϱ�
            GameObject talkObj = transform.Find("Talk").gameObject;
            talkObj.SetActive(false);

            // K : �浹�� �и����� ���Ÿ� ���� �����ߴ� body type�� �ٽ� dynamic���� ����
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}
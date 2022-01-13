using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// K : Scene > AI > Rigidbody 2D > body type > kinetic
// K : Scene > AI > AiAcition script �߰�
public class AutoAIAction : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;                          // C : �ִϸ��̼� ����
    public int nextAIMoveX = 0;             // K : object�� ���� X�� ���� ����
    public int nextAIMoveY = 0;             // K : object�� ���� Y�� ���� ����

    void NextAiMoveDirection()              // K : ai�� �����ϰ� �����̵��� ������ ������ �������ִ� �Լ�
    {
        int random = Random.Range(1, 6);    // K : ai�� ������ ���� ���� ����
        int vel = 1;                        // K : ai �̵� �ӵ� ���� 
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
                nextAIMoveX = vel; ;
                nextAIMoveY = 0;
                break;
            case 5:                         // K : �Ʒ�
                nextAIMoveX = 0;
                nextAIMoveY = -1 * vel;
                break;
            default:
                nextAIMoveX = 0;
                nextAIMoveY = 0;
                break;
        }

        Invoke("NextAiMoveDirection", 5);   // K : ����Լ�, 5�� �� �ڱ� �ڽ��� ����� 
    }

    void Awake()
    {
        // C : Animator component instance ����
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("NextAiMoveDirection", 2);   // J : 2�� �� object�� ������ ���� ���� �Լ� ����
    }

    void Update()
    {
        // C : object �̵�(NextAiMoveDirection�� ���)���� ���� �ִϸ��̼� ����
        if (anim.GetInteger("hAxisRaw") != nextAIMoveX)         // C : object �¿� �̵� �� ������ �ִϸ��̼� ����
        {
            anim.SetInteger("hAxisRaw", (int)nextAIMoveX);
        }
        else if (anim.GetInteger("vAxisRaw") != nextAIMoveY)    // C : object ���� �̵� �� ������ �ִϸ��̼� ����
        {
            anim.SetInteger("vAxisRaw", (int)nextAIMoveY);
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextAIMoveX, nextAIMoveY); // K : object �̵�
    }

    void OnCollisionEnter2D(Collision2D coll)   // object �浹 ���� �Լ�
    {
        Debug.Log("object �浹 �߻�");
        nextAIMoveX = 0;                        // object �浹 �߻��� ������ ����
        nextAIMoveY = 0;
    }
}

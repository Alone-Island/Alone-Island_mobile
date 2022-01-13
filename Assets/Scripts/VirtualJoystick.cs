using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// https://wergia.tistory.com/231
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PlayerAction playerAction;

    [SerializeField] 
    private RectTransform lever;    // J : ���� ������Ʈ�� ��ġ
    private RectTransform rectTransform;    // J : ���̽�ƽ�� ��ġ

    [SerializeField, Range(10f, 150f)] 
    private float leverRange;   // J : ������ �����̴� ����

    private Vector2 inputVector;
    private bool isInput;   // J : �巡�� ���� �÷��� (true�� ��쿡�� �÷��̾� �̵�)

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        ControlJoystickLever(eventData);
        isInput = true;
        //throw new System.NotImplementedException();
    }

    // J : ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ 
    // J : ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        ControlJoystickLever(eventData);
        //throw new System.NotImplementedException();
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        //throw new System.NotImplementedException();
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        //https://indala.tistory.com/50
        // J : touchPosition = �ڱ� �ڽ� ������Ʈ�� �������� �� ��ǥ
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 touchPosition)) 
            return; 
        //Debug.Log("touchPosition:" + touchPosition);
        var clampedDir = touchPosition.magnitude < leverRange ? touchPosition : (touchPosition.normalized * leverRange);    // J : ������ �����̴� ���͸� ���� ���� ������
        lever.anchoredPosition = clampedDir;    // J : ���� ��ġ ����
        inputVector = clampedDir / leverRange;
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInput)    // J : �巡�� ���̸� �÷��̾� �̵�
        {
            if (Mathf.Abs(inputVector.x) >= Mathf.Abs(inputVector.y))
            {
                //Debug.Log("horizontal");
                if (inputVector.x > 0)  // J : ���������� �̵�
                {
                    playerAction.v = 0;playerAction.h = 1;
                }
                else if (inputVector.x < 0) // J : �������� �̵�
                {
                    playerAction.v = 0;playerAction.h = -1;
                }
            }
            else
            {
                //Debug.Log("vertical");
                if (inputVector.y > 0)  // J : ���� �̵�
                {
                    playerAction.v = 1; playerAction.h = 0;
                }
                else if (inputVector.y < 0) // J : �Ʒ��� �̵�
                {
                    playerAction.v = -1; playerAction.h = 0;
                }
            }
        }
        else    // J : �÷��̾� �̵� X
        {
            //Debug.Log("No");
            playerAction.v = 0; playerAction.h = 0;
        }
    }

    // J : ���� ����Ű ����
    /*
    public PlayerAction playerAction;

    public Image up, down, right, left;

    private RectTransform rectTransform;
    private bool isInput;

    private Vector2 inputVec;

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        ControlJoystickLever(eventData);
        isInput = true;
        //throw new System.NotImplementedException();
    }

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ 
    // ������ Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        ControlJoystickLever(eventData);
        //throw new System.NotImplementedException();
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        isInput = false;
        //throw new System.NotImplementedException();
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        //https://indala.tistory.com/50
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out inputVec))
            return;
        Debug.Log(inputVec);
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInput)
        {
            if (Mathf.Abs(inputVec.x) >= Mathf.Abs(inputVec.y))
            {
                //Debug.Log("horizontal");
                if (inputVec.x > 0)
                {
                    playerAction.v = 0; playerAction.h = 1;
                    right.color = new Color(0.7f, 0.7f, 0.7f);
                    left.color = new Color(1, 1, 1);
                    up.color = new Color(1, 1, 1);
                    down.color = new Color(1, 1, 1);
                }
                else if (inputVec.x < 0)
                {
                    playerAction.v = 0; playerAction.h = -1;
                    left.color = new Color(0.7f, 0.7f, 0.7f);
                    right.color = new Color(1, 1, 1);
                    up.color = new Color(1, 1, 1);
                    down.color = new Color(1, 1, 1);
                }
            }
            else
            {
                //Debug.Log("vertical");
                if (inputVec.y > 0)
                {
                    playerAction.v = 1; playerAction.h = 0;
                    up.color = new Color(0.7f, 0.7f, 0.7f);
                    right.color = new Color(1, 1, 1);
                    left.color = new Color(1, 1, 1);
                    down.color = new Color(1, 1, 1);
                }
                else if (inputVec.y < 0)
                {
                    playerAction.v = -1; playerAction.h = 0;
                    down.color = new Color(0.7f, 0.7f, 0.7f);
                    right.color = new Color(1, 1, 1);
                    left.color = new Color(1, 1, 1);
                    up.color = new Color(1, 1, 1);
                }
            }
        }
        else
        {
            //Debug.Log("No");
            playerAction.v = 0; playerAction.h = 0;
            right.color = new Color(1, 1, 1);
            left.color = new Color(1, 1, 1);
            up.color = new Color(1, 1, 1);
            down.color = new Color(1, 1, 1);
        }
    }
    */
}

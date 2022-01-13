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
    private RectTransform lever; 
    private RectTransform rectTransform;

    [SerializeField, Range(10f, 150f)] 
    private float leverRange;

    private Vector2 inputVector;
    private bool isInput;

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        ControlJoystickLever(eventData);
        isInput = true;
        //throw new System.NotImplementedException();
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트 
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        ControlJoystickLever(eventData);
        //throw new System.NotImplementedException();
    }

    // 드래그 종료
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
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 touchPosition)) 
            return; 
        Debug.Log("touchPosition:" + touchPosition);
        var clampedDir = touchPosition.magnitude < leverRange ? touchPosition : (touchPosition.normalized * leverRange);
        lever.anchoredPosition = clampedDir;
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
        if (isInput)
        {
            if (Mathf.Abs(inputVector.x) >= Mathf.Abs(inputVector.y))
            {
                //Debug.Log("horizontal");
                if (inputVector.x > 0)
                {
                    playerAction.v = 0;playerAction.h = 1;
                }
                else if (inputVector.x < 0)
                {
                    playerAction.v = 0;playerAction.h = -1;
                }
                else
                {
                    playerAction.v = 0; playerAction.h = 0;
                }
            }
            else
            {
                //Debug.Log("vertical");
                if (inputVector.y > 0)
                {
                    playerAction.v = 1; playerAction.h = 0;
                }
                else if (inputVector.y < 0)
                {
                    playerAction.v = -1; playerAction.h = 0;
                }
                else
                {
                    playerAction.v = 0; playerAction.h = 0;
                }
            }
        }
        else
        {
            //Debug.Log("No");
            playerAction.v = 0; playerAction.h = 0;
        }
    }
    /*
    public PlayerAction playerAction;

    public Image up, down, right, left;

    private RectTransform rectTransform;
    private bool isInput;

    private Vector2 inputVec;

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        ControlJoystickLever(eventData);
        isInput = true;
        //throw new System.NotImplementedException();
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트 
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        ControlJoystickLever(eventData);
        //throw new System.NotImplementedException();
    }

    // 드래그 종료
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

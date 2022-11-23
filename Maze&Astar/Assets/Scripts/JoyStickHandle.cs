using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickHandle : MonoBehaviour
{
    [Header("position calculation")]
    [SerializeField] private bl_Joystick JS;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject PlayerObj;
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private void Start()
    {
        //ĵ������ �׷��ȷ���ĳ���͸� ������
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();

        //�̺�Ʈ �ý��� ������
        m_EventSystem = eventSystem.GetComponent<EventSystem>();

        StartCoroutine(JoystickDirection());
    }

    public IEnumerator JoystickDirection()
    {
        if (JS.isDrag)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);

            //m_PointerEventData.position = Input.mousePosition;
            Camera.main.ScreenToWorldPoint(transform.position);
            m_PointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.name != "Stick" &&
                        results[i].gameObject.name != "Joystick")
                    {
                        Debug.Log(results[i].gameObject.name);
                        switch (results[i].gameObject.name)
                        {
                            case "Up":
                                PlayerObj.transform.position += new Vector3(0, 1);
                                break;
                            case "Down":
                                PlayerObj.transform.position += new Vector3(0, -1);
                                break;
                            case "Left":
                                PlayerObj.transform.position += new Vector3(-1, 0);
                                break;
                            case "Right":
                                PlayerObj.transform.position += new Vector3(1, 0);
                                break;
                        }
                    }

                }
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(JoystickDirection());
    }

}

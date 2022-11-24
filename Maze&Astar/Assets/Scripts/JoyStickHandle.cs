using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class JoyStickHandle : MonoBehaviour
{
    [Header("position calculation")]
    [SerializeField] private bl_Joystick JS;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private Player player;
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private void Start()
    {
        //캔버스의 그래픽레이캐스터를 가져옴
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();

        //이벤트 시스템 가져옴
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
                        player.PlayerMove(results[i].gameObject.name);
                    }

                }
            }
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(JoystickDirection());
    }

}

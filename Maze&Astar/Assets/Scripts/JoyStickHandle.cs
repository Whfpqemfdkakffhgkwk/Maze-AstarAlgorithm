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

    //조이스틱이 어느 방향인지 알아내고 그 방향대로 움직이게 하는 코루틴
    public IEnumerator JoystickDirection()
    {
        //조이스틱이 드래그 중이면
        if (JS.isDrag)
        {
            #region UI 레이캐스트
            m_PointerEventData = new PointerEventData(m_EventSystem);
            Camera.main.ScreenToWorldPoint(transform.position);
            m_PointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            #endregion
            //레이캐스트에 맞은 오브젝트들이 1개 이상이면
            if (results.Count > 0)
            {
                //맞은 오브젝트들 갯수만큼 for
                for (int i = 0; i < results.Count; i++)
                {
                    //오브젝트 이름들이 스틱, 조이스틱이 아니면
                    if (results[i].gameObject.name != "Stick" &&
                        results[i].gameObject.name != "Joystick")
                    {
                        //벽 테스트 오브젝트 소환
                        GameObject WallTest = ObjPool.GetObject(EPoolType.WallTest, player.gameObject.transform.position);
                        //벽 테스트를 움직여야하는 위치로 미리 이동시켜봄
                        WallTest.GetComponent<WallCheck>().Move(results[i].gameObject.name);
                        yield return new WaitForSeconds(0.01f);
                        //벽 테스트가 벽이랑 충돌하지 않았다면
                        if(WallTest.GetComponent<WallCheck>().isWall == false)
                        {
                            //그 방향대로 플레이어를 움직인다
                            player.PlayerMove(results[i].gameObject.name);
                        }
                        //벽 충돌 bool값을 꺼주고
                        WallTest.GetComponent<WallCheck>().isWall = false;
                        //벽 테스트 오브젝트 반납
                        ObjPool.ReturnObject(EPoolType.WallTest, WallTest);
                    }

                }
            }
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(JoystickDirection());
    }

}

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
    /// <summary>
    /// 방향 상하좌우로 분류하는 함수a
    /// </summary>
    public int DirClassification()
    {
        int dir = 0;
        if (JS.StickDirection >= 45 && JS.StickDirection < 135)
            dir = 1; //위
        else if (JS.StickDirection >= 135 || JS.StickDirection < -135)
            dir = 2; //왼
        else if (JS.StickDirection <= -45 && JS.StickDirection >= -135)
            dir = 3; //아래
        else //if(JS.StickDirection <= 315 || JS.StickDirection < 45)
            dir = 4; //오른쪽
        return dir;
    }
    //조이스틱이 어느 방향인지 알아내고 그 방향대로 움직이게 하는 코루틴
    public IEnumerator JoystickDirection()
    {
        //조이스틱이 드래그 중이면
        if (JS.isDrag)
        {
            //벽 테스트 오브젝트 소환
            GameObject WallTest = ObjPool.GetObject(EPoolType.WallTest, player.gameObject.transform.position);
            //벽 테스트를 움직여야하는 위치로 미리 이동시켜보고 벽이 없으면 이동
            WallTest.GetComponent<WallCheck>().PlayerMove(DirClassification());
            //벽 테스트 오브젝트 반납
            ObjPool.ReturnObject(EPoolType.WallTest, WallTest);
        }
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(JoystickDirection());
    }
}

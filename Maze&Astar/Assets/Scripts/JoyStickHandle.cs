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
        //ĵ������ �׷��ȷ���ĳ���͸� ������
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();

        //�̺�Ʈ �ý��� ������
        m_EventSystem = eventSystem.GetComponent<EventSystem>();

        StartCoroutine(JoystickDirection());
    }
    /// <summary>
    /// ���� �����¿�� �з��ϴ� �Լ�a
    /// </summary>
    public int DirClassification()
    {
        int dir = 0;
        if (JS.StickDirection >= 45 && JS.StickDirection < 135)
            dir = 1; //��
        else if (JS.StickDirection >= 135 || JS.StickDirection < -135)
            dir = 2; //��
        else if (JS.StickDirection <= -45 && JS.StickDirection >= -135)
            dir = 3; //�Ʒ�
        else //if(JS.StickDirection <= 315 || JS.StickDirection < 45)
            dir = 4; //������
        return dir;
    }
    //���̽�ƽ�� ��� �������� �˾Ƴ��� �� ������ �����̰� �ϴ� �ڷ�ƾ
    public IEnumerator JoystickDirection()
    {
        //���̽�ƽ�� �巡�� ���̸�
        if (JS.isDrag)
        {
            //�� �׽�Ʈ ������Ʈ ��ȯ
            GameObject WallTest = ObjPool.GetObject(EPoolType.WallTest, player.gameObject.transform.position);
            //�� �׽�Ʈ�� ���������ϴ� ��ġ�� �̸� �̵����Ѻ��� ���� ������ �̵�
            WallTest.GetComponent<WallCheck>().PlayerMove(DirClassification());
            //�� �׽�Ʈ ������Ʈ �ݳ�
            ObjPool.ReturnObject(EPoolType.WallTest, WallTest);
        }
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(JoystickDirection());
    }
}

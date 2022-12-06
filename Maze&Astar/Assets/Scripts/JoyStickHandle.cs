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

    //���̽�ƽ�� ��� �������� �˾Ƴ��� �� ������ �����̰� �ϴ� �ڷ�ƾ
    public IEnumerator JoystickDirection()
    {
        //���̽�ƽ�� �巡�� ���̸�
        if (JS.isDrag)
        {
            #region UI ����ĳ��Ʈ
            m_PointerEventData = new PointerEventData(m_EventSystem);
            Camera.main.ScreenToWorldPoint(transform.position);
            m_PointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            #endregion
            //����ĳ��Ʈ�� ���� ������Ʈ���� 1�� �̻��̸�
            if (results.Count > 0)
            {
                //���� ������Ʈ�� ������ŭ for
                for (int i = 0; i < results.Count; i++)
                {
                    //������Ʈ �̸����� ��ƽ, ���̽�ƽ�� �ƴϸ�
                    if (results[i].gameObject.name != "Stick" &&
                        results[i].gameObject.name != "Joystick")
                    {
                        //�� �׽�Ʈ ������Ʈ ��ȯ
                        GameObject WallTest = ObjPool.GetObject(EPoolType.WallTest, player.gameObject.transform.position);
                        //�� �׽�Ʈ�� ���������ϴ� ��ġ�� �̸� �̵����Ѻ�
                        WallTest.GetComponent<WallCheck>().Move(results[i].gameObject.name);
                        yield return new WaitForSeconds(0.01f);
                        //�� �׽�Ʈ�� ���̶� �浹���� �ʾҴٸ�
                        if(WallTest.GetComponent<WallCheck>().isWall == false)
                        {
                            //�� ������ �÷��̾ �����δ�
                            player.PlayerMove(results[i].gameObject.name);
                        }
                        //�� �浹 bool���� ���ְ�
                        WallTest.GetComponent<WallCheck>().isWall = false;
                        //�� �׽�Ʈ ������Ʈ �ݳ�
                        ObjPool.ReturnObject(EPoolType.WallTest, WallTest);
                    }

                }
            }
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(JoystickDirection());
    }

}

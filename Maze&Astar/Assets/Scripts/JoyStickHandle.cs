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
    private void Start()
    {
        StartCoroutine(JoystickDirection());
    }
    /// <summary>
    /// 방향 상하좌우로 분류하는 함수
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
            //그 방향의 벽이 있는지 체크를 해본다
            Player.Instance.WallCheck(DirClassification());
        }
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(JoystickDirection());
    }
}

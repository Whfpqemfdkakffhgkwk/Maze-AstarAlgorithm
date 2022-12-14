using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    /// <summary>
    /// 게임 포기버튼 눌렀을때 실행되는 함수
    /// </summary>
    public void GameGiveUp()
    {
        StartCoroutine(AutoClear());
    }
    /// <summary>
    /// 자동 클리어
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoClear()
    {
        //플레이어가 오른쪽이 비어 있을 시 오른쪽으로 이동
        if (!MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX + 1, Player.Instance.PlayerPosY])
            Player.Instance.PlayerMove(4);
        //또는 플레이어가 윗쪽이 비어 있을 시 윗쪽으로 이동
        else if (!MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1])
            Player.Instance.PlayerMove(1);
        //플레이어가 도착점에 다달았을 시
        if (MazeCreate.Instance.MazeSize - 1 == Player.Instance.PlayerPosX &&
            MazeCreate.Instance.MazeSize == Player.Instance.PlayerPosY)
        {
            //이 자동 클리어 반복을 멈춘다
            yield break;
        }
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(AutoClear());
    }
}

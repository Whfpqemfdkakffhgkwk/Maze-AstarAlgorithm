using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    /// <summary>
    /// ���� �����ư �������� ����Ǵ� �Լ�
    /// </summary>
    public void GameGiveUp()
    {
        StartCoroutine(AutoClear());
    }
    /// <summary>
    /// �ڵ� Ŭ����
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoClear()
    {
        //�÷��̾ �������� ��� ���� �� ���������� �̵�
        if (!MazeCreate.Instance.Nodes[Player.Instance.PlayerPosX + 1, Player.Instance.PlayerPosY].isWall)
            Player.Instance.PlayerMove(4);
        //�Ǵ� �÷��̾ ������ ��� ���� �� �������� �̵�
        else if (!MazeCreate.Instance.Nodes[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1].isWall)
            Player.Instance.PlayerMove(1);
        //�÷��̾ �������� �ٴ޾��� ��
        if (MazeCreate.Instance.MazeSize - 1 == Player.Instance.PlayerPosX &&
            MazeCreate.Instance.MazeSize == Player.Instance.PlayerPosY)
        {
            //�� �ڵ� Ŭ���� �ݺ��� �����
            yield break;
        }
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(AutoClear());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private IEnumerator coroutine;
    public void Test()
    {
        coroutine = AutoClear();
        StartCoroutine(coroutine);
    }
    IEnumerator AutoClear()
    {
        if (!MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX + 1, Player.Instance.PlayerPosY])
            Player.Instance.PlayerMove(4);
        else if (!MazeCreate.Instance.Blocks[Player.Instance.PlayerPosX, Player.Instance.PlayerPosY + 1])
            Player.Instance.PlayerMove(1);
        if (MazeCreate.Instance.MazeSize - 1 == Player.Instance.PlayerPosX &&
            MazeCreate.Instance.MazeSize == Player.Instance.PlayerPosY)
        {
            yield break;
        }
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(AutoClear());
    }
}

using UnityEngine;
using DG.Tweening;
using System.Drawing;

public enum ItemType
{
    Bomb,
    Hint,
    Fail
}

public class Player : Singleton<Player>
{
    public int PlayerPosX, PlayerPosY;
    public int CntBomb, CntHint, CntFail;
    public bool isWall = false, isMoving = false;
    private I_Item item;

    #region ItemManager
    /// <summary>
    /// 아이템 종류를 세팅해주고 아이템을 사용
    /// </summary>
    /// <param name="itemType">아이템 타입</param>
    public void SetItem(ItemType itemType)
    {
        Component c = gameObject.GetComponent<I_Item>() as Component; // 현재 게임 옵젝의 I_Item 타입의 컴포넌트를 가져온다.
        if(c != null)
        {
            Destroy(c);
        }
        switch(itemType)
        {
            case ItemType.Bomb:
                item = gameObject.AddComponent<Bomb>();
                break;
                case ItemType.Hint:
                item = gameObject.AddComponent<Hint>();
                break;
            case ItemType.Fail:
                item = gameObject.AddComponent<Fail>();
                break;
        }
        item.UseItem();
    }
    #endregion

    private void Update()
    {
        //임시
        if(Input.GetKeyDown(KeyCode.Space))
        {
             SetItem(ItemType.Fail);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetItem(ItemType.Bomb);
        }
    }
    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    /// <param name="name">방향</param>
    public void PlayerMove(int name)
    {
        switch (name)
        {
            case 1:
                PlayerPosY += 1;
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                PlayerPosX -= 1;
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                PlayerPosY -= 1;
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                PlayerPosX += 1;
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
    /// <summary>
    /// 플레이어가 움직일 방향으로 미리 벽을 체크해보기
    /// </summary>
    /// <param name="name">방향</param>
    public void WallCheck(int name)
    {
        switch (name)
        {
            case 1:
                if (MazeCreate.Instance.Blocks[PlayerPosX, PlayerPosY + 1])
                    isWall = true;
                break;
            case 2:
                if (MazeCreate.Instance.Blocks[PlayerPosX - 1, PlayerPosY])
                    isWall = true;
                break;
            case 3:
                if (PlayerPosY - 1 < 0
                    || MazeCreate.Instance.Blocks[PlayerPosX, PlayerPosY - 1])
                        isWall = true;
                break;
            case 4:
                if (MazeCreate.Instance.Blocks[PlayerPosX + 1, PlayerPosY])
                    isWall = true;
                break;
        }
        if (isWall)
            isWall = false;
        else
            PlayerMove(name); //벽이 없으니깐 움직임 실행

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Bomb"))
        //{
        //    CntBomb++;
        //}
    }
}

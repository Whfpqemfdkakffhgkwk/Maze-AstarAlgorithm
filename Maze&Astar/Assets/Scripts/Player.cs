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
    /// ������ ������ �������ְ� �������� ���
    /// </summary>
    /// <param name="itemType">������ Ÿ��</param>
    public void SetItem(ItemType itemType)
    {
        Component c = gameObject.GetComponent<I_Item>() as Component; // ���� ���� ������ I_Item Ÿ���� ������Ʈ�� �����´�.
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
        //�ӽ�
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
    /// �÷��̾� ������
    /// </summary>
    /// <param name="name">����</param>
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
    /// �÷��̾ ������ �������� �̸� ���� üũ�غ���
    /// </summary>
    /// <param name="name">����</param>
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
            PlayerMove(name); //���� �����ϱ� ������ ����

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Bomb"))
        //{
        //    CntBomb++;
        //}
    }
}

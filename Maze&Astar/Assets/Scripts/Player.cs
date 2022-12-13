using UnityEngine;
using DG.Tweening;

public enum ItemType
{
    Bomb,
    Hint,
    Fail
}

public class Player : Singleton<Player>
{
    public int PlayerPosX, PlayerPosY;
    private I_Item item;

    #region ItemManager
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
                item = new Bomb();
                break;
                case ItemType.Hint:
                item = new Hint();
                break;
            case ItemType.Fail:
                item = new Fail();
                break;
        }
        item.UseItem();
    }
    #endregion
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bomb"))
        {
            SetItem(ItemType.Bomb);
        }
    }
}

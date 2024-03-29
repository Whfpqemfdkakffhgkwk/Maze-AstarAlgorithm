using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor;
using System.Collections;

public enum ItemType
{
    Bomb,
    Hint,
    Bulb
}

public class Player : Singleton<Player>
{
    public int PlayerPosX, PlayerPosY;
    public int CntBomb, CntHint, CntFail;
    public bool isWall = false, isMoving = false;
    [SerializeField]
    private GameObject[] Heart;
    [SerializeField] GameObject Boss, FlameParticle;
    private I_Item item;
    
    private int hp = 3;

    public int Hp 
    {
        get { return hp; }

        set 
        { 
            hp = value;
            Heart[hp].SetActive(false);
        }
    }

    private int cntPlayerMove;

    public int CntPlayerMove
    { 
        get { return cntPlayerMove; }

        set 
        {
            cntPlayerMove = value;

            if (cntPlayerMove >= 5) Boss.SetActive(true);
        }
    }

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
            case ItemType.Bulb:
                item = gameObject.AddComponent<Bulb>();
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
             SetItem(ItemType.Hint);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            SetItem(ItemType.Bomb);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            SetItem(ItemType.Bulb);
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
        CntPlayerMove++;
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
                if (MazeCreate.Instance.Nodes[PlayerPosX, PlayerPosY + 1].isWall)
                    isWall = true;
                break;
            case 2:
                if (MazeCreate.Instance.Nodes[PlayerPosX - 1, PlayerPosY].isWall)
                    isWall = true;
                break;
            case 3:
                if (PlayerPosY - 1 < 0
                    || MazeCreate.Instance.Nodes[PlayerPosX, PlayerPosY - 1].isWall)
                        isWall = true;
                break;
            case 4:
                if (MazeCreate.Instance.Nodes[PlayerPosX + 1, PlayerPosY].isWall)
                    isWall = true;
                break;
        }
        if (isWall)
            isWall = false;
        else
            PlayerMove(name); //벽이 없으니깐 움직임 실행

    }

    public IEnumerator Explode()
    {
        FlameParticle.transform.position = transform.position;
        FlameParticle.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        FlameParticle.SetActive(false);
    }
}

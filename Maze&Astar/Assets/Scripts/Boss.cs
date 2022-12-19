using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node
{
    [Tooltip("�̵��ߴ� �Ÿ�")]public int G; 
    [SerializeField, Tooltip("��ǥ������ �Ÿ�")]public int H;
    
    //G + H, �̵��ߴ��Ÿ� + ��ǥ������ �Ÿ�
    public int F { get { return G + H; } }
}

public class Boss : MonoBehaviour
{
    private int CurPosX = 1, CurPosY = 0;
    [Tooltip("0��, 1�Ʒ�, 2�� 3��")]Node[] nodes;

    private void Start()
    {
        HCount();
    }
    void HCount()
    {
        nodes[0].H = Mathf.Abs(CurPosX - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY + 1 - Player.Instance.PlayerPosY);
        nodes[1].H = Mathf.Abs(CurPosX - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - 1 - Player.Instance.PlayerPosY);
        nodes[2].H = Mathf.Abs(CurPosX - 1 - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - Player.Instance.PlayerPosY);
        nodes[3].H = Mathf.Abs(CurPosX + 1 - Player.Instance.PlayerPosX) + Mathf.Abs(CurPosY - Player.Instance.PlayerPosY);
    }
    public void Move(int name)
    {
        switch (name)
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(270, -90, 90);
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}

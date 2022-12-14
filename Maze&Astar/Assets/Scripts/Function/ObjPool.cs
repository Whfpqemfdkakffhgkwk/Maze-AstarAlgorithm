using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPoolType
{
    Wall,
    DeleteWall
}

public class ObjPool : Singleton<ObjPool>
{
    [SerializeField, Tooltip("Ǯ���� ������Ʈ �����յ�")]
    private GameObject[] poolingObjectPrefab;

    //poolingQueue(������ ����, �� ������ ������Ʈ��)
    public Dictionary<EPoolType, Queue<GameObject>> poolingQueue = new Dictionary<EPoolType, Queue<GameObject>>();


    //SetActive(true)���� ����
    public static GameObject GetObject(EPoolType type, Vector2 pos)
    {
        //Ű�� �ߺ����� ���� ���
        if(Instance.poolingQueue.ContainsKey(type) == false)
        {
            //���ϴ� Ÿ���� ��ųʸ� �迭�� �߰�
            Instance.poolingQueue.Add(type, new Queue<GameObject>());
        }
        Queue<GameObject> queue = Instance.poolingQueue[type];

        GameObject obj;

        //��밡���� ���ӿ�����Ʈ���� �ִٸ�
        if (queue.Count > 0)
        {
            //������Ʈ ť���� ������ �����
            obj = Instance.poolingQueue[type].Dequeue();
            obj.transform.position = pos;
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        //��밡���� ���ӿ�����Ʈ���� ���ٸ�
        else
        {
            //������Ʈ �����ؼ� �����ش�
            obj = Instantiate(Instance.poolingObjectPrefab[(int)type]);
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.SetParent(null);
            return obj;
        }
    }


    //SetActive(false)���� ����
    public static void ReturnObject(EPoolType type, GameObject obj)
    {
        //������Ʈ�� �� ����
        obj.gameObject.SetActive(false);
        //�θ� �ʱ�ȭ���ְ�
        obj.transform.SetParent(Instance.transform);
        //�� ������Ʈ�� Ÿ���� ť�� �ٽ� �־��ش�
        Instance.poolingQueue[type].Enqueue(obj);
    }
}

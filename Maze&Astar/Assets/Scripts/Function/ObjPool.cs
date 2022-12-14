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
    [SerializeField, Tooltip("풀링할 오브젝트 프리팹들")]
    private GameObject[] poolingObjectPrefab;

    //poolingQueue(프리팹 종류, 그 종류의 오브젝트들)
    public Dictionary<EPoolType, Queue<GameObject>> poolingQueue = new Dictionary<EPoolType, Queue<GameObject>>();


    //SetActive(true)같은 역할
    public static GameObject GetObject(EPoolType type, Vector2 pos)
    {
        //키가 중복하지 않을 경우
        if(Instance.poolingQueue.ContainsKey(type) == false)
        {
            //원하는 타입의 딕셔너리 배열을 추가
            Instance.poolingQueue.Add(type, new Queue<GameObject>());
        }
        Queue<GameObject> queue = Instance.poolingQueue[type];

        GameObject obj;

        //사용가능한 게임오브젝트들이 있다면
        if (queue.Count > 0)
        {
            //오브젝트 큐에서 꺼내다 사용함
            obj = Instance.poolingQueue[type].Dequeue();
            obj.transform.position = pos;
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        //사용가능한 게임오브젝트들이 없다면
        else
        {
            //오브젝트 생성해서 빌려준다
            obj = Instantiate(Instance.poolingObjectPrefab[(int)type]);
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.SetParent(null);
            return obj;
        }
    }


    //SetActive(false)같은 역할
    public static void ReturnObject(EPoolType type, GameObject obj)
    {
        //오브젝트를 끈 다음
        obj.gameObject.SetActive(false);
        //부모도 초기화해주고
        obj.transform.SetParent(Instance.transform);
        //그 오브젝트의 타입의 큐에 다시 넣어준다
        Instance.poolingQueue[type].Enqueue(obj);
    }
}

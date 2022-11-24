using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreate : MonoBehaviour
{
    [SerializeField] private GameObject MazeStartingPoint, Wall, DeleteWall;

    [SerializeField] private int MazeSize = 100;


    private void Start()
    {
        int Size = MazeSize + 1;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if(i % 2 == 0 || j % 2 == 0)
                {
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i % 2 == 0 || j % 2 == 0)
                    continue;

                if(i == Size - 2)
                {
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                    continue;
                }
                if(j == Size - 2)
                {
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                    continue;
                }

                if(Random.Range(0, 2) == 0)
                {
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                }
                else
                {
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
    }
}

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
                    //°ÝÀÚ¹«´Ì·Î º®µéÀ» »ý¼ºÇÔ
                    ObjPool.GetObject(EPoolType.Wall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j));
                }
            }
        }

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //µÎÁÙ¸¶´Ù x, yÃà ´Ù »ì·Á³õ°í ·£´ýÇÏ°Ô ¶Õ´Â°Å±â ¶§¹®¿¡ °Ç³Ê¶Ù¾îÁà¾ßÇÔ
                if (i % 2 == 0 || j % 2 == 0)
                    continue;

                if(i == Size - 2)
                {
                    //¸¶Áö¸· ºÎºÐ yÃà ¼± ¶Õ¾îÁÜ
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                    continue;
                }
                if(j == Size - 2)
                {
                    //¸¶Áö¸· ºÎºÐ xÃà ¼± ¶Õ¾îÁÜ
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                    continue;
                }

                if(Random.Range(0, 2) == 0)
                {
                    //·£´ýÇÏ°Ô xÃà ±âÁØÀ¸·Î ¶Õ¾îÁÜ
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i, MazeStartingPoint.transform.position.y + j + 1));
                }
                else
                {
                    //·£´ýÇÏ°Ô yÃà ±âÁØÀ¸·Î ¶Õ¾îÁÜ
                    ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + i + 1, MazeStartingPoint.transform.position.y + j));
                }
            }
        }
        //½ÃÀÛÁöÁ¡ ¶Õ¾îÁÜ
        ObjPool.GetObject(EPoolType.DeleteWall, new Vector2(MazeStartingPoint.transform.position.x + 1, MazeStartingPoint.transform.position.y));
    }
}

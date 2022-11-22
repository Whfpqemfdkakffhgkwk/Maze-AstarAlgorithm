using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreate : MonoBehaviour
{
    [SerializeField] private GameObject Player, Wall, DeleteWall;

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
                    Instantiate(Wall, new Vector2(Player.transform.position.x + i, Player.transform.position.y + j), Wall.transform.rotation);
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
                    Instantiate(DeleteWall, new Vector2(Player.transform.position.x + i, Player.transform.position.y + j + 1), Wall.transform.rotation);
                    continue;
                }
                if(j == Size - 2)
                {
                    Instantiate(DeleteWall, new Vector2(Player.transform.position.x + i + 1, Player.transform.position.y + j), Wall.transform.rotation);
                    continue;
                }

                if(Random.Range(0, 2) == 0)
                {
                    Instantiate(DeleteWall, new Vector2(Player.transform.position.x + i, Player.transform.position.y + j + 1), Wall.transform.rotation);
                }
                else
                {
                    Instantiate(DeleteWall, new Vector2(Player.transform.position.x + i + 1, Player.transform.position.y + j), Wall.transform.rotation);
                }
            }
        }
        Instantiate(DeleteWall, new Vector2(Player.transform.position.x + 1, Player.transform.position.y), Wall.transform.rotation);
    }
}

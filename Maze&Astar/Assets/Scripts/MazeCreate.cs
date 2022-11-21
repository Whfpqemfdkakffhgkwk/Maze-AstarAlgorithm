using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreate : MonoBehaviour
{
    [SerializeField] private GameObject Player, Wall;

    [SerializeField] private int MazeSize = 100;


    private void Start()
    {
        for (int i = 0; i < Random.Range(0, MazeSize + 1); i++)
        {
            for (int j = 0; j < Random.Range(0, MazeSize + 1); j++)
            {
                Instantiate(Wall, new Vector2(Player.transform.position.x + i, Player.transform.position.y + j), Wall.transform.rotation);
            }
        }
    }
    /*
     * from mcpi.minecraft import Minecraft
    import mcpi.block as block
    import random

    mc = Minecraft.create()
    mc.postToChat("Create Maze")    
    
    playerPos = mc.player.getPos()
    
    target_size = 500
    size = target_size + 1
    # for b in range(0, size):
    #     for a in range(0, size):
    #         mc.setBlock(playerPos.x + b, playerPos.y - 1, playerPos.z + a, block.GRASS) 
    
    for b in range(0, size):
        for a in range(0, size):
            if a % 2 == 0 or b % 2 == 0:
                mc.setBlock(playerPos.x + b, playerPos.y, playerPos.z + a, block.STONE) 
                mc.setBlock(playerPos.x + b, playerPos.y + 1, playerPos.z + a, block.STONE) 
            else:
                pass
    
    for b in range(0, size):
        for a in range(0, size):
            if a % 2 == 0 or b % 2 == 0:
                continue
    
            if b == size - 2:
                mc.setBlock(playerPos.x + b, playerPos.y, playerPos.z + a + 1, block.AIR) 
                mc.setBlock(playerPos.x + b, playerPos.y + 1, playerPos.z + a + 1, block.AIR) 
                continue
    
            if a == size - 2:
                mc.setBlock(playerPos.x + b + 1, playerPos.y, playerPos.z + a, block.AIR) 
                mc.setBlock(playerPos.x + b + 1, playerPos.y + 1, playerPos.z + a, block.AIR) 
                continue
    
            if(random.randrange(0,2) == 0):
                mc.setBlock(playerPos.x + b, playerPos.y, playerPos.z + a + 1, block.AIR) 
                mc.setBlock(playerPos.x + b, playerPos.y + 1, playerPos.z + a + 1, block.AIR) 
            else:
                mc.setBlock(playerPos.x + b + 1, playerPos.y, playerPos.z + a, block.AIR) 
                mc.setBlock(playerPos.x + b + 1, playerPos.y + 1, playerPos.z + a, block.AIR) 
    
    mc.setBlock(playerPos.x + 1, playerPos.y, playerPos.z, block.AIR) 
    mc.setBlock(playerPos.x + 1, playerPos.y + 1, playerPos.z, block.AIR) 
    
    mc.setBlock(playerPos.x + 1, playerPos.y - 1, playerPos.z, block.SAND) 
    mc.setBlock(playerPos.x + size - 2, playerPos.y - 1, playerPos.z + size - 1, block.SAND)
    */
}

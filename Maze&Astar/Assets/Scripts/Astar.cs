using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    string[] dir = {"Up", "Down", "Left", "Right"};
    IEnumerator Test(Vector3 pos)
    {
        for (int i = 0; i < dir.Length; i++)
        {
            GameObject WallTest = ObjPool.GetObject(EPoolType.WallTest, pos);
            WallTest.GetComponent<WallCheck>().Move(dir[i]);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

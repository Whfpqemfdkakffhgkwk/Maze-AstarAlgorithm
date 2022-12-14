using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(DestoryThisGameobject());
    }

    //바로 삭제되면 인식불가
    IEnumerator DestoryThisGameobject()
    {
        yield return new WaitForSeconds(0.01f);
        ObjPool.ReturnObject(EPoolType.DeleteWall, gameObject);
    }
    //닿은 벽 뚫어줌으로써 미로 만들어감
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            ObjPool.ReturnObject(EPoolType.Wall, collision.gameObject);
        }
    }
}

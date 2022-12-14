using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(DestoryThisGameobject());
    }

    //�ٷ� �����Ǹ� �νĺҰ�
    IEnumerator DestoryThisGameobject()
    {
        yield return new WaitForSeconds(0.01f);
        ObjPool.ReturnObject(EPoolType.DeleteWall, gameObject);
    }
    //���� �� �վ������ν� �̷� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            ObjPool.ReturnObject(EPoolType.Wall, collision.gameObject);
        }
    }
}

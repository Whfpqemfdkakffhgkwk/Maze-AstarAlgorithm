using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.01f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Destroy(collision.gameObject);
            print("asd");
        }
    }
}

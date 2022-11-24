using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    Sequence mySequence;
    bool isWallCollision = false;
    private void Awake()
    {
        mySequence = DOTween.Sequence();
    }
    private void FixedUpdate()
    {
        isWallCollision = false;
    }
    public void PlayerMove(string name)
    {
        if (!isWallCollision)
        {
            switch (name)
            {
                case "Up":
                    mySequence.Append(transform.DOMove(transform.position + new Vector3(0, 1), 0.3f));
                    break;
                case "Down":
                    mySequence.Append(transform.DOMove(transform.position + new Vector3(0, -1), 0.3f));
                    break;
                case "Left":
                    mySequence.Append(transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f));
                    break;
                case "Right":
                    mySequence.Append(transform.DOMove(transform.position + new Vector3(1, 0), 0.3f));
                    break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWallCollision = true;
            Debug.Log("a");
            mySequence.Kill();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public void PlayerMove(string name)
    {
        switch (name)
        {
            case "Up":
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case "Down":
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case "Left":
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case "Right":
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}

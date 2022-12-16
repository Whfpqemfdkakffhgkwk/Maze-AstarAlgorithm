using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public void Move(int name)
    {
        switch (name)
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(270, -90, 90);
                transform.DOMove(transform.position + new Vector3(0, 1), 0.3f);
                break;
            case 2:
                transform.DOMove(transform.position + new Vector3(-1, 0), 0.3f);
                break;
            case 3:
                transform.DOMove(transform.position + new Vector3(0, -1), 0.3f);
                break;
            case 4:
                transform.DOMove(transform.position + new Vector3(1, 0), 0.3f);
                break;
        }
    }
}

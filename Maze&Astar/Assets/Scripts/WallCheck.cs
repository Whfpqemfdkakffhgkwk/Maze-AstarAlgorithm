using DG.Tweening;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool isWall = false;

    public void Move(string name)
    {
        Vector3 vec = new Vector3(0, 0);
        switch (name)
        {
            case "Up":
                vec = new Vector3(0, 1);
                break;
            case "Down":
                vec = new Vector3(0, -1);
                break;
            case "Left":
                vec = new Vector3(-1, 0);
                break;
            case "Right":
                vec = new Vector3(1, 0);
                break;
        }
        transform.position += vec;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            isWall = true;
        }
    }
}

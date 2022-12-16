using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBlock : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ReturnObj());
    }

    IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(1.3f);
        ObjPool.ReturnObject(EPoolType.HintBlock, gameObject);

    }
}

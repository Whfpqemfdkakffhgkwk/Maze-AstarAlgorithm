using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Fail : MonoBehaviour, I_Item
{
    
    public void UseItem()
    {
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value = 0.32f;
        StartCoroutine(ReturnStr(Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>()));
    }
    IEnumerator ReturnStr(Vignette vignette)
    {
        yield return new WaitForSeconds(10f);
        vignette.intensity.value = 0.56f;
    }
}

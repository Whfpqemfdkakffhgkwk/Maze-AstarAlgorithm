using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class Bulb : MonoBehaviour, I_Item
{
    
    public void UseItem()
    {
        //포스트 프로세싱 Vignette 줄임(공포도 하락)
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value -= 0.09f;
    }
}

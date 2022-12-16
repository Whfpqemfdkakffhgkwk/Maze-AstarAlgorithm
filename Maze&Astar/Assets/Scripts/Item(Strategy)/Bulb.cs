using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class Bulb : MonoBehaviour, I_Item
{
    
    public void UseItem()
    {
        //����Ʈ ���μ��� Vignette ����(������ �϶�)
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>().intensity.value -= 0.09f;
    }
}

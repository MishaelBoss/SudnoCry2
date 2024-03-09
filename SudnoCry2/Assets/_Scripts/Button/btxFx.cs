using UnityEngine;


public class btxFx : MonoBehaviour
{

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void hoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }


    public void clickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}

using DG.Tweening;
using UnityEngine;

public class ClicerAnimDOTween : MonoBehaviour
{
    Tween tween;
    /*void Update()
    {
        tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f)
            .SetUpdate(UpdateType.Manual, true)
            .SetLoops(-1, LoopType.Yoyo);
        //Invoke("Restart", 0.1f);
    }*/
    public void Click()
    {
        //animator.SetTrigger("Click");
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f))
            .Append(transform.DORotate(new Vector3(0, 0, 5f), 0.5f))
            .AppendInterval(0.5f)
            .AppendCallback(Restart);
        //transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        //transform.DORotate(new Vector3(0, 0, 5f), 0.6f);
        //Invoke("Restart", 1);
    }

    void Restart()
    {
        transform.DOScale(1f, 1f);
        transform.DORotate(new Vector3(0, 0, 0f), 0.5f);
    }
}

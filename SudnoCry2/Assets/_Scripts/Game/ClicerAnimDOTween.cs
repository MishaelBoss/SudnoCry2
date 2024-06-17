using DG.Tweening;
using UnityEngine;

public class ClicerAnimDOTween : MonoBehaviour
{
    public float DOScale = 0.3f;
    public float DORotate = 0.3f;
    public float RestartTime = 0.4f;
    void Update()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector2(2, 2), 0.1f))
            .AppendInterval(0.5f)
            .SetEase(Ease.InOutQuart)
            .Append(transform.DOScale(new Vector2(1, 1), 0.1f))
            .AppendInterval(1);
    }

 /*   public void Click()
    {
        //animator.SetTrigger("Click");
        *//*DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f))
            .Append(transform.DORotate(new Vector3(0, 0, 5f), 0.5f))
            .AppendInterval(0.5f)
            .AppendCallback(Restart);*//*
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), DOScale);
        transform.DORotate(new Vector3(0, 0, 5f), DORotate);
        Invoke("Restart", RestartTime);
    }

    void Restart()
    {
        transform.DOScale(1f, 1f);
        transform.DORotate(new Vector3(0, 0, 0f), 0.5f);
    }*/
}

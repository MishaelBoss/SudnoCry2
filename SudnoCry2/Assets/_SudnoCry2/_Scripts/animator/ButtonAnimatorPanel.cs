using DG.Tweening;
using UnityEngine;

public class ButtonAnimatorPanel : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject panel;

    [Header("Animator")]
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BlackPanel2() {

        DOTween.Sequence()
            .Append(transform.DOPunchPosition(new Vector3(0, 3, 0), 0.5f))
            .AppendInterval(0.1f)
            .Append(transform.DOPunchPosition(new Vector3(0, 0, 0), 0.7f));
    }

    public void BlackPanel()
    {
        animator.SetTrigger("Black");
        Invoke("FalsePanel", 1f);
    }

    public void Click()
        => animator.SetTrigger("Click");

    void FalsePanel()
        => panel.SetActive(false);
}

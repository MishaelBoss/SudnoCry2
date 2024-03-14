using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonAnimatorPanel : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject panel;

    [Header("Animator")]
    Animator animator;

    Vector3 ToObject;

    void Start()
        => animator = GetComponent<Animator>();

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

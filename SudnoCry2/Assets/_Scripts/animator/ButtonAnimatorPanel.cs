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

    public void BlackPanel2()
    {
        animator.SetTrigger("Black");
        Invoke("FalsePanel", 1f);
    }

    public void BlackPanel()
    {
        animator.SetTrigger("Black");
        Invoke("FalsePanel", 1f);
    }

    void FalsePanel()
        => panel.SetActive(false);

    public void Click() {
        //animator.SetTrigger("Click");
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        // transform.DOShakeRotation(0f);
        Invoke("Restart", 0.1f);
    }

    void Restart() { 
        transform.DOScale(1f, 1f);
        //transform.DOShakeRotation(0f);
    }
}

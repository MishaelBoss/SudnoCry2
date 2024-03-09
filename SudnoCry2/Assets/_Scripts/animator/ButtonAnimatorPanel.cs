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

    public void BlackPanel2()
    {
        animator.SetTrigger("Black");
        Invoke("Delet", 1f);
    }

    void Delet()
    {
        panel.SetActive(false);
    }

    public void BlackPanel()
    {
        animator.SetTrigger("Black");
        Invoke("FalsePanel", 1f);
    }

    void FalsePanel()
    {
        panel.SetActive(false);
    }

    public void Click() {
        animator.SetTrigger("Click");
    }
}

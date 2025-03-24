using DG.Tweening;
using TMPro;
using UnityEngine;

public class ClickParent : MonoBehaviour
{
    private Vector2 randomPosition;

    private void Update()
        => transform.Translate(randomPosition * Time.deltaTime);

    public void StartMotion(int value) { 
        transform.localPosition = Vector2.zero; 
        randomPosition = new Vector2(Random.Range(-200, 200), Random.Range(-200, 200));

        GetComponent<TMP_Text>().text = "+" + value;

        StartAnimation();
        Invoke("Delet", 10);
    }

    private void StartAnimation() {
        DOTween.Sequence()
            .Append(GetComponent<TMP_Text>().DOFade(0f, 5f).From().SetEase(Ease.Linear));
    }

    private void Delet()
        => Destroy(gameObject);
}

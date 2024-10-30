using TMPro;
using UnityEngine;

public class ClickParent : MonoBehaviour
{
    private bool move;

    private Vector2 randomPosition;

    private void Update()
    {
        if (!move) return;
        transform.Translate(randomPosition * Time.deltaTime);
    }

    public void StartMotion(int value) { 
        transform.localPosition = Vector2.zero; 
        GetComponent<TMP_Text>().text = "+" + value;
        randomPosition = new Vector2(Random.Range(-200, 200), Random.Range(-200, 200));
        move = true;
    }
}

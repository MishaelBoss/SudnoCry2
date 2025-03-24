using UnityEngine;
using UnityEngine.UI;

public class CartReword : MonoBehaviour
{
    public Text textName;
    public Text textDescription;
    public Button button;
    public Text buttonText;

    private int coin;

    private GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();
    }

    public void SetInfo(AchieveData info) {
        textName.text = info.achievement.name;
        textDescription.text = info.achievement.description;

        coin = info.achievement.coin;
    }

    public void Take() {
        gameManager.coin += coin;
        button.interactable = false;
        buttonText.text = "<color=white>Награда была взята</color>";
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CartAchieve : MonoBehaviour
{
    public Image icon;
    public Text text;

    public void SetInfo(AchieveData info) {
        icon.sprite = info.achievement.sprite;
        text.text = info.achievement.description;
    }
}

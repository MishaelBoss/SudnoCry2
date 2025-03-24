using System.Collections.Generic;
using UnityEngine;

public class ViewListAchive : MonoBehaviour
{
    public List<AchieveData> achieveDataLists = new List<AchieveData>();

    public CartReword expectedCartReword;
    public CartReword unavailableContentCartReword;
    public CartAchieve cartAchieve;
    public Transform expectedContent;
    public Transform unavailableContent;

    private void Start()
    {
        if(cartAchieve.gameObject.activeSelf)
            cartAchieve.gameObject.SetActive(false);

        foreach (AchieveData info in achieveDataLists) {
            CartReword listItem = Instantiate(unavailableContentCartReword, unavailableContent);
            if (listItem != null)
                listItem.SetInfo(info);
        }
    }

    public void Achieve(int id)
    {
        foreach (var achieveData in achieveDataLists)
        {
            if (!achieveData.isAchieved && achieveData.achievement.Id == id)
            {
                achieveData.isAchieved = true;
                cartAchieve.SetInfo(achieveDataLists[id]);
                cartAchieve.gameObject.SetActive(true);
                UpdateUI(id);
            }
        }
    }

    private void UpdateUI(int id)
    {
        CartReword listItem = Instantiate(expectedCartReword, expectedContent);

        foreach (AchieveData info in achieveDataLists)
        {
            if (listItem != null)
                listItem.SetInfo(achieveDataLists[id]);
        }
    }
}

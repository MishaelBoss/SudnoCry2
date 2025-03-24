using UnityEngine;

[System.Serializable]
public class AchieveDataList : AchieveDataListInterface
{
    public float id;

    public string name; 

    public Sprite sprite;

    public int coin;

    public string description;

    public float Id { get => id; }
    public string Name { get => name; set => name = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int Coin { get => coin; set => coin = value; }
    public string Description { get => description; set => description = value; }
}
public interface AchieveDataListInterface
{
    float Id { get; }
    string Name { get; set; }
    Sprite Sprite { get; set; }
    int Coin { get; set; }
    string Description { get; set; }
}

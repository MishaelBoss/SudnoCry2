[System.Serializable]
public class AchieveData
{
    public bool isAchieved;
    public AchieveDataList achievement;

    public AchieveData(AchieveDataList achievement)
    {
        this.achievement = achievement;
        this.isAchieved = false;
    }
}
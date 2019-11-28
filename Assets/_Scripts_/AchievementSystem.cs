#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[System.Serializable]
public class Achievement
{
    public enum AchievementType
    {
        TotalPlayerDeaths,
        TotalKills,
        TotalItemUse_Vodka
    }
    public AchievementType type;
    public bool isUnlocked;
    public int valueToUnlock;
    public string achievementTitle, achievementMessage;
}

public class AchievementSystem : MonoBehaviour
{
    #region Variables
    [SerializeField] private Achievement[] achievements;
    private int TotalPlayerDeaths
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_PLAYER_DEATHS", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_PLAYER_DEATHS", value);
            PlayerPrefs.Save();
        }
    }
    private int TotalKills
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_KILLS", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_KILLS", value);
            PlayerPrefs.Save();
        }
    }
    private int TotalItemUse_Vodka
    {
        get
        {
            return PlayerPrefs.GetInt("TOTAL_ITEM_USE_VODKA", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TOTAL_ITEM_USE_VODKA", value);
            PlayerPrefs.Save();
        }
    }
    #endregion

    private void Start()
    {
        Player.Instance.OnPlayerDeath += OnPlayerDeath;
        Enemy.Instance.OnEnemyDeath += OnEnemyDeath;
        //PickupBase.Instance.itemPickedUp += OnItemPickUp;
    }

    private void OnPlayerDeath()
    {
        TotalPlayerDeaths++;
        CheckForUnlock(Achievement.AchievementType.TotalPlayerDeaths, TotalPlayerDeaths);
    }

    private void OnEnemyDeath()
    {
        TotalKills++;
        CheckForUnlock(Achievement.AchievementType.TotalKills, TotalKills);
    }

    private void OnItemPickUp(string pickUpName)
    {
        switch (pickUpName)
        {
            case "VODKA":
                TotalItemUse_Vodka++;
                CheckForUnlock(Achievement.AchievementType.TotalItemUse_Vodka, TotalItemUse_Vodka);
                break;
        }
    }

    private void CheckForUnlock(Achievement.AchievementType selectedType, int inputValue)
    {
        foreach (Achievement ach in achievements)
        {
            if (ach.type == selectedType)
            {
                if (!ach.isUnlocked && inputValue >= ach.valueToUnlock)
                {
                    UnlockAchievement(ach);
                }
            }
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        achievement.isUnlocked = true;
        print($"Achievement Unlocked! '{achievement.achievementTitle}': {achievement.achievementMessage}");
    }
}

using UnityEngine;
using UnityEditor;

public static class LevelManager 
{
    //When coming to next level, update this class and then load level accordingly.
    public static int NextLevel { get; set; }
    public static bool[] IsBonusLevelUnlocked = {false, false, false};
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Global_Event
{
    #region Enemy Death

    public static event Action<int> Enemy_Death_Event;

    public static void Enemy_Death(int score_Points)
    {
        Enemy_Death_Event?.Invoke(score_Points);
    }

    #endregion

    #region EndGame

    public static event Action End_Game_Event;

    public static void End_Game()
    {
        End_Game_Event?.Invoke();
    }

    #endregion

}
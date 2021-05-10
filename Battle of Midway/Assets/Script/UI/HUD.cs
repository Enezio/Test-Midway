using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Health Settings")]
    Health_System player_Health;
    public Text text;

    [Header("score Settings")]
    public int score = 123;
    [SerializeField] Sprite[] numbers_Sprite;
    [SerializeField] Image[] numbers_Image;

    [Header("End_Game_settings")]
    [SerializeField] GameObject end_Game_Obj;

    void Awake()
    {
        // Player Events
        Player_FSM.Player_On_Enable_Event += Get_Player_Health;
        Player_FSM.Player_Take_Damage_Event += Update_Health;
        Player_FSM.Player_Heal_Event += Update_Health;

        // Global Events
        Global_Event.Enemy_Death_Event += Update_Score;
        Global_Event.End_Game_Event += End_Game;
    }

    private void Start()
    {
        Update_Score(0);
        Update_Health();
    }

    #region Health

    void Get_Player_Health(Health_System health)
    {
        player_Health = health;
        Update_Health();
    }

    void Update_Health(int health_Modifier = 0)
    {
        text.text = player_Health.Current_Health.ToString();
    }

    #endregion

    #region Score

    void Update_Score(int score_Points)
    {
        score += score_Points;
        score = Mathf.Clamp(score, 0, 999999);

        Update_Score_GUI(Isolate_Every_Number(score));
    }

    int[] Isolate_Every_Number(int number)
    {
        List<int> numbers_Isolated = new List<int>();

        while(number >= 10)
        {
            numbers_Isolated.Add(number % 10);
            number = number / 10;
        }

        numbers_Isolated.Add(number);

        return numbers_Isolated.ToArray();
    }

    void Update_Score_GUI(int[] score)
    {
        if(score != null)
        {
            for(int i = 0; i < numbers_Image.Length; i++)
            {
                if(i < score.Length)
                    numbers_Image[i].sprite = numbers_Sprite[score[i]];
                else
                    numbers_Image[i].sprite = numbers_Sprite[0];
            }
        }
        else
        {
            foreach (Image image in numbers_Image)
                image.sprite = numbers_Sprite[0];
        }
    }

    #endregion

    #region End_Game

    void End_Game()
    {
        end_Game_Obj.SetActive(true);
    }

    #endregion

}
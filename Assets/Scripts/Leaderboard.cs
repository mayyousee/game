using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text[] highScores;
    public Text[] highNames;
    int[] highScoreValues;
    string[] highScoreNames;
    public int scorePosition;
    public bool isScore;

    // Use this for initialization
    void Start()
    {
        isScore = false;
        highScoreValues = new int[highScores.Length];
        highScoreNames = new string[highScores.Length];
        for (int x = 0; x < highScores.Length; x++)
        {
            highScoreValues[x] = PlayerPrefs.GetInt("0" + x);
            highScoreNames[x] = PlayerPrefs.GetString("highScoreNames" + x);
        }
    //    DrawScores();
    }
    void SaveScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            PlayerPrefs.SetInt("0" + x, highScoreValues[x]);
            PlayerPrefs.SetString("highScoreNames" + x, highScoreNames[x]);
        }
    }
    public void DrawNames(string userName)
    {
        highScoreNames[scorePosition] = userName;
        PlayerPrefs.SetString("highScoreNames" + scorePosition, highScoreNames[scorePosition]);
        DrawScores();
    }

    public void CheckScores(int value)
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            if (value > highScoreValues[x])
            {
                isScore = true;
                for (int y = highScores.Length - 1; y > x; y--)
                {
                    highScoreValues[y] = highScoreValues[y - 1];
                    highScoreNames[y] = highScoreNames[y - 1];
                }
                highScoreValues[x] = value;
                highScoreNames[x] = "";
                scorePosition = x;
                DrawScores();
                SaveScores();
                break;
            }
        }
    }


    public void DrawScores()
    {
        for (int x = 0; x < highScores.Length; x++)
        {
            highScores[x].text = highScoreValues[x].ToString();
            highNames[x].text = highScoreNames[x].ToString();
        }
    }
}

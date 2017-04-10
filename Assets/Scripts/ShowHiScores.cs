using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowHiScores : MonoBehaviour {

    public GameObject splashScreen;
    public GameObject startButt;
    public GameObject exitButt;
    public GameObject hiScoresButt;
    public GameObject backButt;
    public GameObject leaderboard;
    public Leaderboard lead;

    public void ShowOn ()
        {

        lead.DrawScores();
        splashScreen.SetActive(false);
        startButt.SetActive(false);
        exitButt.SetActive(false);
        hiScoresButt.SetActive(false);
        leaderboard.SetActive(true);
        backButt.SetActive(true);
        
        }

}

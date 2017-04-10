using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int bunnyValue;
    public int score;
    public AudioClip playScore;
    public AudioClip playScoreGold;
    public AudioClip playScull;
    public GameController gameController;

    // Use this for initialization
    void Start()
    {
        score = 0;
        UpdateScore();

    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GoldBunny")
        {
            score += bunnyValue * 3;
            gameController.timeLeft += 10.0f ;
            //gameController.UpdateText();
            UpdateScore();
            PlayScore(playScore);
            new WaitForSeconds(1.0f);
            PlayScore(playScoreGold);
        }
        else
        {
            score += bunnyValue;
            UpdateScore();
            PlayScore(playScore);
        }
    } 

    void OnCollisionEnter2D(Collision2D hatCollider)
    {
        if (hatCollider.gameObject.tag == "Scull")
        {
            score -= bunnyValue * 2;
            UpdateScore();
            PlayScore(playScull);
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score\n" + score;
    }

    void PlayScore(AudioClip play)
    {
        AudioSource.PlayClipAtPoint(play, transform.position);
    }
}

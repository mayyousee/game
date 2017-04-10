using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public Camera cam;
	private float maxWidth;
	public GameObject[] bunnyRabbits;
	public float timeLeft;
    public Text timerText;
    public Text countdownText;
	public GameObject gameOverText;
    public GameObject leaderBoard;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
    public GameObject menuButton;
	private bool playing;
	public HatController hatController;
	public Score scoreClass;
	public float addTime;
    public GameObject submitButton;
    public GameObject playerInputField;
    public int getRand;
    public bool scull = true;

    // Use this for initialization
    void Start ()
	{
		if (cam == null)
		{
			cam = Camera.main;
		}

		// Leaderboard cleanup
		// PlayerPrefs.DeleteAll();

		// Hat position
		playing = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = bunnyRabbits[0].GetComponent<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;

		UpdateText ();

        StartCoroutine(StartCountdown());
        StartCoroutine(Spawn(3.0f));  
    }

    // Game timer
	void FixedUpdate()
	{
		if (playing)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
			{
				timeLeft = 0;
			}
			UpdateText();
		}
	}

    // Starting spawn, game over events
    public IEnumerator Spawn(float timeSpawn)
    {
        yield return new WaitForSeconds(4);
        hatController.ToggleControl(true);
        playing = true;

        while (timeLeft > 0 && scoreClass.score >= 0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth),transform.position.y,0.0f);
            Quaternion spawnRotation = Quaternion.identity;

            // Spawn management
            WasItScull();
            Instantiate(bunnyRabbits[getRand], spawnPosition, spawnRotation);
            if (getRand > 0)
            {
                scull = true;
            }
            else
            {
                scull = false;
            }

            // Speedup
            if (scoreClass.score % 5 == 0 && scoreClass.score > 0 && scoreClass.score < 50)
            {
                addTime = addTime + 0.2f;
            }
            yield return new WaitForSeconds(timeSpawn / addTime);
        }

        // Game over
        // Mb this will work that way
        StartCoroutine(GameOver());

        /*timeLeft = 0.0f;
        yield return new WaitForSeconds(0.5f);
        hatController.ToggleControl(false);
        yield return new WaitForSeconds(0.5f);
        gameOverText.SetActive(true);
        // Score management
        GetComponent<Leaderboard>().CheckScores(scoreClass.score);
        yield return new WaitForSeconds(0.5f);
        leaderBoard.SetActive(true);
        GetComponent<Leaderboard>().DrawScores();
        if (GetComponent<Leaderboard>().isScore)
        {
        playerInputField.SetActive(true);
        submitButton.SetActive(true);
        }
        else
        {
        yield return new WaitForSeconds(0.5f);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
        }*/
    }

	// Upd time left
	public void UpdateText()
	{
		timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft);
	}

	// Spawn management
    int getRandom()
    {
        if(Random.Range(0.0f, 1.0f) < 0.6f)
        {
            return 0;
        }
        return Random.Range(1, bunnyRabbits.Length);
    }

    // Spawn management
    public void WasItScull()
    {
        if (scull)
        {
            getRand = 0;
        }
        else
        {
            getRand = getRandom();
        }
    }

    // Score management
    public void InitialsEntered()
    {
       GetComponent<Leaderboard>().DrawNames(playerInputField.GetComponentInChildren<InputField>().text );
        submitButton.SetActive(false);
        playerInputField.SetActive(false);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }

    // Starting game countdown
    IEnumerator StartCountdown(float countdownValue = 4.0f)
    {
        while (countdownValue > 0)
        {
            //Debug.Log("countDown: " + countDown);
            yield return new WaitForSeconds(1.0f);
            countdownValue--;
            countdownText.text = "Get Ready!\n" + Mathf.RoundToInt(countdownValue);
        }
        countdownText.GetComponent<Text>().enabled = false;
    }

	IEnumerator GameOver ()
    {
     timeLeft = 0.0f;
     yield return new WaitForSeconds(0.5f);
     hatController.ToggleControl(false);
     yield return new WaitForSeconds(0.5f);
     gameOverText.SetActive(true);
     // Score management
     GetComponent<Leaderboard>().CheckScores(scoreClass.score);
     yield return new WaitForSeconds(0.5f);
     leaderBoard.SetActive(true);
     GetComponent<Leaderboard>().DrawScores();
     if (GetComponent<Leaderboard>().isScore)
     {
        playerInputField.SetActive(true);
        submitButton.SetActive(true);
     }
     else
     {
        yield return new WaitForSeconds(0.5f);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
     }
	}
}

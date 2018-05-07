using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    float timeLeft = 120f; //300

    private float minutes, seconds;

    private int scoreAcounter, scoreBcounter;
    public Text scoreA, scoreB, infoText, time;
    public GameObject ball, playerA, playerB;
    // private GameObject collisionObj;
	public AudioClip clipToPlay;
	public AudioSource audioSource;

    void Start()
    {
        scoreAcounter = 0;
        scoreBcounter = 0;
        infoText.enabled = false;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = Mathf.Round(timeLeft % 60);
        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }
        if (timeLeft > 0)
        {
            if (seconds < 10) time.text = "Time: " + minutes + ":0" + seconds;
            else time.text = "Time: " + minutes + ":" + seconds;
        }
        else
        {
            if (scoreAcounter > scoreBcounter)
                StartCoroutine(ShowMessage("Times up!\n Player A won!", 2.0f));
            else if(scoreAcounter < scoreBcounter)
                StartCoroutine(ShowMessage("Times up!\n Player B won!", 2.0f));
            else
                StartCoroutine(ShowMessage("Times up!\n It's a draw!", 2.0f));
            Invoke("GoToMenu", 2f);
        }
        UpdateScore();

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "GoalPost")
        {
			audioSource.PlayOneShot(clipToPlay);

            StartCoroutine(ShowMessage("Goal!", 2.0f));

            if (other.name == "GoalA")
            {
                scoreAcounter++;
            }
            else if (other.name == "GoalB")
            {
                scoreBcounter++;
            }
            // Move objects to the original (starting) positions
            ball.GetComponent<Rigidbody>().AddForce(0, 0, 0);
            playerA.GetComponent<Rigidbody>().AddForce(0, 0, 0);
            playerB.GetComponent<Rigidbody>().AddForce(0, 0, 0);
            ball.transform.position = GameObject.Find("BallPosition").transform.position;
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerA.transform.position = GameObject.Find("PlayerAPosition").transform.position;
            playerB.transform.position = GameObject.Find("PlayerBPosition").transform.position;
            playerA.transform.rotation = GameObject.Find("PlayerAPosition").transform.rotation;
            playerB.transform.rotation = GameObject.Find("PlayerBPosition").transform.rotation;
        }
    }

    void UpdateScore()
    {
        scoreA.text = scoreAcounter.ToString();
        scoreB.text = scoreBcounter.ToString();

    }

    IEnumerator ShowMessage(string message, float delay)
    {
        infoText.text = message;
        infoText.enabled = true;
        yield return new WaitForSeconds(delay);
        infoText.enabled = false;
    }

    void GoToMenu() {
        SceneManager.LoadScene ("MenuScene");
    }

}

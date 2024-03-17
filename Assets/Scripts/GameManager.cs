using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameStarted = false;
    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public Vector3 charPos;

    public void Awake()
    {
        scoreText.text = $"Score: {score}";
        highScoreText.text = $"High Score: {GetHighScore()}";
    }

    private void Update()
    {
        if ( gameStarted == false && Input.GetKeyDown(KeyCode.Mouse1)) { StartGame(); }
    }

    public void StartGame()
    {
        gameStarted = true;
        //FindAnyObjectByType<RoadGeneration>().StartBuildingRoad();
    }

    public void EndGame()
    {
        
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
        if (score > GetHighScore()) {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = $"High Score: {score}";
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("HighScore");
        return i;
    }
    








    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

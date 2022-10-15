using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public LevelEditor levelEditor;
    public PaddleBehaviour paddle;
    public BallBehaviour ball;
    public BallBehaviour tempball;
    public GameObject menuCanvas;
    public GameObject gameOverCanvas;
    public GameObject inGameCanvas;
    public GameObject serveCanvas;
    public GameObject nextLevelcanvas;
    public bool playState = false;
    private GameObject ballObject;
    private GameObject paddleObject;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI ingameScoreText;
    public TextMeshProUGUI ingameLifeText;
    public TextMeshProUGUI leveltext;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip nexxtLevelSound;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(playState == true && paddle.TilesDefeated == levelEditor.tileArray.Length)  // Move on the Next Level
		{
            ResetPaddle(paddleObject);
            ResetPaddleScale(paddleObject);
            paddleObject.SetActive(false);
            paddle.movementvalue = 0;
            paddle.scoreMultiplier = 1;
            Destroy(ballObject);
            leveltext.text = "Level " + levelEditor.level + " is Clear.";
            inGameCanvas.SetActive(false);
            nextLevelcanvas.SetActive(true);
		}
        if(playState == true && paddle.life <= 0) // Game over 
		{
            ResetPaddleScale(paddleObject);
            Destroy(ballObject);
            Destroy(paddleObject);
            levelEditor.DeleteAllTiles();
            scoretext.text = "Your Score is : " + paddle.score;
            inGameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);

		}
        UpdateTexts();
        CheckIfBallIsPlayable();
    }

    public void CheckIfBallIsPlayable()
	{
		if (tempball!=null && tempball.isAlive == false && paddle.life>0)
		{
            playState = false;
            ResetPaddle(paddleObject);
            ResetPaddleScale(paddleObject);
            paddleObject.SetActive(false);
            paddle.movementvalue = 0;
            paddle.life--;
            animator.SetTrigger("LifeHighlight");
            Destroy(ballObject);
            levelEditor.DeactiveAllTiles();
            serveCanvas.SetActive(true);
		}
	}
    public void AddPaddletoScreen()
	{
        Vector3 spawnPos = new Vector3(0f, -5.5f, 0f);
        paddleObject = Instantiate(paddle.gameObject, spawnPos, paddle.transform.rotation) as GameObject;
        paddle = paddle.gameObject.GetComponent<PaddleBehaviour>();
        paddle.life = 15;
        paddle.TilesDefeated = 0;
        paddle.score = 0;
        ball.InstantiatePaddleBehaviour(paddle);
    }

    public void ResetPaddle(GameObject obj)
	{
        obj.transform.position = new Vector3(0, -5.5f, 0);
        paddle.paddleBody.velocity = new Vector2(0, 0);
	}

    public void AddBalltoScreen()
	{
        Vector3 spawnPos = new Vector3(0, 0, 0);
        ballObject = Instantiate(ball.gameObject, spawnPos, ball.transform.rotation) as GameObject;
        tempball = ballObject.GetComponent<BallBehaviour>();
        tempball.isAlive = true;
        tempball.SetAnimator(ingameScoreText.gameObject);

    }

    public void ResetExistingPaddle()
	{
        Vector3 spawnPos = new Vector3(0f, -5.5f, 0f);
        paddle.transform.position = spawnPos;
    }

    public void ResetPaddleScale(GameObject paddleObject)
	{

        Vector3 scale = paddleObject.transform.localScale;
        scale.x = 7;
        paddleObject.transform.localScale = scale;
    }

    public void GotoNewLevel() // Next level preperations.
	{
        audioSource.PlayOneShot(nexxtLevelSound);
        nextLevelcanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        paddle.TilesDefeated = 0;
        paddleObject.SetActive(true);
        levelEditor.level++;
        levelEditor.CreateNewLevel();
        AddBalltoScreen();
        ResetExistingPaddle();
    }
    
    public void StartTheGame()
	{
        AddPaddletoScreen();
        AddBalltoScreen();
        menuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        levelEditor.CreateNewLevel();
        playState = true;
    }


    public void RestartTheGame()
    {
        AddBalltoScreen();
        menuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        serveCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        paddleObject.SetActive(true);
        levelEditor.ReactiveAllTiles();
        playState = true;
    }

    public void TriggerReplayButton()
	{
        AddPaddletoScreen();
        AddBalltoScreen();
        menuCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        levelEditor.level = 1;
        levelEditor.CreateNewLevel();
        playState = true;

    }

    public void QuitGAme()
	{
        Application.Quit();
	}
    

    public void UpdateTexts()
	{
        ingameLifeText.text = "Life: " + paddle.life;
        ingameScoreText.text = "Score: " + paddle.score;
	}
}

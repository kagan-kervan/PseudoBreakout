using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public GameManager manager;
    public PaddleBehaviour paddle;
    public BallBehaviour ball;
    public float xPosForBall;
    public bool PlayState = false;
    public bool ServeState = true;
    public bool MenuState = false;
    public bool HighScoreState = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manager.AddPaddletoScreen();
        while(ServeState == true)
		{
            xPosForBall = xPosForBall + paddle.movementvalue;
			if (Input.GetKeyDown(KeyCode.Space) == true)
			{
                manager.AddBalltoScreen();
                ServeState = false;
                PlayState = true;
			}
		}
		if (ball.isAlive == false)
		{
            PlayState = false;
            ServeState = true;
		}
    }
}

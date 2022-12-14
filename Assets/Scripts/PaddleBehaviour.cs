using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    public Rigidbody2D paddleBody;
    public BallBehaviour ballBehaviour;
    public float moveSpeed = 1.3f;
    public bool serveState = false;
    public int score;
    public int TilesDefeated;
    public float movementvalue;
    public int life = 10;
    private Vector3 initialPos;
    private Vector2 initialSpeed;
    public int scoreMultiplier = 1;
    // Start is called before the first frame update
    void Awake()
    {
        TilesDefeated = 0;
		score = 0;

	}
	private void Start()
	{
        initialPos = this.transform.position;
        initialSpeed = paddleBody.velocity;
	}

	// Update is called once per frame
	void Update()
    {
        PaddleMovementInTouchScreen();
    }

    public void PaddleMovement(float value)
	{
        Vector2 velocity = paddleBody.velocity;
        velocity.x = value * moveSpeed;
        paddleBody.velocity = velocity;
	}

    public void PaddleMovementInTouchScreen()
	{
		for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            touchPosition.z = 1;
            touchPosition.y = transform.position.y;
            transform.position = touchPosition;
        }
    }
    public float GetValue()
	{
        float value = Input.GetAxisRaw("Horizontal");
        if (transform.position.x < -14 && value < 0)
            value = 0;
        else if (transform.position.x > 14 && value > 0)
            value = 0;
        return value;
    }

	public void ResetPaddle()
	{
        this.transform.position = initialPos;
        this.paddleBody.velocity = initialSpeed;
	}
}

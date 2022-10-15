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
        movementvalue = GetValue();
        PaddleMovement(movementvalue);
    }

    public void PaddleMovement(float value)
	{
        Vector2 velocity = paddleBody.velocity;
        velocity.x = value * moveSpeed;
        paddleBody.velocity = velocity;
	}

    public float GetValue()
	{
        float value = Input.GetAxisRaw("Horizontal");
        return value;
    }

	public void ResetPaddle()
	{
        this.transform.position = initialPos;
        this.paddleBody.velocity = initialSpeed;
	}
}

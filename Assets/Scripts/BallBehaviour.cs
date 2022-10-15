using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float moveSpeed = 1;
    public float value;
    public float speedMultipilier = 1.9f;
    public Rigidbody2D ballBody;
    public GameObject paddleObject;
    public BallBehaviour prefabBall;
    private Vector2 initialvelocity;
    public bool firstHitMultiplier = true;
    public bool isAlive = true;
    public PaddleBehaviour paddleBehaviour = null;
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip pointClip;
    // Start is called before the first frame update
    void Start()
    {
       initialvelocity = ballBody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBallontheBroad();
        
    }

	public void OnCollisionEnter2D(Collision2D collision)
	{
        audioSource.PlayOneShot(hitClip);
        PaddleBehaviour paddle = collision.collider.GetComponent<PaddleBehaviour>();
        if (paddle != null)
		{
            if(firstHitMultiplier == true)
            {
                ballBody.velocity = ballBody.velocity * speedMultipilier;
                firstHitMultiplier = false;

            }
            value = BallPositionFeedback(paddle.gameObject);
            BallAngleRandomizer(value);

		}
		else
		{
            TileBehaviour tile = collision.collider.GetComponent<TileBehaviour>();
            if(tile != null)
			{
                audioSource.PlayOneShot(pointClip);
                tile.UpdateLifePoint();
			}
		}
	}

    public void CheckBallontheBroad()
	{
        if (this.transform.position.y < -7f)
		{
            isAlive = false;
		}
	}

    public void BallReset()
	{
        firstHitMultiplier = true;
        isAlive = false;
        this.ballBody.velocity = initialvelocity;
        paddleBehaviour.life--;
        if (paddleBehaviour.life <= 0)
        {
            return;
        }
        else
            ResetBalltoScreen();
       
	}

    public float BallPositionFeedback(GameObject obj)
	{
        float objectpos = obj.transform.position.x;
        float ballpos = this.transform.position.x;
        if (objectpos == ballpos)
            return 0f;
        else if (ballpos < objectpos)
		{
            if (objectpos >= 0)
                return -(objectpos - ballpos);
            else
                return ballpos-objectpos;
		}
        else
		{
            if (objectpos >= 0)
                return ballpos - objectpos;
            else
                return -(objectpos - ballpos);
		}
            
	}

    public void InstantiatePaddleBehaviour(PaddleBehaviour paddleObject)
	{
        paddleBehaviour = paddleObject;
    }
    public void BallAngleRandomizer(float value)
	{
        float RandomXVelocity = 0;
        if (Mathf.Abs(value) > 0.48)
            RandomXVelocity = Random.Range(Mathf.Abs(value)*6.2f, Mathf.Abs(value)*10);
        else if (value != 0)
            RandomXVelocity = Random.Range(0f, Mathf.Abs(value)*5);
        if (value < 0)
            RandomXVelocity = -RandomXVelocity;
        Vector2 newVelocity = new Vector2(RandomXVelocity, ballBody.velocity.y);
        ballBody.velocity = newVelocity;

	}

    public void ResetBalltoScreen()
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        this.transform.position = spawnPos;
        isAlive = true;
    }
}

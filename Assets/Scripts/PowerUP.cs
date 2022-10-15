using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public PowerupEffect powerupEffect;
	public PaddleBehaviour behaviour;
	public PaddleBehaviour paddle;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		behaviour = collision.GetComponent<PaddleBehaviour>();
		if(behaviour != null || collision.GetComponent<BallBehaviour>()!=null)
		{
			powerupEffect.Apply(paddle.gameObject);
		}
	}
}

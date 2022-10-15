using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePowerUp : MonoBehaviour
{
	public PowerupEffect powerupEffect;
	public PaddleBehaviour behaviour;
	public PaddleBehaviour paddle;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		behaviour = collision.GetComponent<PaddleBehaviour>();
		if (behaviour != null)
		{
			powerupEffect.Apply(behaviour.gameObject);
			Destroy(gameObject);
		}

	}

	public void Update()
	{
		PowerUpPositionCheck();
	}
	public void PowerUpPositionCheck()
	{
		if (this.transform.position.y < -10)
			Destroy(gameObject);
	}
}

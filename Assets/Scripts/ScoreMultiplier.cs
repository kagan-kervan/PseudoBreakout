using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Powerups/ScoreMultiplier")]
public class ScoreMultiplier : PowerupEffect
{
	public int amount;
	public override void Apply(GameObject target)
	{
		target.GetComponent<PaddleBehaviour>().scoreMultiplier += amount;
	}
}

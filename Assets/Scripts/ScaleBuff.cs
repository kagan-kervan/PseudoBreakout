using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ScaleBuff")]
public class ScaleBuff : PowerupEffect
{

	public override void Apply(GameObject target)
	{

		Vector3 scale = target.transform.localScale;
		scale.x = 10;
		target.transform.localScale = scale;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class TileBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PaddleBehaviour paddle;
    public int tier;
    public int lifepoint;
    public Sprite[] tier1Sprites;
    public Sprite[] tier2Sprites;
    public Sprite[] tier3Sprites;
    public Sprite[] tier4Sprites;

    
    public void TileColourDecider(int value)
	{
        int randomNum = Random.Range(0, value);
	}
    public void InstiateLifePoint(int tileValue)
	{
        if (tileValue == 0)
            lifepoint = 1;
        else if (tileValue == 1)
            lifepoint = 2;
        else if (tileValue == 2)
            lifepoint = 3;
        else if (tileValue == 3)
            lifepoint = 4;
        else if (tileValue == 4)
            lifepoint = 5;
    }

    public void UpdateLifePoint()
	{
        lifepoint--;
        paddle.score = paddle.score + (tier * 200 + 25 * lifepoint)*paddle.scoreMultiplier;
        if (lifepoint <= 0)
		{
            if (tier == 1)
            {
                GameObject.Destroy(gameObject);
                paddle.TilesDefeated++;
                return;
            }
            else
            {
                tier--;
                lifepoint = 5;
            }
		}
        ChangeTileSprite(tier);

    }
    public void ChangeTileSprite(int tierValue)
	{
        if(tierValue==1)
            spriteRenderer.sprite = tier1Sprites[lifepoint-1];
        if (tierValue == 2)
            spriteRenderer.sprite = tier2Sprites[lifepoint - 1];
        if (tierValue == 3)
            spriteRenderer.sprite = tier3Sprites[lifepoint - 1];
        if (tierValue == 4)
            spriteRenderer.sprite = tier4Sprites[lifepoint - 1];

    }
}

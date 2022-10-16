using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public int level = 1;
    public int Row;
    public int Column;
    public PaddleBehaviour paddle;
    public GameObject[,] tileArray;
    public TileBehaviour tier1Tile;
    public TileBehaviour tier2Tile;
    public TileBehaviour tier3Tile;
    public TileBehaviour tier4Tile;
    private GameObject[,] temptileArray;
    public GameManager manager;


    public void CreateNewLevel()
	{
        
        Column = (int)(8 + (level * 0.2));
        if (Column % 2 == 1)
            Column++;
        Row = (int)(2 + (level * 0.2));
        tileArray= new GameObject[Row,Column];
        temptileArray = new GameObject[Row, Column];
        for (int i = 0; i < tileArray.GetLength(0); i++)
		{
			for (int j = 0; j < tileArray.GetLength(1); j++)
			{
				int randomTierNum = Random.Range((int)(level*0.1), (int)(1+level*0.4));
				int randomTileNum = Random.Range((int)(0+(level*0.5)%4), (int)(1 + level * 0.6));
                GameObject newtile = SetTiles(randomTierNum, randomTileNum);
                tileArray[i, j] = newtile;
			}
		}
        DrawtheLevel();
	}

    public GameObject SetTiles(int tiernum,int tilenum)
	{
        tiernum = Mathf.Min(tiernum, 3);
        tilenum = Mathf.Min(tilenum, 4);
        GameObject tile;
		if (tiernum==0)
		{
            tier1Tile.InstiateLifePoint(tilenum);
            tier1Tile.ChangeTileSprite(tiernum+1);
            tile = tier1Tile.gameObject;
		}
        else if (tiernum == 1)
        {
            tier2Tile.InstiateLifePoint(tilenum);
            tier2Tile.ChangeTileSprite(tiernum + 1);
            tile = tier2Tile.gameObject;
        }
        else if (tiernum == 2)
        {
            tier3Tile.InstiateLifePoint(tilenum);
            tier3Tile.ChangeTileSprite(tiernum + 1);
            tile = tier3Tile.gameObject;
        }
        else
        {
            tier4Tile.InstiateLifePoint(tilenum);
            tier4Tile.ChangeTileSprite(tiernum + 1);
            tile = tier4Tile.gameObject;
        }
        return tile;
    }

    public void DrawtheLevel()
	{
        float startingPointX = -8.9f;
        float startingPointY = 6.8f;
		for (int i = 0; i < tileArray.GetLength(0); i++)
		{
			for (int j = 0; j < tileArray.GetLength(1); j++)
			{
                int skipNum = Random.Range(0, 100);
                if (skipNum > 70)
				{
                    paddle.TilesDefeated++;
                    continue;
				}
                Vector3 spawnPosition = new Vector3(startingPointX + j * 2.1f, startingPointY + i * -1f, 0);
                GameObject tileObject = Instantiate(tileArray[i, j], spawnPosition, tileArray[i, j].transform.rotation) as GameObject;
                temptileArray[i, j] = tileObject;
			}
		}

    }

    public void DeleteAllTiles()
	{
		for (int i = 0; i < temptileArray.GetLength(0); i++)
		{
			for (int j = 0; j < temptileArray.GetLength(1); j++)
			{
				if (temptileArray[i, j] != null)
				{
                    Destroy(temptileArray[i,j]);
                }
			}
		}
	}

    public void DeactiveAllTiles()
    {
        for (int i = 0; i < temptileArray.GetLength(0); i++)
        {
            for (int j = 0; j < temptileArray.GetLength(1); j++)
            {
                if (temptileArray[i, j] != null)
                {
                    temptileArray[i, j].SetActive(false);
                }
            }
        }
        manager.audioSource.PlayOneShot(manager.deathSound);

    }

    public void ReactiveAllTiles()
    {
        for (int i = 0; i < temptileArray.GetLength(0); i++)
        {
            for (int j = 0; j < temptileArray.GetLength(1); j++)
            {
                if (temptileArray[i, j] != null)
                {
                    temptileArray[i, j].SetActive(true);
                }
            }
        }
    }

}

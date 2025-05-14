using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocks_Generator : MonoBehaviour
{
	public const int NUM_BLOCKS = 7;

	[SerializeField]
	private GameObject [] blocks = new GameObject[NUM_BLOCKS];
	[SerializeField]
	private GameObject m_GameManager = null;

	[SerializeField]
	private Transform p1SpawnPoint;
	[SerializeField]
    private Transform p2SpawnPoint;

    public void GenerateBlock(string objTag)
	{
		if (m_GameManager.GetComponent<GameManager>().isGameRunning())
		{
			bool ready = true;

			for (int index = 0; index < NUM_BLOCKS && ready; ++index)
			{
				if (blocks[index] == null)
				{
					ready = false;
				}
            }

			if (ready)
			{
				int blockIndex = Random.Range (0, NUM_BLOCKS);
				if (objTag == "Start")
				{
					var p1Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p1SpawnPoint.position, Quaternion.identity);
					p1Block.tag = "P1_Block";
					var p2Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p2SpawnPoint.position, Quaternion.identity);
					p2Block.tag = "P2_Block";
				}
				else if (objTag == "P1_Block")
				{
					var p1Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p1SpawnPoint.position, Quaternion.identity);
					p1Block.tag = "P1_Block";
				}
				else if (objTag == "P2_Block")
				{
					var p2Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p2SpawnPoint.position, Quaternion.identity);
					p2Block.tag = "P2_Block";
				}
				else if (objTag == "P1_Base")
				{
					var baseBlock = GameObject.Instantiate<GameObject>(blocks[blockIndex], p1SpawnPoint.position, Quaternion.identity);
					baseBlock.tag = "P1_Block";
                }
                else if (objTag == "P2_Base")
                {
                    var baseBlock = GameObject.Instantiate<GameObject>(blocks[blockIndex], p2SpawnPoint.position, Quaternion.identity);
                    baseBlock.tag = "P2_Block";
                }
                else if (objTag == "P1_Limite")
				{
					var p1Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p1SpawnPoint.position, Quaternion.identity);
					p1Block.tag = "P1_Block";
				}
				else if (objTag == "P2_Limite")
				{
					var p2Block = GameObject.Instantiate<GameObject>(blocks[blockIndex], p2SpawnPoint.position, Quaternion.identity);
					p2Block.tag = "P2_Block";
				}
			}
		}
	}
}

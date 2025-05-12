using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blocks_Generator : MonoBehaviour {
	public const int NUM_BLOCKS = 7;

	[SerializeField]
	private GameObject [] blocks = new GameObject[NUM_BLOCKS];
	[SerializeField]
	private GameObject m_GameManager = null;

	public void GenerateBlock(){
		if (m_GameManager.GetComponent<GameManager>().isGameRunning()) {
			bool ready = true;

			for (int index = 0; index < NUM_BLOCKS && ready; ++index)
				if (blocks [index] == null)
					ready = false;

			if (ready) {
				int blockIndex = Random.Range (0, NUM_BLOCKS);
				GameObject.Instantiate<GameObject> (blocks [blockIndex]);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class VictoryCheck : MonoBehaviour {
	[SerializeField]
	private GameObject m_GameManager = null;
	[SerializeField]
	private LayerMask m_LayerMask = 0;
	/*
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector2(12f, transform.position.y-0.15f), new Vector2(12f, transform.position.y-0.15f) + Vector2.left * 30f);
	}*/

	private void Update(){
		RaycastHit2D colpito = Physics2D.Raycast (new Vector2 (12f, transform.position.y-0.15f), Vector2.left, 30f, m_LayerMask);
		if (colpito.collider != null) {
			if(colpito.collider.gameObject.tag.Equals("Block")){
				if (!(colpito.collider.gameObject.GetComponent<Block_OnFalling>().enabled) && m_GameManager!=null) {
					m_GameManager.GetComponent<GameManager> ().setGameRunning (false);
					m_GameManager.GetComponent<GameManager> ().setVictory (true);
				}
			}
		}
	}
}

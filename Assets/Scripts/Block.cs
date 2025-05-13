using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.tag.Equals("Limite"))
		{
			GameManager.FindObjectOfType<GameManager> ().youLoseLife ();
			Destroy (this.gameObject);
		}
	}
}

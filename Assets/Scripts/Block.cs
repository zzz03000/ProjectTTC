using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D other)
	{
        //p1가 아래로 떨어지면
        if (other.gameObject.tag.Equals("P1_Limite"))
		{
			GameManager.FindObjectOfType<GameManager> ().youLoseLife("P1");
			Destroy (this.gameObject);
		}
		//p2가 아래로 떨어지면
		else if(other.gameObject.tag.Equals("P2_Limite"))
		{
            GameManager.FindObjectOfType<GameManager>().youLoseLife("P2");
            Destroy(this.gameObject);
        }
	}
}

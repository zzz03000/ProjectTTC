using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

	[SerializeField]
	Heart[] hearts = null;

	int numHearts;

	void Start ()
	{
		if (hearts == null)
			return;

		numHearts = hearts.Length;
	}

	public void removeHeart ()
	{
		if (numHearts > 0)
		{
			--numHearts;
			hearts [numHearts].GetComponent<Heart> ().hide ();
		}
	}

	public void addHeart ()
	{
		if (numHearts < hearts.Length)
		{
			++numHearts;
			hearts [numHearts].GetComponent<Heart> ().hide ();
		}
	}

}

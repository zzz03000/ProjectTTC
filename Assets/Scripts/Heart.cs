using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{

	[SerializeField]
	Image heartImage = null;

	public void show ()
	{
		heartImage.enabled = true;
	}

	public void hide ()
	{
		heartImage.enabled = false;
	}
}

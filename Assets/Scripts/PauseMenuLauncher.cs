using UnityEngine;
using System.Collections;

public class PauseMenuLauncher : MonoBehaviour
{
	[SerializeField]
	private GameObject m_PauseMenu = null;

	// Use this for initialization
	void Start ()
	{
		if (m_PauseMenu != null)
			m_PauseMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Time.timeScale = 0;
			m_PauseMenu.SetActive (true);
		}
	}
}

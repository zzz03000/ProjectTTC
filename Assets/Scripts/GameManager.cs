using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

	[SerializeField]
	private Image m_GameOver = null;
	[SerializeField]
	private Image m_Winner = null;

	[SerializeField]	
	private int m_numLifes = 3;			// numero di vite del giocatore

	[SerializeField]
	private float m_safeTime = 2;		// tempo prima di perdere un'altra vita

	[SerializeField]
	private Health m_Health = null;

	private float m_timeBeforeLosingAnotherLife = 0;	// usato in combinazione con m_safeTime
	private bool m_canYouLoseLife = true;				// usato in combinazione con m_safeTime

	// ------------------------------------

	private bool m_GameRunning = false;
	private bool m_Victory = false;

	void Start ()
	{
		m_GameRunning = true;

		if ( m_GameOver != null )
			m_GameOver.enabled = false;

		if ( m_Winner != null )
			m_Winner.enabled = false;	


		if (m_Health == null)
		{
			Debug.Log ("Inserisci il Prefab della vita nel GameObject");
			return;
		}
		Blocks_Generator generator = GameObject.Find("GameManager").GetComponent<Blocks_Generator>();
		generator.GenerateBlock ();
		Time.timeScale = 1f;

	}

	// Update is called once per frame
	void Update ()
	{
		if (m_GameRunning)
		{

			// se hai perso tutte le vite il gioco finisce
			if (m_numLifes == 0)
			{
				m_Victory = false;
				m_GameRunning = false;
				EndGame();
			}

			if (m_timeBeforeLosingAnotherLife <= 0)
				m_canYouLoseLife = true;
			else
				m_timeBeforeLosingAnotherLife -= Time.deltaTime;
			
		} else {
			EndGame ();
		}

	}

	public bool isGameRunning()
	{
		return m_GameRunning;
	}

	public void setGameRunning(bool state)
	{
		m_GameRunning = state;
	}

	public void setVictory(bool state)
	{
		m_Victory = state;
	}

	public void youLoseLife ()
	{
		if (m_canYouLoseLife == true)
		{
			m_timeBeforeLosingAnotherLife = m_safeTime;
			m_canYouLoseLife = false;
			--m_numLifes;

			m_Health.removeHeart ();
		}
	}

	private void EndGame()
	{
		if (m_Victory)
		{
			if (m_Winner != null)
				m_Winner.enabled = true;
		} else
		{
			if (m_GameOver != null)
				m_GameOver.enabled = true;
		}
		Time.timeScale = 0f;

	}
}

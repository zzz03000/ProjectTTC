using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

	[SerializeField]
	private Image P1_m_GameOver = null;
	[SerializeField]
	private Image P1_m_Winner = null;
    [SerializeField]
    private Image P2_m_GameOver = null;
    [SerializeField]
    private Image P2_m_Winner = null;

    [SerializeField]	
	private int P1_m_numLifes = 3;          // numero di vite del giocatore
    [SerializeField]
    private int P2_m_numLifes = 3;          // numero di vite del giocatore

    [SerializeField]
	private float m_safeTime = 2;		// tempo prima di perdere un'altra vita

	[SerializeField]
	private Health p1_m_Health = null;
	[SerializeField]
    private Health p2_m_Health = null;

    private float P1_m_timeBeforeLosingAnotherLife = 0; // usato in combinazione con m_safeTime
    private float P2_m_timeBeforeLosingAnotherLife = 0; // usato in combinazione con m_safeTime
    private bool P1_m_canYouLoseLife = true;                // usato in combinazione con m_safeTime
    private bool P2_m_canYouLoseLife = true;                // usato in combinazione con m_safeTime

    // ------------------------------------

    private bool m_GameRunning = false;
	private bool P1_m_Victory = false;

	void Start ()
	{
		m_GameRunning = true;

		if (P1_m_GameOver != null)
		{
			P1_m_GameOver.enabled = false;
		}

		if (P1_m_Winner != null)
		{
			P1_m_Winner.enabled = false;
		}

        if (P2_m_GameOver != null)
        {
            P2_m_GameOver.enabled = false;
        }

        if (P2_m_Winner != null)
        {
            P2_m_Winner.enabled = false;
        }


        if (p1_m_Health == null || p2_m_Health == null)
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
			if (P1_m_numLifes == 0 || P2_m_numLifes == 0)
			{
                P1_m_Victory = false;
				m_GameRunning = false;
				EndGame();
			}

			if (P1_m_timeBeforeLosingAnotherLife <= 0)
			{
                P1_m_canYouLoseLife = true;
			}
			else
			{
				P1_m_timeBeforeLosingAnotherLife -= Time.deltaTime;
			}
            if (P2_m_timeBeforeLosingAnotherLife <= 0)
            {
                P2_m_canYouLoseLife = true;
            }
            else
            {
                P2_m_timeBeforeLosingAnotherLife -= Time.deltaTime;
            }

        }
		else
		{
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
        P1_m_Victory = state;
	}

	public void youLoseLife (string player)
	{
		if (P1_m_canYouLoseLife == true)
		{
			switch(player)
			{
                case "P1":
                    P1_m_timeBeforeLosingAnotherLife = m_safeTime;
                    P1_m_canYouLoseLife = false;
                    --P1_m_numLifes;

                    p1_m_Health.removeHeart();
                    break;
                case "P2":
                    P2_m_timeBeforeLosingAnotherLife = m_safeTime;
                    P2_m_canYouLoseLife = false;
                    --P2_m_numLifes;

                    p2_m_Health.removeHeart();
                    break;
            }
		}
	}

	private void EndGame()
	{
		if (P1_m_Victory)
		{
			if (P1_m_Winner != null)
			{
				P1_m_Winner.enabled = true;
			}
            if (P2_m_GameOver != null)
            {
                P2_m_GameOver.enabled = true;
            }
        }
		else
		{
			if (P1_m_GameOver != null)
			{
				P1_m_GameOver.enabled = true;
			}
            if (P2_m_Winner != null)
            {
                P2_m_Winner.enabled = true;
            }
        }
		Time.timeScale = 0f;

	}
}

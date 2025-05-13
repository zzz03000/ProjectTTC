using UnityEngine;
using System.Collections;

public class Block_OnFalling : MonoBehaviour
{

	[SerializeField]
	private float m_FallingSpeed = 1.5f;
	private float m_ActualFallingSpeed = 0f;

	[SerializeField]
	private float m_Acceleration = 2f;

	[SerializeField]
	private GameObject m_GameManager = null;

	private bool m_Collided = false;

	[SerializeField]
	private float m_MovementSpeed = 10f;

	[SerializeField]
	private int m_StopFramesStep = 1;
	private int countStopFramesStep = 0;

	private bool repeatGeneration = true;

	void Awake()
	{
		Rigidbody2D myRB2D = GetComponent<Rigidbody2D> ();
		myRB2D.gravityScale = 0f;
		m_GameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 position = transform.position;
		position += m_ActualFallingSpeed * Vector3.down * Time.fixedDeltaTime;
		transform.position = position;

		m_ActualFallingSpeed = m_FallingSpeed;
		position = transform.position;

		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if (countStopFramesStep > m_StopFramesStep)
			{
				position.x -= m_MovementSpeed * Time.deltaTime;
				gameObject.transform.position = position;
				countStopFramesStep = 0;
			}
			else
			{
				countStopFramesStep++;
			}
		}

		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if (countStopFramesStep > m_StopFramesStep)
			{
				position.x += m_MovementSpeed * Time.deltaTime;
				gameObject.transform.position = position;
				countStopFramesStep = 0;
			}
			else
			{
				countStopFramesStep++;
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse0))
		{
			gameObject.transform.Rotate (new Vector3 (0f, 0f, +90f));
		}		

		if (Input.GetKeyDown (KeyCode.Mouse1))
		{
			gameObject.transform.Rotate (new Vector3 (0f, 0f, -90f));
		}

		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			m_ActualFallingSpeed *= m_Acceleration;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (m_Collided)
			return;

		m_Collided = true;

		if(other.gameObject.tag.Equals("Block") || other.gameObject.name.Equals("Base") || other.gameObject.tag.Equals("P1_Limite") || other.gameObject.tag.Equals("P2_Limite"))
		{
			Rigidbody2D myRB2D = GetComponent<Rigidbody2D> ();
			myRB2D.gravityScale = 1f;
			if(m_GameManager!=null && repeatGeneration)
			{
				Blocks_Generator generator = m_GameManager.GetComponent<Blocks_Generator>();
				generator.GenerateBlock ();
				repeatGeneration = false;
			}
			this.enabled = false;
		}
	}
}

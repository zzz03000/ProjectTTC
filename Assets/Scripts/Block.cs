using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.tag.Equals("Limite")){
			GameManager.FindObjectOfType<GameManager> ().youLoseLife ();
			Destroy (this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject[] boxPrefabs;
	
	public float spawnDelay = 3f;
	public float nextSpawn = 1f;
	
	public float initialVelocity = 2f;

	GameObject bGO;

	public void StartGame() {
		Application.LoadLevel ("Game");
	}

	public void Quit(){
		Application.Quit ();
	}

	void Update () {
		nextSpawn -= Time.deltaTime;
		
		if (nextSpawn <= 0) {
			nextSpawn = spawnDelay;
			bGO = (GameObject)Instantiate (boxPrefabs [Random.Range (0, boxPrefabs.Length)], transform.position, transform.rotation * Quaternion.Euler (0, 0, 90));
			bGO.GetComponent<Rigidbody2D> ().velocity = transform.rotation * new Vector2 (initialVelocity, 0);
		}		
	}

}

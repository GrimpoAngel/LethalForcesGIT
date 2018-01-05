using UnityEngine;

public class PlayerSpawnPoint_P1 : MonoBehaviour {

	public GameObject defaultPlayerPrefab1;
	public GameObject defaultCamFollow;

	void Awake(){

		//get selected player from character selection screen
		if(GlobalGameData_LF.Player1Prefab) {
			loadPlayer1(GlobalGameData_LF.Player1Prefab);
            return;
		}	

		//otherwise load default character
		if(defaultPlayerPrefab1) {
			loadPlayer1(defaultPlayerPrefab1);
			CameraFollow (defaultCamFollow);
		} else {
			Debug.Log("Please assign a default player prefab in the  playerSpawnPoint");
		}
	}

	//load a player prefab
	void loadPlayer1(GameObject playerPrefab){
		GameObject player1 = GameObject.Instantiate(playerPrefab) as GameObject;
		player1.transform.position = transform.position;
	}

	void CameraFollow(GameObject camPrefab){
		GameObject camfollow = GameObject.Instantiate (camPrefab) as GameObject;
		camfollow.transform.position = transform.position;
	}

}
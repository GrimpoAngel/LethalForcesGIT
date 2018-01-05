using UnityEngine;

public class PlayerSpawnPoint_P2 : MonoBehaviour {


    public GameObject defaultPlayerPrefab2;
    public KeyCode Enter2p = KeyCode.P;

    //If key is pressed, spawn player 2
    private void Update()
    {
        loadPlayer2(defaultPlayerPrefab2);
    }


    //load a player2 prefab
    //Turn off Player2 spawn point
    void loadPlayer2(GameObject playerPrefab2)
    {
        if (Input.GetKeyDown(Enter2p))
        {
            GameObject player2 = GameObject.Instantiate(playerPrefab2) as GameObject;
            player2.transform.position = transform.position;
            gameObject.SetActive(false);
        }

    }

}
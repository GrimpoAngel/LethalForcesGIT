using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlayer2 : MonoBehaviour {

    public KeyCode Enter2p = KeyCode.P;
    public GameObject SpawnPoint2p;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartPlayer2();
	}

    void StartPlayer2()
    {
        if (Input.GetKeyDown(Enter2p))
        {
            Debug.Log("Pressing P");
            SpawnPoint2p.SetActive(true);
        }

    }
}

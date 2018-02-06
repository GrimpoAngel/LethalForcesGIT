using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene_Shows : MonoBehaviour {

    public string Player1Level;
    public string Player2Level;

    UIManager UI;

    // Use this for initialization
    void Start () {
        //Destroy(UI.transform.parent);
        Destroy(GameObject.Find("UI(Clone)"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevelP1()
    {
        SceneManager.LoadScene(Player1Level);
    }

    public void LoadLevelP2()
    {
        SceneManager.LoadScene(Player2Level);
    }

}

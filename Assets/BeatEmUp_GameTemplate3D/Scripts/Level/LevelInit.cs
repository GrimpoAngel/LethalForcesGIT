using UnityEngine;

public class LevelInit : MonoBehaviour {

	[Space(5)]
	[Header ("Settings")]
	public string LevelMusic = "Music";
	public string showMenuAtStart = "";
	public bool playMusic = true;
	public bool createUI;
	public bool createInputManager;
    public bool createInputManager_P2;
	public bool createAudioPlayer;
	public bool createGameCamera;
	private GameObject audioplayer;
	private GameSettings settings;

	void Awake() {

		//set settings
		settings = Resources.Load("GameSettings", typeof(GameSettings)) as GameSettings;
		if(settings != null){
			Time.timeScale = settings.timeScale;
			Application.targetFrameRate = settings.framerate;
		}

		//create Audio Player
		if(!GameObject.FindObjectOfType<BeatEmUpTemplate.AudioPlayer>() && createAudioPlayer)	audioplayer = GameObject.Instantiate(Resources.Load("AudioPlayer"), Vector3.zero, Quaternion.identity) as GameObject;

		//create InputManager
		if(!GameObject.FindObjectOfType<InputManager>() && createInputManager) GameObject.Instantiate(Resources.Load("InputManager"), Vector3.zero, Quaternion.identity);
        
        //create InputManager Player 2 - LETHAL FORCES
        if (!GameObject.FindObjectOfType<InputManager_P2>() && createInputManager_P2) GameObject.Instantiate(Resources.Load("InputManager_P2"), Vector3.zero, Quaternion.identity);

        //create UI
        if (!GameObject.FindObjectOfType<UIManager>() && createUI) GameObject.Instantiate(Resources.Load("UI"), Vector3.zero, Quaternion.identity);
	
		//create Game Camera
		if(!GameObject.FindObjectOfType<CameraFollow>() && createGameCamera) GameObject.Instantiate(Resources.Load("GameCamera"), Vector3.zero, Quaternion.identity);

		//start music
		if(playMusic && createAudioPlayer) Invoke("PlayMusic", 1f);

		//open a menu at level start
		if(!string.IsNullOrEmpty(showMenuAtStart)) ShowMenuAtStart();
	}

    //LETHAL FORCES - Press ESC show Pause
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
        
    }

    //LETHAL FORCES - Pause Menu
    void ShowPauseMenu()
    {
        UIManager UI = GameObject.FindObjectOfType<UIManager>();
        UI.DisableAllScreens();
        UI.ShowMenu("Pause_Menu");
        Time.timeScale = 0;
    }


    void PlayMusic() {
		if(audioplayer != null)	audioplayer.GetComponent<BeatEmUpTemplate.AudioPlayer>().playMusic(LevelMusic);
	}

	void ShowMenuAtStart() {
		 GameObject.FindObjectOfType<UIManager>().ShowMenu(showMenuAtStart);
	}
}
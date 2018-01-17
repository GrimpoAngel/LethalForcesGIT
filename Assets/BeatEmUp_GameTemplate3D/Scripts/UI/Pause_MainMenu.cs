using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_MainMenu : MonoBehaviour {

	public GameObject PauseMenu;
	public Gradient ColorTransition;
	public float speed = 3.5f;
	public UIFader fader;
	private bool restartInProgress = false;

	private void OnEnable() {
		InputManager.onCombatInputEvent += InputEvent;
        InputManager.onCombatInputEvent += InputEvent2;
    }

	private void OnDisable() {
		InputManager.onCombatInputEvent -= InputEvent;
        InputManager.onCombatInputEvent -= InputEvent2;
    }

	//input event
	private void InputEvent(INPUTACTION action) {
		if (action == INPUTACTION.JUMP) LoadMainMenu();
	}

    private void InputEvent2(INPUTACTION action)
    {
        if (action == INPUTACTION.KICK) ResumeGame();
    }

	void ResumeGame(){

            if (PauseMenu.gameObject.activeInHierarchy == true)
            {
                PauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
	}

	//restarts the current level
	void LoadMainMenu(){

        Time.timeScale = 1; //LETHAL FORCES

		if(!restartInProgress){
			restartInProgress = true;

			//sfx
			GlobalAudioPlayer.PlaySFX("ButtonStart");

			//button flicker
			ButtonFlicker bf =  GetComponentInChildren<ButtonFlicker>();
			if(bf != null) bf.StartButtonFlicker();

			//fade out
			fader.Fade(UIFader.FADE.FadeOut, 0.5f, 0.5f);

			//reload level
			Invoke("GoToTitleScreen", 1f);
		}
	}

	void GoToTitleScreen(){
		restartInProgress = false;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Title_Screen");
	}
}

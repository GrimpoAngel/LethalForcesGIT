using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Restart : MonoBehaviour {

	public Text restarttext;
	public Gradient ColorTransition;
	public float speed = 3.5f;
	public UIFader fader;
	private bool restartInProgress = false;

	private void OnEnable() {
		InputManager.onCombatInputEvent += InputEvent;
    }

	private void OnDisable() {
		InputManager.onCombatInputEvent -= InputEvent;
    }

	//input event
	private void InputEvent(INPUTACTION action) {
		if (action == INPUTACTION.PUNCH || action == INPUTACTION.KICK) RestartLevel();
	}

	void Update(){

		//text effect
		if(restarttext != null && restarttext.gameObject.activeSelf){
			float t = Mathf.PingPong(Time.time * speed, 1f);
			restarttext.color = ColorTransition.Evaluate(t);
		}

		//alternative input events
		if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)){
			RestartLevel();
		}
	}

	//restarts the current level
	void RestartLevel(){

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
			Invoke("RestartScene", 1f);
		}
	}

	void RestartScene(){
		restartInProgress = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

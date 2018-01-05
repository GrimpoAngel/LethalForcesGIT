using UnityEngine;
using System.Collections;

public class InputManager_P2 : MonoBehaviour {

	public bool UseKeyboardInput;
	public bool UseJoypadInput;
	public bool UseTouchScreenInput;

	[Header("Keyboard keys")]
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode PunchKey = KeyCode.Z;
	public KeyCode KickKey = KeyCode.X;
	public KeyCode DefendKey = KeyCode.C;
	public KeyCode JumpKey = KeyCode.Space;

	[Header("Joypad keys")]
    public KeyCode JoypadPunch2 = KeyCode.Joystick2Button2;
    public KeyCode JoypadKick2 = KeyCode.Joystick2Button3;
    public KeyCode JoypadDefend2 = KeyCode.Joystick2Button1;
    public KeyCode JoypadJump2 = KeyCode.Joystick2Button0;

    //delegates
    public delegate void InputEventHandler(Vector2 dir);
	public static event InputEventHandler onInputEvent;
	public delegate void CombatInputEventHandler(INPUTACTION_2 action);
	public static event CombatInputEventHandler onCombatInputEvent;

	[Space(15)]
	public UIManager _UIManager; //link to the UI manager
	[HideInInspector]
	public Vector2 dir;
	private bool TouchScreenActive;
	public static bool defendKeyDown;

	void Start(){
		_UIManager = GameObject.FindObjectOfType<UIManager>();

		//automatically enable touch controls on IOS or android
		#if UNITY_IOS || UNITY_ANDROID
			UseTouchScreenInput = true;
			UseKeyboardInput = UseJoypadInput = false;
		#endif
	}

	public static void InputEvent(Vector2 dir){
		if( onInputEvent != null) onInputEvent(dir);
	}

	public static void CombatInputEvent(INPUTACTION_2 action){
		if(onCombatInputEvent != null) onCombatInputEvent(action);
	}

	public static void OnDefendButtonPress(bool state){
		defendKeyDown = state;
	}
		
	void Update(){

		//use keyboard
		if (UseKeyboardInput) KeyboardControls();

		//use joypad
		if (UseJoypadInput) JoyPadControls();

		//use touchScreen
		EnableDisableTouchScrn(UseTouchScreenInput);
	}

	void KeyboardControls(){
		
		//vector
		float x = 0f;
	 	float y = 0f;

		if (Input.GetKey (Left)) x = -1f;
		if (Input.GetKey (Right))x = 1f;
		if (Input.GetKey (Up)) y = 1f;
		if (Input.GetKey (Down)) y = -1f;
	
		dir = new Vector2(x,y);
		InputEvent(dir);

		//Combat input
		if(Input.GetKeyDown(PunchKey)){
			CombatInputEvent(INPUTACTION_2.PUNCH);
		}

		if(Input.GetKeyDown(KickKey)){
			CombatInputEvent(INPUTACTION_2.KICK);
		}
			
		if(Input.GetKeyDown(JumpKey)){
			CombatInputEvent(INPUTACTION_2.JUMP);
		}

		defendKeyDown = Input.GetKey(DefendKey);
	}

	void JoyPadControls(){
		float x = Input.GetAxis("Joypad Left-RightP2");
		float y = Input.GetAxis("Joypad Up-DownP2");

		dir = new Vector2(x,y);
		InputEvent(dir.normalized);

		if(Input.GetKeyDown(JoypadPunch2)){
			CombatInputEvent(INPUTACTION_2.PUNCH);
		}

        if (Input.GetKeyDown(JoypadKick2)){
			CombatInputEvent(INPUTACTION_2.KICK);
		}

		if(Input.GetKey(JoypadJump2)){
			CombatInputEvent(INPUTACTION_2.JUMP);
		}

		defendKeyDown = Input.GetKey(JoypadDefend2);
	}

	//enables or disables the touch screen interface
	public void EnableDisableTouchScrn(bool state){
		InputEvent(dir.normalized);

		if (_UIManager != null) {
			if (state) {
				
				//show touch screen
				if(!TouchScreenActive) {
					_UIManager.ShowMenu ("TouchScreenControls", false);
					TouchScreenActive = true;
				}

			} else {

				//hide touch screen
				if (TouchScreenActive) {
					TouchScreenActive = false;
					_UIManager.CloseMenu ("TouchScreenControls");
				}
			}
		}
	}

	//returns true if the defend key is held down
	public bool isDefendKeyDown(){
		return defendKeyDown;
	}
}

public enum INPUTACTION_2 {
	NONE,
	PUNCH,
	KICK,
	JUMP,
	DEFEND,
	WEAPONATTACK,
}

public enum INPUTTYPE2 {
	KEYBOARD = 0,	
	JOYPAD = 5,	
	TOUCHSCREEN = 10, 
}

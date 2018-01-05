using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarP2 : MonoBehaviour {

	public Text nameField;
	public Slider HpSlider;
	public bool isPlayer;

	void OnEnable() {
		HealthSystemP2.onHealthChange += UpdateHealth;
	}

	void OnDisable() {
		HealthSystemP2.onHealthChange -= UpdateHealth;
	}

	void Start(){
		HpSlider.gameObject.SetActive(isPlayer);
	}

	void UpdateHealth(float percentage, GameObject go){
		if(isPlayer && go.CompareTag("Player")){
			HpSlider.value = percentage;
		} 	

		if(!isPlayer && go.CompareTag("Enemy")){
			HpSlider.gameObject.SetActive(true);
			HpSlider.value = percentage;
			nameField.text = go.GetComponent<EnemyActions>().enemyName;
			if(percentage == 0) Invoke("HideOnDestroy", 2);
		}
	}

	void HideOnDestroy(){
		HpSlider.gameObject.SetActive(false);
		nameField.text = "";
	}
}

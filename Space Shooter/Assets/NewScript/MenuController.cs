using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

    public GameObject startGameButton;
    public GameObject MainMenu;
    public ToggleGroup movementTypeRadio;
    public ToggleGroup difficultyRadio;
    public Slider accelerometerSlider;
    public Text accelerometerCurrentValue;

    void Start()
    {
        accelerometerSlider.value = PlayerPrefs.GetFloat("accelerometer");
        UpdateAccelerometerSlider();
    }
	
    public void StartGame()
    {
        PlayerPrefs.SetInt("movementType", 
                                movementTypeRadio.ActiveToggles().FirstOrDefault().ToString().Contains("Tactile Movement Radio") ? 0 : 1);
        PlayerPrefs.SetInt("difficulty", 
            difficultyRadio.ActiveToggles().FirstOrDefault().ToString().Contains("Normal Radio") ? 1 :
            difficultyRadio.ActiveToggles().FirstOrDefault().ToString().Contains("Easy Radio") ? 0 : 2);
        PlayerPrefs.SetFloat("accelerometer", accelerometerSlider.value);
        SceneManager.LoadScene(1);
    }

    public void UpdateMovementType()
    {
        var f = movementTypeRadio.ActiveToggles().FirstOrDefault();
    }

    public void UpdateAccelerometerSlider()
    {
        accelerometerCurrentValue.text = accelerometerSlider.value.ToString("0.00");
    }
}

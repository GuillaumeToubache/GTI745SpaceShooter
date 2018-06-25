using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MenuController : MonoBehaviour {

    public GameObject startGameButton;
    public ToggleGroup movementTypeRadio;
    public ToggleGroup difficultyRadio;
    public Slider accelerometerSlider;
    public Text accelerometerCurrentValue;

    void Start()
    {

    }
	
    public void StartGame()
    {

    }

    public void UpdateMovementType()
    {
        var f = movementTypeRadio.ActiveToggles().FirstOrDefault();
        Debug.Log(f.gameObject.name);
    }

    public void UpdateAccelerometerSlider()
    {
        accelerometerCurrentValue.text = accelerometerSlider.value.ToString("0.00");
    }
}

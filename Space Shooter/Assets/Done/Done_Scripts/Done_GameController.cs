using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
    	
	public Text scoreText;
	public Text gameOverText;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    
    private bool gameOver;
	private int score;
    private bool isTouchPadActive;
    private int difficulty;
    private float accelerometerValue;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
	{
        isTouchPadActive = PlayerPrefs.GetInt("movementType", 0) == 0;
        accelerometerValue = PlayerPrefs.GetFloat("accelerometer");
        difficulty = PlayerPrefs.GetInt("difficulty", 1);
        gameOver = false;
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
		
	IEnumerator SpawnWaves()
	{
        int hazardLength = hazards.Length;
        if (difficulty == 0) {
            hazardLength = 1;
        }
        else if (difficulty == 2) {
            hazardCount = (int) (hazardCount * 1.5);
            spawnWait = spawnWait / 1.5f;
        }

		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
                Debug.Log(hazardCount);
                GameObject hazard = hazards[Random.Range(0, hazardLength)];
                if (difficulty == 0)
                {
                    hazard = hazards[0];
                }
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if(gameOver)
			{
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
                break;
			}
		}
	}
	
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public bool GetMovementType() {
        return isTouchPadActive;
    }

    public float GetAccelerometerValue()
    {
        return accelerometerValue;
    }
}
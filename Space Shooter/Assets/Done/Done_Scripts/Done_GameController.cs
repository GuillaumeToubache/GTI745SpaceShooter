﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
    public List<GameObject> hazardArrayList;
     	
	public Text scoreText;
	public Text gameOverText;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public Slider secondaryWeaponCharge;
    public GameObject secondaryWeapon;

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
        secondaryWeapon.SetActive(false);
        secondaryWeaponCharge.value = 0;
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
                GameObject hazard = hazards[Random.Range(0, hazardLength)];
                hazardArrayList.Add(hazard);
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
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
        Debug.Log(newScoreValue);
		score += newScoreValue;
        Debug.Log(score);
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

    public void DestroyAllHazards()
    {
        foreach (var hazard in GameObject.FindGameObjectsWithTag("Ship"))
        {
            Debug.Log(hazard.tag);
            AddScore(20);
            Destroy(hazard);
        }
        foreach (var hazard in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Debug.Log(hazard.tag);
            AddScore(10);
            Destroy(hazard);
        }
        secondaryWeaponCharge.value = 0;
        secondaryWeapon.SetActive(false);
    }
}
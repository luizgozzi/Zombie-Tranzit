using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Manager : MonoBehaviour{
    int score;
    public static Game_Manager inst;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] Slider fuelSlider;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Coins: " + score;

    }

    private void Awake()
    {
        inst = this;
    }

    public void UpdateFuelBar(float currentFuel, float maxFuel)
    {
        fuelSlider.value = currentFuel;
        fuelSlider.maxValue = maxFuel;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pausegame()
    {
        Time.timeScale = 0f;
    }

    public void Unpaused()
    {
        Time.timeScale = 1f;
    }
}

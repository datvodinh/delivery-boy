using UnityEngine;
using TMPro; // If using TextMeshPro
using UnityEngine.UI; // If using standard UI Text

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Use TextMeshProUGUI for TextMeshPro
    // public Text timerText; // Use Text for standard UI Text

    public float startTime = 60f; // Set the starting time in seconds
    private float currentTime;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0;
            // Optionally, add code to handle what happens when the timer reaches zero
        }

        DisplayTime(currentTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // To avoid displaying 59.999999 seconds

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
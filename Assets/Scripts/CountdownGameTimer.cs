using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class CountdownGameTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 180f;

    public TextMeshProUGUI countdownText; // Change the type to TextMeshProUGUI

    public void Start()
    {
        currentTime = startTime;
        UpdateCountdownText(); // Update the text initially to show the starting time
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
                // Handle countdown end here (e.g., game over logic)
            }
            UpdateCountdownText();
        }
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        string timerText = string.Format("{0:0}:{1:00}", minutes, seconds);
        countdownText.text = timerText;
    }
}
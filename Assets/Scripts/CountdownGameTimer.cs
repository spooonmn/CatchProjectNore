using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;
using UnityEngine.UI;

public class CountdownGameTimer : MonoBehaviour
{ float currentTime = 0f;
    float startTime = 180f;

    public Text countdownText;

    public void Start()
    {
        currentTime = startTime;
    }
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        print(currentTime);
        countdownText.text = currentTime.ToString();
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class timerController : MonoBehaviour
{
    public float timer = -1.0f;
    public UnityEvent onTimerEnded = new UnityEvent();
    public TMP_Text timerText;

    public void beginTimer(float seconds)
    {
        timer = seconds;
        Debug.Log($"Timer began!", this);
    }

    public void UpdateText()
    {
        if (timer <= 0)
        {
            timerText.text = "00:00";
        }
        else
        {
            TimeSpan t = TimeSpan.FromSeconds(timer);
            string str = t.ToString(@"mm\:ss");
            timerText.text = str;
        }

    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0 && timer > -100)
        {
            onTimerEnded.Invoke();
            timer -= 1000f;
        }
        UpdateText();
    }
}

using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hudController : MonoBehaviour
{
    [SerializeField] TMP_Text lifeTxt;
    [SerializeField] TMP_Text timeTxt;
    [SerializeField] Image filledImageCunai;
    [SerializeField] Image filledImageCheto;
    [SerializeField] Image filledImageMonster;
    [SerializeField] Image filledImageDice;
    [SerializeField] Image filledImageFart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLifeText(int lifeCount){
        lifeTxt.SetText(lifeCount.ToString());
    }

    public void setTimeText(float timeRemaining){
        var span = new TimeSpan(0, 0, (int)Math.Round(timeRemaining)); //Or TimeSpan.FromSeconds(seconds); (see Jakob C´s answer)
        var stringTime = string.Format("{0}:{1:00}", (int)span.TotalMinutes, span.Seconds);
        timeTxt.SetText(stringTime);
    }

    public void setFilledAmountImageCheto(float fillamount){
        filledImageCheto.fillAmount = fillamount;
    }
    public void setFilledAmountImageCunai(float fillamount){
        filledImageCunai.fillAmount = fillamount;
    }
    public void setFilledAmountImageMonster(float fillamount){
        filledImageMonster.fillAmount = fillamount;
    }

    public void setFilledAmountImageDice(float fillamount){
        filledImageDice.fillAmount = fillamount;
    }
    public void setFilledAmountImageFart(float fillamount){
        filledImageFart.fillAmount = fillamount;
    }
}

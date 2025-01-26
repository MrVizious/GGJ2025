using UnityEngine;
using UnityEngine.Events;

public class timerController : MonoBehaviour
{
    float timer = 0.0f;
    public UnityEvent onTimerEnded = new UnityEvent();

    public void beginTimer(float seconds){
        timer = seconds;
    }


    void Update(){
        if(timer > 0 ){
            timer -= Time.deltaTime;
        } 
        if(timer == 0){
            onTimerEnded.Invoke();
            timer -= 1f;
        }
    }
}

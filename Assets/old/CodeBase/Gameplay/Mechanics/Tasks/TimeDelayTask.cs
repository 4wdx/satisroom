using System.Collections;
using UnityEngine;

namespace CodeBase.Gameplay.Mechanics
{
    public class TimeDelayTask : Task
    {
        [SerializeField] private float _timeDelay;
        
        private void Start() => 
            OnStart += StartTimer;

        private void StartTimer() => 
            StartCoroutine(Timer(_timeDelay));

        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            StopTask();

            OnStart -= StartTimer;
        }
    }
}

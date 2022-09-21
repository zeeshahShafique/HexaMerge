using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Core.TimeMachine
{
    [CreateAssetMenu(fileName = "TimeMachine", menuName = "Utilities/TimeMachine")]
    public class TimeMachine : ScriptableObject
    {
        [Header("Events Raised")]
        [SerializeField] private UnityEvent TickEvent;
        private readonly WaitForSeconds _waitForOneSecond = new(1);

        public IEnumerator Tick()
        {
            while (true)
            {
                yield return _waitForOneSecond;
                TickEvent.Invoke();
            }
        }
    }
}
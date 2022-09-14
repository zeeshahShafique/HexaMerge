using Core.TimeMachine;
using UnityEngine;

public class StartTimeMachine : MonoBehaviour
{
    [SerializeField] private TimeMachine TimeMachine;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(TimeMachine.Tick());
    }

    
}

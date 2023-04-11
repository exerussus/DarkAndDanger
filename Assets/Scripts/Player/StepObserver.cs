
using UnityEngine;
using System;

public class StepObserver : MonoBehaviour
{
    public Action OnStep;

    public void Step()
    {
        OnStep?.Invoke();
    }
}

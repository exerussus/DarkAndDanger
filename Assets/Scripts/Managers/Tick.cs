
using System;
using UnityEngine;

public class Tick : MonoBehaviour
{
    public static Tick instance = null;

    [SerializeField] private float oneTickTime = 1f;
    private float _tickTimer;
    public static float time;

    public static Action OnTick;
    public static Action OnFixedUpdate;
    
    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        time = Time.fixedTime;
        OnFixedUpdate?.Invoke();
        if (_tickTimer > oneTickTime)
        {
            OnTick?.Invoke();
            _tickTimer = 0f;
        }
        else _tickTimer += Time.fixedDeltaTime;
        
    }
}


using System;
using UnityEngine;

public class VisionRayDirection : MonoBehaviour
{
    [SerializeField] public Character character;
    [SerializeField] public int rayCount = 160;
    [SerializeField] private Transform playerTransform;

    public Action<Vector2> OnGetDirection;
    
    private void Start()
    {
        playerTransform = playerTransform == null ? GetComponent<Transform>() : playerTransform;
    }

    private void RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;

        for (int i = 0; i < rayCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += (character.Parameter.visionRadius / rayCount) * Mathf.Deg2Rad;

            Vector2 direction = transform.TransformDirection(new Vector2(x, y));
            OnGetDirection?.Invoke(direction);

            if (x != 0)
            {
                direction = transform.TransformDirection(new Vector3(-x, y, 0));
                OnGetDirection?.Invoke(direction);
            }
        }
    }
    
    private void FixedUpdate()
    {
        RayToScan();
    }
}


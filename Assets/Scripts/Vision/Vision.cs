
using System;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] public Character character;
    [SerializeField] public int rayCount = 160;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int distance;

    public Action OnHitFOG;
    
    private void Start()
    {
        visibleLayer = LayerMask.GetMask("VisibleObject");
        playerTransform = playerTransform == null ? GetComponent<Transform>() : playerTransform;
    }
    
    bool GetRaycast(Vector2 dir)
    {
        bool result = false;
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, dir, distance, visibleLayer);
        Vector3 position = transform.position + offset;

        if(hit.collider != null)
        {
            result = true;
            VisibleObject visibleObject = hit.collider.GetComponent<VisibleObject>();
            visibleObject.Show();
            if(!visibleObject.IsAlwaysVisible) Debug.DrawLine(position, hit.point, Color.green);
            else Debug.DrawLine(position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawRay(position, dir * distance, Color.red);
        }
        return result;
    }
    
    bool RayToScan()
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

            Vector2 dir = transform.TransformDirection(new Vector2(x, y));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, y, 0));
                if (GetRaycast(dir)) b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }
    
    private void Update()
    {
        
        if (RayToScan())
        {
           
        }
        
    }

    
    
}
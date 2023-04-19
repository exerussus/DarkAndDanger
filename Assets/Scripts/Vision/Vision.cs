
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private LayerMask visibleLayer;
    [SerializeField] private Vector3 offset;
    [SerializeField] private int distance;
    [SerializeField] private VisionRayDirection visionRayDirection;

    private void OnEnable()
    {
        visionRayDirection.OnGetDirection += GetRaycast;
    }

    private void OnDisable()
    {
        visionRayDirection.OnGetDirection -= GetRaycast;
    }

    private void Start()
    {
        visibleLayer = LayerMask.GetMask("VisibleObject");
    }
    
    private void GetRaycast(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(visionRayDirection.UnitTransform.position, direction, distance, visibleLayer);
        Vector3 position = transform.position + offset;

        if(hit.collider != null)
        {
            VisibleObject visibleObject = hit.collider.GetComponent<VisibleObject>();
            visibleObject.Show();
            if(!visibleObject.IsAlwaysVisible) Debug.DrawLine(position, hit.point, Color.green);
            else Debug.DrawLine(position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawRay(position, direction * distance, Color.red);
        }
    }
}

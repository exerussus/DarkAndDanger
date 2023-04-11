
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -300);
        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }
}

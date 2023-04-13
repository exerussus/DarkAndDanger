using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public GameObject fogOfWarPlane;
    public Transform playerTransform;
    public LayerMask fogLayer;
    public float visionRadius = 5f;
    private float radiusSqr => visionRadius * visionRadius;

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;
    
    void Start()
    {
        Initialize();
    }
    
    void Update()
    {
        Ray ray = new Ray(transform.position, playerTransform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqr)
                {
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqr);
                    colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        mesh.colors = colors;
    }
}
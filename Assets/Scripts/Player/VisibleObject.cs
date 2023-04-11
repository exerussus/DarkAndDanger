using UnityEngine;

public class VisibleObject : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool isAlwaysVisible;
    [SerializeField] private bool isDetected;
    [SerializeField] private float hideTimeTimer;
    [SerializeField] private float hideTime;


    public bool IsAlwaysVisible => isAlwaysVisible;
    
    

    void Start()
    {
        if(!isAlwaysVisible) _renderer = _renderer == null ? GetComponent<Renderer>() : _renderer;
    }

    public void Show()
    {
        isDetected = true;
        if(!isAlwaysVisible && isDetected)
        {
            _renderer.enabled = true;
            hideTimeTimer = 0f;
        }
    }

    public void TryToHide()
    {
        if (hideTimeTimer > hideTime)
        {
            _renderer.enabled = false;
            isDetected = false;
        }
        else hideTimeTimer += Time.deltaTime;
    }
    
    void FixedUpdate()
    {
        if(!isAlwaysVisible) TryToHide();
    }
}

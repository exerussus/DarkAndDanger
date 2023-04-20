
using UnityEngine;

public class MainEffect : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    public void DestroyEffect()
    {
        Destroy(effectPrefab);
    }
    
}

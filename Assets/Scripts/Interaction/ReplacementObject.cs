
using UnityEngine;

public class ReplacementObject : MonoBehaviour
{
    [SerializeField] private GameObject newGameObject;
    [SerializeField] private Vector3 correction;

    public GameObject NewGameObject => newGameObject;
    public Vector3 CorrectionVector => correction;
}

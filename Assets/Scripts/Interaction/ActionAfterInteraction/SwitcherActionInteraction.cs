
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SwitcherActionInteraction : IActionAfterInterection
{
    [SerializeField] private GameObject firstGameObject;
    [SerializeField] private GameObject secondGameObject;
    [SerializeField] private bool isFirstOnStart;
    [SerializeField] private bool isSwitchOnlyOnce;
    private bool isSwitched;
    private bool isFirst;

    private void Start()
    {
        if (isFirstOnStart) SetFirstObject();
        else SetSecondObject();
    }

    public override void Action()
    {
        SwitchGameObjects();
        AdditionAction();
    }
    
    private void SetSecondObject()
    {
        firstGameObject.SetActive(false);
        secondGameObject.SetActive(true);
        isFirst = false;
    }
    
    private void SetFirstObject()
    {
        secondGameObject.SetActive(false);
        firstGameObject.SetActive(true);
        isFirst = true;
    }
    
    private void SwitchGameObjects()
    {
        if (isSwitched && isSwitchOnlyOnce) return;
        if (isFirst) SetSecondObject();
        else SetFirstObject();
        isSwitched = true;
    }

    protected virtual void AdditionAction()
    {
        
    }
}

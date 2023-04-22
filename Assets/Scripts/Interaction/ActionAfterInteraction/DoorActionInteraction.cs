
using UnityEngine;

public class DoorActionInteraction : IActionAfterInterection
{
    [SerializeField] private GameObject openedDoor;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private bool isOpenedOnStart;
    private bool isOpened;

    private void Start()
    {
        if (isOpenedOnStart) ChangeDoorMode();
    }

    public override void Action(GameObject gameObject)
    {
        ChangeDoorMode();
    }

    private void ChangeDoorMode()
    {
        if (isOpened)
        {
            closedDoor.SetActive(true);
            openedDoor.SetActive(false);
        }
        else
        {
            closedDoor.SetActive(false);
            openedDoor.SetActive(true);
        }
        isOpened = !isOpened;
    }
}

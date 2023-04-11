
using UnityEngine;
using UnityEngine.UI;

public class InteractionBar : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private Interaction interaction;
    [SerializeField] private Slider interactionSlider;

    private void OnEnable()
    {
        HideBar();
        interaction.OnProgress += Progress;
        interaction.OnInteraction += ShowBar;
        interaction.OnInteractionEnd += HideBar;
    }
    private void OnDisable()
    {
        interaction.OnProgress -= Progress;
        interaction.OnInteraction -= ShowBar;
        interaction.OnInteractionEnd -= HideBar;
    }

    private void HideBar()
    {
        bar.SetActive(false);
    }
    
    private void ShowBar()
    {
        bar.SetActive(true);
        interactionSlider.maxValue = interaction.InteractTime;
        interactionSlider.value = 0f;
    }
    
    private void Progress()
    {
        interactionSlider.value += Time.fixedDeltaTime;
    }
}

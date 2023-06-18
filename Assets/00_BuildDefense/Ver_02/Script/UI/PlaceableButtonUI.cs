using UnityEngine.UI;
using UnityEngine;
using Lam.DefenderBuilder.Tower;

public class PlaceableButtonUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] Image icon;
    [SerializeField] Image selectedBG;

    InteractableData itemData;
    public InteractableData ItemData => itemData;
    bool isSelected = false;

    public void SetData(InteractableData itemData)
    {
        this.itemData = itemData;
        SetButtonUI();
    }

    public void SetButtonUI()
    {
        icon.sprite = itemData.sprite;
        selectedBG.enabled = false;
    }

    public void SetSelectedActive(bool active)
    {
        selectedBG.enabled = active;
        isSelected = active;
    }

    public void ToggleSelected()
    {
        isSelected = !isSelected;
        selectedBG.enabled = isSelected;
    }
}

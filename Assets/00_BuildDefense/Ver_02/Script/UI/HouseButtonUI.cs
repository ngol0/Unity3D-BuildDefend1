using UnityEngine.UI;
using UnityEngine;
using Lam.DefenderBuilder.Tower;

public class HouseButtonUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] Image icon;
    [SerializeField] Image selectedBG;

    [Header("Event to raise")]
    [SerializeField] TowerTypeEvent OnButtonClicked;
    HouseData towerData;

    public void SetTowerData(HouseData towerData)
    {
        this.towerData = towerData;
        SetButtonUI();
    }

    public void SetButtonUI()
    {
        icon.sprite = towerData.sprite;
        selectedBG.enabled = false;
    }

    public void OnTowerSelect()
    {
        OnButtonClicked?.Raise(towerData);
    }
}

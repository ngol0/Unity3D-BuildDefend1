using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItemPanel : MonoBehaviour
{
    [SerializeField] PlaceableButtonUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] HouseDataList baseHouseList;

    [Header("Gameplay ref")]
    [SerializeField] GameplayController gameplayLogic;

    PlaceableButtonUI currentButton;

    private void Awake() 
    {
        buttonPrefab.gameObject.SetActive(false);

        foreach (var item in baseHouseList.list)
        {
            var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
            btn.SetData(item);
            btn.gameObject.SetActive(true);
        }
    }

    private void Start() 
    {
        gameplayLogic.OnItemPlaced += Panel_OnItemPlaced;
    }

    public void CheckSelectedBtn(PlaceableButtonUI btn)
    {
        if (btn == null) return;
        if (btn == currentButton) 
        {
            btn.ToggleSelected();
            return;
        }

        if (currentButton != null)
        {
            currentButton.SetSelectedActive(false);
        }
        btn.SetSelectedActive(true);
        currentButton = btn;
        gameplayLogic.SetActiveItemData(currentButton.ItemData);
    }

    public void Panel_OnItemPlaced()
    {
        currentButton.gameObject.SetActive(false);
        currentButton = null;
    }
}

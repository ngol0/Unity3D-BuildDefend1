using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI ref")]
    [SerializeField] PlaceableButtonUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] GameObject guiMain;

    [Header("Logic ref")]
    [SerializeField] GameplayController gameplayLogic;

    [Header("InventorySO")]
    [SerializeField] InventorySO inventory;

    PlaceableButtonUI currentButton;

    private void Awake() 
    {
        buttonPrefab.gameObject.SetActive(false);

        //init initial buildings
        if (inventory.interactableItemList.Count <= 0) return;
        foreach (var item in inventory.interactableItemList)
        {
            var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
            btn.SetData(item);
            btn.gameObject.SetActive(true);
        }
    }

    private void Start() 
    {
        gameplayLogic.OnItemPlaced += OnDonePlaced;
        gameplayLogic.OnCancelPlacedItem += ResetUI;

        inventory.OnAddComplete += InitNewItem;
    }

    //set current item when clicked into each item
    public void SetActiveInventoryItem(PlaceableButtonUI btn)
    {
        if (btn == null) return;
        if (btn == currentButton) //when clicked onto the selected button -> unselect
        {
            currentButton.SetSelectedActive(false);
            gameplayLogic.SetInventoryItem(null);
            currentButton = null;
            return;
        }

        if (currentButton != null) //select different btn
        {
            currentButton.SetSelectedActive(false);
        }
        btn.SetSelectedActive(true);
        currentButton = btn;

        gameplayLogic.SetInventoryItem(currentButton.ItemData); //set item to place
        gameplayLogic.CancelItemSelection(); //cancel item selection if one is selected
    }

    private void ResetUI()
    {
        if (currentButton == null) return;

        currentButton.SetSelectedActive(false);
        currentButton = null;
    }

    private void OnDonePlaced()
    {
        if (currentButton == null) return;

        currentButton.gameObject.SetActive(false);
        currentButton = null;
    }

    public void InitNewItem(InteractableData data)
    {
        var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
        btn.SetData(data);
        btn.gameObject.SetActive(true);
    }
}

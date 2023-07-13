using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPanelUI : MonoBehaviour
{
    [Header("UI ref")]
    [SerializeField] PlaceableButtonUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] GameObject guiMain;

    [Header("Logic ref")]
    [SerializeField] GameplayController gameplayLogic;

    PlaceableButtonUI currentButton;

    private void Awake() 
    {
        buttonPrefab.gameObject.SetActive(false);

        //init initial buildings
        foreach (var item in gameplayLogic.baseHouseList.list)
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
    }

    //set current item when clicked into each item
    public void SetPlaceableItem(PlaceableButtonUI btn)
    {
        if (btn == null) return;
        if (btn == currentButton) 
        {
            currentButton.SetSelectedActive(false);
            gameplayLogic.SetPlaceableItem(null);
            currentButton = null;
            return;
        }

        if (currentButton != null)
        {
            currentButton.SetSelectedActive(false);
        }
        btn.SetSelectedActive(true);
        currentButton = btn;

        gameplayLogic.SetPlaceableItem(currentButton.ItemData);
        gameplayLogic.CancelItemSelection();
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

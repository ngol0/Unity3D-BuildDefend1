using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItemPanel : MonoBehaviour
{
    [SerializeField] PlaceableButtonUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] HouseDataList baseHouseList;
    [SerializeField] GameObject guiMain;

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
        gameplayLogic.OnItemPlaced += OnDonePlaced;
        gameplayLogic.OnCancelPlacedItem += ResetUI;
    }

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
        Debug.Log("Eh?");
        var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
        btn.SetData(data);
        btn.gameObject.SetActive(true);
    }
}

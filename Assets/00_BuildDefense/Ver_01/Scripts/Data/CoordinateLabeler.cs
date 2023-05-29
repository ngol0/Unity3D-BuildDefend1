using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Lam.DefenderBuilder
{
    [ExecuteAlways]
    [RequireComponent(typeof(TextMeshPro))]
    public class CoordinateLabeler : MonoBehaviour
    {
        TextMeshPro label;
        Vector2Int coordinates = new Vector2Int();

        [Header("Ref")]
        [SerializeField] Waypoints waypoint;

        [Header("UI")]
        [SerializeField] Color blockColor;
        [SerializeField] Color defaultColor;

        #if UNITY_EDITOR
        private void Awake()
        {
            label = GetComponent<TextMeshPro>();
            DisplayCoordinate();
            label.enabled = false;
        }

        void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinate();
                UpdateObjectName();
                //label.enabled = true;
            }
            SetLabelColor();
            DebugToggleLabel();
        }

        private void DisplayCoordinate()
        {
            coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
            coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

            label.text = coordinates.x + "," + coordinates.y;
        }

        #endif

        private void UpdateObjectName()
        {
            transform.parent.name = coordinates.ToString();
        }

        private void SetLabelColor()
        {
            label.color = waypoint.IsPlaceable ? defaultColor : blockColor;
        }

        private void DebugToggleLabel()
        {
            if (Input.GetKeyDown(KeyCode.C))
                label.enabled = !label.IsActive();
        }
    }
}


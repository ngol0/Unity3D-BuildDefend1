using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lam.DefenderBuilder.Events
{
    public class MouseEnterEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public System.Action onMouseEnterEvent;
        public System.Action onMouseExitEvent;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseExitEvent.Invoke();
        }
    }

}
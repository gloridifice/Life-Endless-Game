using System;
using TMPro;
using UnityEngine;

namespace GGJ2023.UI
{
    public class IgnoreUIRaycast : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<TMP_Text>().raycastTarget = false;
        }
    }
}
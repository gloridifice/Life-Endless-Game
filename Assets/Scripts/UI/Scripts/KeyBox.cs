using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBox : MonoBehaviour
{
    private TMP_Text text;

    public TMP_Text Text
    {
        get
        {
            if (text == null)
            {
                text = GetComponentInChildren<TMP_Text>();
            }

            return text;
        }
    }
    
}

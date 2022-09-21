using System;
using TMPro;
using UnityEngine;

public class ChangeText : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI Option3Text;

     public void Change(String text)
     {
          Option3Text.text = text;
     }
}

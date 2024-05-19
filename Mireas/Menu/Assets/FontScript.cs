using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FontScript : MonoBehaviour
{
    public TMP_Text textMeshProText;
    public TMP_FontAsset newFont;

    void Start()
    {
        textMeshProText.font = newFont;
    }
}
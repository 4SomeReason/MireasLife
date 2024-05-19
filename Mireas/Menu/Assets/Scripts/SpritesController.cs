using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesController : MonoBehaviour
{
    //public Sprite[] sprites; // ������ �������� ��� ������������
    //private int currentSpriteIndex = 0;
    //private SpriteRenderer spriteRenderer;

    //void Start()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    if (spriteRenderer == null)
    //    {
    //        Debug.LogError("SpriteRenderer �� ������ �� �������!");
    //    }
    //    UpdateSprite();
    //}

    //void Update()
    //{
    //    // ������: ������������ �������� �� ������� ������� "������"
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
    //        UpdateSprite();
    //    }
    //}

    //void UpdateSprite()
    //{
    //    if (sprites.Length > 0 && currentSpriteIndex < sprites.Length)
    //    {
    //        spriteRenderer.sprite = sprites[currentSpriteIndex];
    //    }
    //}

    public bool isSwitched = false;
    public Image background1;
    public Image background2;

    public void SwitchImage(Sprite sprite)
    {
        if (!isSwitched) 
        { 
            background2.sprite = sprite;
        }
        else
        {
            background1.sprite = sprite;
        }
        isSwitched = !isSwitched;
    }
}
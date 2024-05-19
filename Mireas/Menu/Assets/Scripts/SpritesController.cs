using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesController : MonoBehaviour
{
    //public Sprite[] sprites; // Массив спрайтов для переключения
    //private int currentSpriteIndex = 0;
    //private SpriteRenderer spriteRenderer;

    //void Start()
    //{
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    if (spriteRenderer == null)
    //    {
    //        Debug.LogError("SpriteRenderer не найден на объекте!");
    //    }
    //    UpdateSprite();
    //}

    //void Update()
    //{
    //    // Пример: переключение спрайтов по нажатию клавиши "Пробел"
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
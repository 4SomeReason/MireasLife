using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.IO;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Xml.Linq;
using UnityEditor;

public class SpritesController : MonoBehaviour
{
    public JsonParse.JsonData jp;
    public string[] sprites;
    private int currentSpriteIndex = 0;
    private Image spriteRenderer;
    void Start()
    {
        sprites = new string[105];
        jp = GameObject.FindGameObjectWithTag("JsonParse").GetComponent<JsonParse>().data;
        for (int i = 1; i <= 105; i++)
        {
            sprites[i - 1] = jp.ReturnNode(i).Item2;
        }
        spriteRenderer = GetComponent<Image>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на объекте!");
        }
        UpdateSprite();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            UpdateSprite();
        }
    }

    void UpdateSprite()
    {
        if (sprites.Length > 0 && currentSpriteIndex < sprites.Length)
        {
            //C:\Users\Алексей\Documents\GitHub\MireasLife\Mireas\Menu\Assets\StreamingAssets\1.jpeg
            Debug.Log(Application.streamingAssetsPath);
            //C:/Users/Алексей/Documents/GitHub/MireasLife/Mireas/Menu/Assets/StreamingAssets
            string path = $"{Application.streamingAssetsPath}/{sprites[currentSpriteIndex]}.jpeg";
            Debug.Log(File.Exists(path));
            if (File.Exists(path))
            {
                Texture2D texture = Resources.Load<Texture2D>(sprites[currentSpriteIndex]);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                spriteRenderer.sprite = sprite;
            }
        }
    }
}
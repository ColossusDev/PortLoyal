using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTile : MonoBehaviour
{
    public int[,] islandMap = null;
    public int x;
    public int y;

    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite UpRight;
    [SerializeField] Sprite UpLeft;
    [SerializeField] Sprite DownRight;
    [SerializeField] Sprite DownLeft;

    [SerializeField] Sprite LeftRightUp;
    [SerializeField] Sprite LeftRightDown;
    [SerializeField] Sprite UpDownLeft;
    [SerializeField] Sprite UpDownRight;

    [SerializeField] Sprite Left;
    [SerializeField] Sprite Up;
    [SerializeField] Sprite Right;
    [SerializeField] Sprite Down;

    [SerializeField] Sprite LeftRight;
    [SerializeField] Sprite UpDown;

    [SerializeField] Sprite LeftUpRightDown;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        //links unten
        if (islandMap[x, y - 1] == 1)
        {
            if (islandMap[x - 1, y] == 1)
            {
                spriteRenderer.sprite = DownLeft;
            }
        }

        //rechts unten
        if (islandMap[x + 1, y] == 1)
        {
            if (islandMap[x, y - 1] == 1)
            {
                spriteRenderer.sprite = DownRight;
            }
        }

        //rechts oben
        if (islandMap[x + 1, y] == 1)
        {
            if (islandMap[x, y + 1] == 1)
            {
                spriteRenderer.sprite = UpRight;
            }
        }

        //links oben
        if (islandMap[x - 1, y] == 1)
        {
            if (islandMap[x , y + 1] == 1)
            {
                spriteRenderer.sprite = UpLeft;
            }
        }

        //links recht oben
        if (islandMap[x+1,y] == 1)
        {
            if (islandMap[x -1, y] == 1)
            {
                if (islandMap[x, y + 1] == 1)
                {
                    spriteRenderer.sprite = LeftRightUp;
                }
            }
        }

        // links rechts unten
        if (islandMap[x + 1, y] == 1)
        {
            if (islandMap[x - 1, y] == 1)
            {
                if (islandMap[x, y - 1] == 1)
                {
                    spriteRenderer.sprite = LeftRightDown;
                }
            }
        }

        // unten oben rechts
        if (islandMap[x, y + 1] == 1)
        {
            if (islandMap[x, y - 1] == 1)
            {
                if (islandMap[x + 1, y] == 1)
                {
                    spriteRenderer.sprite = UpDownRight;
                }
            }
        }

        // unten oben links
        if (islandMap[x, y + 1] == 1)
        {
            if (islandMap[x, y - 1] == 1)
            {
                if (islandMap[x - 1, y] == 1)
                {
                    spriteRenderer.sprite = UpDownLeft;
                }
            }
        }

        // alle seiten
        if (islandMap[x, y + 1] == 1)
        {
            if (islandMap[x, y - 1] == 1)
            {
                if (islandMap[x + 1, y] == 1)
                {
                    if (islandMap[x - 1, y] == 1)
                    {
                        spriteRenderer.sprite = LeftUpRightDown;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

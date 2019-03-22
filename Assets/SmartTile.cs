using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartTile : MonoBehaviour
{
    public int[,] islandMap = null;
    public int x;
    public int y;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

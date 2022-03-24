using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,
    Normal,
    Wall,
    Lock,
    BlakHole,
}

public class Tile : MonoBehaviour
{
    [SerializeField] private TileType tileType;
    [SerializeField] private Vector2 mapIndex;
    public bool isBlank = true;
    private SpriteRenderer spriteRenderer;
    private Clicker clicker;
    private bool colLock = false;
    public bool onCube = false;
    public bool grapOnCube = false;

    public PlayerCubeCtrl onCubeObject;

    private void TileInit()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        clicker = transform.GetComponentInChildren<Clicker>();
        clicker.clickAction = OnTileClick;
        TileTypeTest();
    }

    private void Awake()
    {
        TileInit();
    }

    void Start()
    {
    }

    void Update()
    {   
    }

    public void SetOnCube(PlayerCubeCtrl cube)
    {
        if (cube != null)
        {
            onCube = true;
            onCubeObject = cube;
        }
        else
        {
           
            onCubeObject = null;
            onCube = false;
            isBlank = true;
        }
    }
    public bool CompareOnCubeType(CubeType type)
    {
        if (onCubeObject)
            return onCubeObject.cubeType == type;
        return  false;
    }

    public void SetIndex(int x, int y)
    {
        mapIndex.x = x;
        mapIndex.y = y;
    }

    public void SetTileType(TileType type)
    {
        tileType = type;
        TileTypeTest();
    }

    public TileType GetTileType()
    {
        return tileType;
    }

    public void OnTileClick()
    {
        switch (tileType)
        {
            case TileType.Lock:
                if (!onCube)
                    ChangeColLock();
                break;
            default:
                break;
        }
    }

    public void ChangeColLock()
    {
        colLock = !colLock;

        if (colLock == true)
        {
            isBlank = true;
            spriteRenderer.sprite = Resources.Load<Sprite>("Image/LockTile_off");
        }
        else if (colLock == false)
        {
            isBlank = false; 
            spriteRenderer.sprite = Resources.Load<Sprite>("Image/LockTile_on");
        }
    }

    public void TileTypeTest()
    {
        switch (tileType)
        {
            case TileType.None:
                isBlank = false;
                spriteRenderer.enabled = false;
                break;
            case TileType.Normal:
                isBlank = true;
                spriteRenderer.sprite = Resources.Load<Sprite>("Image/Tile");
                break;
            case TileType.Wall:
                isBlank = false;
                spriteRenderer.sprite = null;
                break;
            case TileType.Lock:
                isBlank = false;
                spriteRenderer.sprite = Resources.Load<Sprite>("Image/LockTile_on");
                break;
            case TileType.BlakHole:
                isBlank = true;
                grapOnCube = true;
                spriteRenderer.sprite = Resources.Load<Sprite>("Image/blackHoleTile");
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingVector
{
    public Vector2 returnIndex;
    public Vector2 returnPos;
    public bool doThisDestroy = false;
    public MoveingVector(Vector2 returnIndex, Vector2 returnPos)
    {
        this.returnIndex = returnIndex;
        this.returnPos = returnPos;
    }
}

public class MapManager : MonoBehaviour
{
    [SerializeField] public static Tile[][] tiles;


    private void InitMap()
    {
        //GameObject line = GameObject.Find("TileMap");

        GameObject[] lines = GameObject.FindGameObjectsWithTag("MAP_LINE");

        //tiles = new Tile[lines.Length][];

        //for (int i = 0; i < lines.Length; i++)
        //{
        //    tiles[i] = new Tile[lines.Length];
        //    for (int ii = 0; ii < tiles[i].Length; ii++)
        //    {
        //        tiles[i][ii] = lines[i].transform.Find("Tile_" + ii).GetComponent<Tile>();
        //        tiles[i][ii].SetIndex(ii, i);
        //    }
        //}
        tiles = new Tile[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            GameObject line = GameObject.Find("Tile_Line_" + i);
            tiles[i] = new Tile[lines.Length];
            for(int ii = 0; ii < lines.Length; ii++)
            {
                tiles[i][ii] = line.transform.Find("Tile_" + ii).GetComponent<Tile>();
            }
        }


        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<PlayerCubeCtrl>().IndexTest();
        }
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("BLOCK");

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].GetComponent<PlayerCubeCtrl>().IndexTest();
        }
    }

    private void Awake()
    {
        InitMap();
    }
    void Start()
    {

    }

    public static void SetOnCube(Vector2 index, PlayerCubeCtrl cube)
    {
        tiles[(int)index.y][(int)index.x].SetOnCube(cube);
    }
    public static void CubeMove(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                for (int i = 0; i < tiles.Length; i++)
                {
                    for (int ii = 0; ii < tiles.Length; ii++)
                    {
                        tiles[i][ii].onCubeObject?.MoveCube(direction);
                    }
                }
                break;
            case MoveDirection.Down:
                for(int i = tiles.Length - 1; i >= 0 ; i--)
                {
                    for(int ii = 0; ii <tiles.Length; ii++)
                    {
                        tiles[i][ii].onCubeObject?.MoveCube(direction);
                    }
                }
                break;
            case MoveDirection.Left:
                for (int i = 0; i < tiles.Length; i++)
                {
                    for (int ii = 0; ii < tiles.Length; ii++)
                    {
                        tiles[i][ii].onCubeObject?.MoveCube(direction);
                    }
                }
                break;
            case MoveDirection.Right:
                for (int i = 0; i < tiles.Length; i++)
                {
                    for (int ii = tiles.Length - 1; ii >= 0; ii--)
                    {
                        tiles[i][ii].onCubeObject?.MoveCube(direction);
                    }
                }
                break;
        }
    }
    public static MoveingVector GetCubeMovePos(int x, int y, MoveDirection direction, CubeType isPlayer)
    {
        Vector2 returnMovePos = new Vector2(x, y);
        Vector2 addMovePos = new Vector2(0, 0);
        MoveingVector moveingVector = new MoveingVector(returnMovePos,
            tiles[(int)returnMovePos.y][(int)returnMovePos.x].transform.position);
        switch (direction)
        {
            case MoveDirection.Up:
                addMovePos.y = -1;
                break;
            case MoveDirection.Down:
                addMovePos.y = 1;
                break;
            case MoveDirection.Right:
                addMovePos.x = 1;
                break;
            case MoveDirection.Left:
                addMovePos.x = -1;
                break;
            default:
                break;
        }

        for(int i = 0; i < tiles.Length; i++)
        {
            if (returnMovePos.y < tiles.Length && returnMovePos.y >= 0 && 
                returnMovePos.x < tiles[0].Length && returnMovePos.x >= 0 &&
                tiles[(int)returnMovePos.y][(int)returnMovePos.x].isBlank &&
                (!tiles[(int)returnMovePos.y][(int)returnMovePos.x].onCube || 
                (isPlayer == CubeType.None  && tiles[(int)returnMovePos.y][(int)returnMovePos.x].CompareOnCubeType(CubeType.None))))
            {
                if(tiles[(int)returnMovePos.y][(int)returnMovePos.x].GetTileType() == TileType.BlakHole &&
                    tiles[(int)returnMovePos.y][(int)returnMovePos.x].grapOnCube == true)
                {
                    tiles[(int)returnMovePos.y][(int)returnMovePos.x].grapOnCube = false;
                    tiles[(int)returnMovePos.y][(int)returnMovePos.x].onCube = false;
                    moveingVector.doThisDestroy = true;

                    break;
                }
                returnMovePos += addMovePos;
            }
            else
            {
                returnMovePos -= addMovePos;
                break;
            }
        }

        moveingVector.returnIndex = returnMovePos;
        moveingVector.returnPos = tiles[(int)returnMovePos.y][(int)returnMovePos.x].transform.position;
        return moveingVector;
    }
}

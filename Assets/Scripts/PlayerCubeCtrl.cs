using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    None,
    Right, //오른쪽
    Left,  //왼쪽
    Up,    //위
    Down   //아래
}

public enum CubeType
{
    None, //목표 큐브
    Block,//벽 큐브
}

public class PlayerCubeCtrl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public CubeType cubeType;
    [SerializeField] private Vector2 playerIndex;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private bool canMove = true;
    private bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    private void Awake()
    {
        SetUpCube();
    }

    void Start()
    {

    }

    public void IndexTest()
    {
        transform.position = MapManager.tiles[(int)playerIndex.y][(int)playerIndex.x].transform.position + new Vector3(0, 0, -1);
        MapManager.tiles[(int)playerIndex.y][(int)playerIndex.x].SetOnCube(this);
    }
    public void SetUpCube()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        anim = transform.GetComponentInChildren<Animator>();
        switch (cubeType)
        {
            case CubeType.None:
                transform.tag = "Player";
                spriteRenderer.sprite = Resources.Load<Sprite>("Image/player");
                break;
            case CubeType.Block:
                transform.tag = "BLOCK";
                spriteRenderer.sprite = Resources.Load<Sprite>("Image/just");
                break;
            default:
                break;
        }
    }

    public void MoveCube(MoveDirection direction)
    {
        if (!CanMove)
            return;
        Vector3 dirvec;

        switch (direction)
        {
            case MoveDirection.Right:
                dirvec = Vector2.right;
                break;
            case MoveDirection.Left:
                dirvec = Vector2.left;
                break;
            case MoveDirection.Up:
                dirvec = Vector2.up;
                break;
            case MoveDirection.Down:
                dirvec = Vector2.down;
                break;
            default:
                dirvec = Vector2.zero;
                break;
        }
        MapManager.SetOnCube(playerIndex, null);
        MoveingVector moveVector = MapManager.GetCubeMovePos((int)playerIndex.x, (int)playerIndex.y, direction, cubeType);
        if (moveVector.returnIndex == playerIndex)
        {
            if (!moveVector.doThisDestroy)
                MapManager.SetOnCube(playerIndex, this);
            return;
        }
        playerIndex = moveVector.returnIndex;
        if (!moveVector.doThisDestroy)
        {
            MapManager.SetOnCube(playerIndex, this);
        }
        else
        {
            MapManager.tiles[(int)playerIndex.y][(int)playerIndex.x].SetTileType(TileType.Normal);
            DestroyCube();
        }
        StartCoroutine(CubeMoveToPosition(moveVector.returnPos, dirvec));

    }

    private IEnumerator CubeMoveToPosition(Vector3 targetPos, Vector3 direction)
    {
        PlayerManager.doNotMoveCubeCount += 1;
        canMove = false;
        while (true)
        {
            transform.position += direction * Time.deltaTime * moveSpeed;

            if (Mathf.Abs((transform.position - (targetPos + new Vector3(0, 0, transform.position.z))).sqrMagnitude) <= 0.3f)
            {
                transform.position = targetPos;
                GameManager.soundManager.PlaySound(SoundType.blockHit);
                break;
            }
            yield return null;
        }

        PlayerManager.doNotMoveCubeCount -= 1;

        CanMove = true;
    }

    public void RemoveCube()
    {
        MapManager.tiles[(int)playerIndex.y][(int)playerIndex.x].SetOnCube(null);
        Destroy(gameObject);
    }

    public void DestroyCube()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Destroy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && cubeType == CubeType.None)
        {
            GameManager.GameClear();
        }
    }
}

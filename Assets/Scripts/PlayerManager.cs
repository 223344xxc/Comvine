using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
     [SerializeField] public static int doNotMoveCubeCount;

    private Vector2 InputPos;
    public float chackDistance;

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.isGameClear)
            return;
        PlayerInput();
    }

    private void PlayerInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            InputPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 dif = (Vector2)Input.mousePosition - InputPos;
            if(dif.magnitude >= chackDistance)
            {
                if (dif.x > 0 && Mathf.Abs(dif.y) < dif.x)
                {
                    MoveCube(MoveDirection.Right);
                }
                else if (dif.x < 0 && Mathf.Abs(dif.y) < Mathf.Abs(dif.x))
                {
                    MoveCube(MoveDirection.Left);
                }
                else if (dif.y > 0 && Mathf.Abs(dif.x) < dif.y)
                {
                    MoveCube(MoveDirection.Up);
                }
                else if (dif.y < 0 && Mathf.Abs(dif.x) < Mathf.Abs(dif.y))
                {
                    MoveCube(MoveDirection.Down);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.ReturnHome();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCube(MoveDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveCube(MoveDirection.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCube(MoveDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCube(MoveDirection.Right);
        }
#endif


        if (Input.GetMouseButtonUp(0))
        {
            int layer = 1 << LayerMask.NameToLayer("ClickLayer");
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero,10000, layer);
            if (hit)
            {
                hit.transform.GetComponent<Clicker>().OnClick();
            }
        }
    }

    private void MoveCube(MoveDirection direction)
    {
        if (doNotMoveCubeCount > 0)
            return;
        MapManager.CubeMove(direction);
        GameManager.soundManager.PlaySound(SoundType.swipe);
    }
}

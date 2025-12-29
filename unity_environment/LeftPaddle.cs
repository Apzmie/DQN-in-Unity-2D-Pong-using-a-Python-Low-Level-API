using UnityEngine;
using UnityEngine.InputSystem;

public class LeftPaddle : MonoBehaviour
{
    public float speed;
    public float limitY;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private float moveDir;
    private float lastTouchY;
    private bool isTouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = rb.position;
    }

    private void ResetPaddle()
    {
        rb.position = startPos;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnGoalLeft()
    {
        ResetPaddle();
    }

    public void OnGoalRight()
    {
        ResetPaddle();
    }

    private void Update()
    {
        moveDir = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.upArrowKey.isPressed)
                moveDir = 1f;
            else if (Keyboard.current.downArrowKey.isPressed)
                moveDir = -1f;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                isTouching = true;
                lastTouchY = touch.position.y;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Moved && isTouching)
            {
                float deltaY = touch.position.y - lastTouchY;

                if (Mathf.Abs(deltaY) > 1f)
                    moveDir = Mathf.Sign(deltaY);

                lastTouchY = touch.position.y;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended ||
                     touch.phase == UnityEngine.TouchPhase.Canceled)
            {
                isTouching = false;
                moveDir = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos.y += moveDir * speed * Time.fixedDeltaTime;
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        rb.MovePosition(pos);
    }
}

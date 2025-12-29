using UnityEngine;

public class Ball : MonoBehaviour
{
    public PaddleAgent paddleAgent;
    public RunModel runModel;
    public LeftPaddle leftPaddle;
    public float speed;
    public float resetSpeed;
    public float ballResetX;
    public float ballResetRangeY;
    public float yThreshold;
    public float yHoldTime;
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private float yHoldTimer = 0f;
    private bool isBallReset = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.position.y) >= yThreshold)
        {
            yHoldTimer += Time.fixedDeltaTime;
            if (yHoldTimer >= yHoldTime)
                ResetBall();
        }
        else
        {
            yHoldTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (isBallReset)
            {
                isBallReset = false;
            }
            direction.x = -direction.x;
            
            Collider2D ballCollider = GetComponent<Collider2D>();
            Collider2D paddleCollider = collision.gameObject.GetComponent<Collider2D>();

	        float ballY = ballCollider.bounds.center.y;
            float paddleY = paddleCollider.bounds.center.y;
            float paddleHeight = paddleCollider.bounds.size.y;
            float relativeY = (ballY - paddleY) / (paddleHeight / 2);
            float maxBounceAngle = 45f * Mathf.Deg2Rad;
            
            direction.y = Mathf.Sin(relativeY * maxBounceAngle);
            direction = direction.normalized;

            float currentSpeed = isBallReset ? resetSpeed : speed;
            rb.linearVelocity = direction * currentSpeed;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
            float currentSpeed = isBallReset ? resetSpeed : speed;
            rb.linearVelocity = direction * currentSpeed;
        }
        else if (collision.gameObject.CompareTag("LeftWall"))
        {
            direction.x = -direction.x;
            float currentSpeed = isBallReset ? resetSpeed : speed;
            rb.linearVelocity = direction * currentSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoalLeft"))
        {
            ResetBall();
            if (paddleAgent != null)
            {
                paddleAgent.OnGoalLeft();
            }
            if (runModel != null)
            {
                runModel.OnGoalLeft();
            }
            if (leftPaddle != null)
            {
                leftPaddle.OnGoalLeft();
            }
        }
        else if (collision.CompareTag("GoalRight"))
        {
            ResetBall();
            if (paddleAgent != null)
            {
                paddleAgent.OnGoalRight();
            }
            if (runModel != null)
            {
                runModel.OnGoalRight();
            }
            if (leftPaddle != null)
            {
                leftPaddle.OnGoalRight();
            }
        }
    }

    public void ResetBall()
    {
        float randomY = Random.Range(-ballResetRangeY, ballResetRangeY);
        rb.position = new Vector2(ballResetX, randomY);

        float xDir = -1f;
        float yDir = Random.Range(-1f, 1f);
        direction = new Vector2(xDir, yDir).normalized;
        rb.linearVelocity = Vector2.zero;

        isBallReset = true;
        rb.linearVelocity = direction * resetSpeed;
    }
}

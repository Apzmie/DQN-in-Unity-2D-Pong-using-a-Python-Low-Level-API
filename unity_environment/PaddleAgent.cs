using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgent : Agent
{
    public Rigidbody2D ball;
    public float speed;
    public float limitY;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private float moveDir;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = rb.position;
    }

    public override void OnEpisodeBegin()
    {
        rb.position = startPos;
        rb.linearVelocity = Vector2.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.position.y);
        sensor.AddObservation(ball.position.x);
        sensor.AddObservation(ball.position.y);
        sensor.AddObservation(ball.linearVelocity.x);
        sensor.AddObservation(ball.linearVelocity.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];

        if (action == 0)
        {
            moveDir = 1f;
        }
        else if (action == 2)
        {
            moveDir = -1f;
        }
        else
        {
            moveDir = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float paddleHeight = GetComponent<Collider2D>().bounds.size.y;
            float hitPointY = collision.GetContact(0).point.y;
            float paddleCenterY = rb.position.y;
            float offset = Mathf.Abs(hitPointY - paddleCenterY) / (paddleHeight / 2f);
            offset = Mathf.Clamp01(offset);
            AddReward(offset);
        }
    }

    public void OnGoalLeft()
    {
        rb.position = startPos;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnGoalRight()
    {
        AddReward(-1f);
        EndEpisode();
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos.y += moveDir * speed * Time.fixedDeltaTime;
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        rb.MovePosition(pos);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;

        if (Keyboard.current.upArrowKey.isPressed)
        {
            actions[0] = 0;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            actions[0] = 2;
        }
        else
        {
            actions[0] = 1;
        }
    }
}

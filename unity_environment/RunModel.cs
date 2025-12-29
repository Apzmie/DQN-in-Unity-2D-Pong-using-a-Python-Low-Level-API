using UnityEngine;
using Unity.InferenceEngine;
using System.Threading.Tasks;

public class RunModel : MonoBehaviour
{
    public ModelAsset modelAsset;
    public Rigidbody2D ball;
    public float speed;
    public float limitY;

    private Model runtimeModel;
    private Worker worker;
    private Tensor<float> inputTensor;
    private Tensor<float> cpuCopyTensor;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private float moveDir;
    private bool inferencing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = new Worker(runtimeModel, BackendType.CPU);

        float[] dummyObservation = new float[5] { 0, 0, 0, 0, 0 };
        TensorShape shape = new TensorShape(1, 5);
        using (Tensor<float> dummyInput = new Tensor<float>(shape, dummyObservation))
        {
            worker.Schedule(dummyInput);
            using (Tensor<float> dummyOutput = worker.PeekOutput() as Tensor<float>) { }
        }

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

    private void FixedUpdate()
    {
        if (!inferencing)
        {
            _ = InferenceAsync();
        }
    }

    private async Task InferenceAsync()
    {
        inferencing = true;

        float[] observation = new float[5]
        {
            rb.position.y,
            ball.position.x,
            ball.position.y,
            ball.linearVelocity.x,
            ball.linearVelocity.y
        };

        TensorShape shape = new TensorShape(1, 5);
        inputTensor?.Dispose();
        inputTensor = new Tensor<float>(shape, observation);
        worker.Schedule(inputTensor);
        using (var outputTensor = worker.PeekOutput() as Tensor<float>)
        {
            cpuCopyTensor = await outputTensor.ReadbackAndCloneAsync();
        }

        int bestAction = 0;
        float maxQ = cpuCopyTensor[0];
        for(int i=1; i<3; i++)
        {
            if(cpuCopyTensor[i] > maxQ)
            {
                maxQ = cpuCopyTensor[i];
                bestAction = i;
            }
        }

        cpuCopyTensor.Dispose();

        switch(bestAction)
        {
            case 0:
                moveDir = 1f;
                break;
            case 1:
                moveDir = 0f;
                break;
            case 2:
                moveDir = -1f;
                break;
        }

        Vector2 pos = rb.position;
        pos.y += moveDir * speed * Time.fixedDeltaTime;
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);
        rb.MovePosition(pos);

        inferencing = false;
    }

    private void OnDestroy()
    {
        inputTensor?.Dispose();
        worker?.Dispose();
    }
}

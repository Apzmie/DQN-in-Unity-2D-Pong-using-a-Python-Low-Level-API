# DQN-in-Unity-2D-Pong-using-a-Python-Low-Level-API
Unity provides a Python low-level API that allows anyone to apply their own reinforcement learning algorithms to objects. This approach enables a deep understanding of reinforcement learning implementation without relying on libraries like Gym or Stable-Baselines3. ML-Agents is used only to observe the environment and retrieve observations into Python.

DQN is used with an MLP, taking the paddle and ball coordinates as neural network inputs. To enhance training, Prioritized replay and Multi-step learning—two techniques from Rainbow DQN—are also applied. This project is implemented in PyTorch, based on the DQN paper [*Playing Atari with Deep Reinforcement Learning*](https://www.cs.toronto.edu/~vmnih/docs/dqn.pdf), [*Human-level control through deep reinforcement
learning*](https://web.stanford.edu/class/psych209/Readings/MnihEtAlHassibis15NatureControlDeepRL.pdf), and the Rainbow DQN paper [*Rainbow: Combining Improvements in Deep Reinforcement Learning*](https://arxiv.org/pdf/1710.02298.pdf).

You can play against the Paddle AI directly in your browser at [https://apzmie.itch.io/](https://apzmie.itch.io/), on both PC and mobile devices.

<img src="images/2d_pong.png" alt="2d_pong" width="40%">


## Environment
### Unity
- Unity Editor: 6000.3.0f1
- ML Agents: 4.0.0
- Inference Engine: 2.2.2

### Python
- Python 3.10.12 (for Training)
- Python 3.12.12 (for ONNX Conversion)

## DQN Diagram
![dqn_diagram](images/dqn_diagram.png)
The agent selects actions using an epsilon-greedy policy. At the beginning of training, epsilon is set to 1.0 to encourage random actions and sufficient exploration. Epsilon is gradually decreased over time using an epsilon decay rate, so that the agent focuses more on exploitation rather than exploration. Epsilon is not reduced to 0 because this would cause the agent to lose opportunities for further improvement.

Q-value represents the expected total reward of taking a specific action in a given state. The target of Q-value is calculated as the sum of immediate reward and the maximum Q-value of the next state. This is because the value of a current action depends on the quality of the resulting future state.

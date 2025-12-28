# DQN-in-Unity-2D-Pong-using-a-Python-Low-Level-API
Unity provides a Python low-level API that allows anyone to apply their own reinforcement learning algorithms to objects. This approach provides a deep understanding of reinforcement learning implementation without relying on libraries like Gym or Stable-Baselines3. ML-Agents is used only to observe the environment and retrieve observations into Python.

DQN is used with an MLP, taking the paddle and ball coordinates as neural network inputs. To enhance training, Prioritized replay and Multi-step learning—two techniques from Rainbow DQN—are also applied. This project is implemented in PyTorch, based on the DQN paper [*Playing Atari with Deep Reinforcement Learning*](https://www.cs.toronto.edu/~vmnih/docs/dqn.pdf), [*Human-level control through deep reinforcement
learning*](https://web.stanford.edu/class/psych209/Readings/MnihEtAlHassibis15NatureControlDeepRL.pdf), and the Rainbow DQN paper [*Rainbow: Combining Improvements in Deep Reinforcement Learning*](https://arxiv.org/pdf/1710.02298.pdf).

You can play against the Paddle AI directly in your browser at [https://apzmie.itch.io/](https://apzmie.itch.io/), on both PC and mobile devices.

## Unity Environment
- Unity Editor: 6000.3.0f1
- ML Agents: 4.0.0
- Inference Engine: 2.2.2

## Python Environment
- Python 3.10.12 (Training)
- Python 3.12.12 (ONNX Conversion)

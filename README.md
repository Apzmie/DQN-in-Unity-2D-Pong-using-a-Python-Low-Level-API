# DQN-in-Unity-2D-Pong-using-a-Python-Low-Level-API
Unity provides a Python low-level API that allows anyone to apply their own reinforcement learning algorithms to objects. This approach provides a deep understanding of reinforcement learning implementation without relying on libraries like Gym or Stable-Baselines3. ML-Agents is used only to observe the environment and retrieve observations in Python.

This is a 2D Pong game using DQN with an MLP, taking the paddle and ball coordinates as neural network inputs. To enhance training, Prioritized Experience Replay and Multi-step Learning—two techniques from Rainbow DQN—are also applied. This project is implemented in PyTorch, based on the DQN paper [*Playing Atari with Deep Reinforcement Learning*](https://www.cs.toronto.edu/~vmnih/docs/dqn.pdf), [*Human-level control through deep reinforcement
learning*](https://web.stanford.edu/class/psych209/Readings/MnihEtAlHassibis15NatureControlDeepRL.pdf), and the Rainbow DQN paper [*Rainbow: Combining Improvements in Deep Reinforcement Learning*](https://arxiv.org/pdf/1710.02298.pdf).


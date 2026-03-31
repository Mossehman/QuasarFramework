using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuasarFramework.Lerp
{
    public class QLerp
    {
        // TODO (Aaron): Maybe cache the root node to avoid un-needed recursion check

        private List<I_QLerpOperation> lerpOperations = new();
        private QLerp nextSequence = null;
        private QLerp prevSequence = null;
        private readonly float delay;

        private bool isPaused = false;

        private event Action OnCompleted;
        private float elapsedTime = 0.0f; // Track the elapsed time since the lerp started
        private int completedOperations = 0; // Keep track of all completed lerp operations, this allows us to determine when our lerp sequence is completed

        public bool toLoop = false;

        public QLerp(float delay = 0.0f, QLerp prevSequence = null)
        {
            this.delay = delay;
            nextSequence = null;
            this.prevSequence = prevSequence;
            toLoop = false;
        }

        public QLerp Start(bool toLoop = false)
        {
            this.toLoop = toLoop;

            elapsedTime = 0.0f;
            completedOperations = 0;

            foreach (var operation in lerpOperations)
            {
                operation.onComplete += HandleLerpComplete;
            }
            QuasarAnimator.Instance.AddLerp(this);
            return this;
        }

        public bool Stop()
        {
            QLerp rootNode = GetRoot();
            return TryRemoveRecursive(rootNode);
        }

        public void Pause()
        {
            QLerp currentNode = TryGetRecursive(GetRoot());
            currentNode.isPaused = true;
        }

        public void Resume()
        {
            QLerp currentNode = TryGetRecursive(GetRoot());
            currentNode.isPaused = false;
        }

        public QLerp Sequence(float delay = 0.0f)
        {
            nextSequence = new QLerp(delay, this);
            return nextSequence;
        }

        public QLerp OnComplete(Action listener)
        {
            OnCompleted += listener;
            return this;
        }

        public void UpdateLerp()
        {
            if (isPaused) return; // Do not lerp if paused

            elapsedTime += Time.deltaTime;
            if (elapsedTime < delay) { return; } // Await delay before executing lerps
            
            if (completedOperations >= lerpOperations.Count)
            {
                HandleNext();
                completedOperations = 0;
                return;
            }

            foreach (var operation in lerpOperations)
            {
                operation.Lerp(elapsedTime - delay);
            }
        }

        public float GetDelay()
        {
            return delay;
        }
        public QLerp GetPrevious() 
        { 
            return prevSequence; 
        }
        public QLerp GetRoot()
        {
            if (prevSequence == null)
                return this;

            return prevSequence.GetRoot();
        }

        public void Reset()
        {
            prevSequence = null;
            nextSequence = null;
            isPaused = false;
            elapsedTime = 0.0f;
            completedOperations = 0;
            

            ClearOperations();
            lerpOperations.Clear();
        }

        private void HandleLerpComplete()
        {
            completedOperations++;
        }

        private void HandleNext()
        {
            ClearOperations();

            if (toLoop && nextSequence == null)
            {
                QuasarAnimator.Instance.ReplaceLerp(this, GetRoot());
            }
            else
            {
                QuasarAnimator.Instance.ReplaceLerp(this, nextSequence);
            }
            OnCompleted?.Invoke();
            OnCompleted = null;
        }

        private QLerp TryGetRecursive(QLerp current)
        {
            if (current == null) { return null; }
            else if (QuasarAnimator.Instance.ContainsLerp(current)) { return current; }

            return TryGetRecursive(current.nextSequence);
        }

        private bool TryRemoveRecursive(QLerp current)
        {
            if (current == null)
            {
                return false;
            }
            if (QuasarAnimator.Instance.RemoveLerp(current))
            {
                return true;
            }


            
            return TryRemoveRecursive(current.nextSequence);
        }

        private void ClearOperations()
        {
            foreach (var operation in lerpOperations)
            {
                operation.onComplete -= HandleLerpComplete;

                if (!toLoop)
                {
                    operation.Return();
                }
                else
                {
                    operation.Reset();
                }
            }
        }

        public QLerp Float(float from, float to, float duration, Action<float> onValue = null, Func<float, float> ease = null, float power = 1.0f)
        {
            var op = QLerpOperationPool<float>.Get(() => new QLerp_FloatOperation());
            op.Initialise(from, to, duration, onValue, ease, power);
            lerpOperations.Add(op);
            return this;
        }
        public QLerp Vector(Vector3 from, Vector3 to, float duration, Action<Vector3> onValue = null, Func<float, float> ease = null, float power = 1.0f)
        {
            var op = QLerpOperationPool<Vector3>.Get(() => new QLerp_VectorOperation());
            op.Initialise(from, to, duration, onValue, ease, power);
            lerpOperations.Add(op);
            return this;
        }
        public QLerp Quaternion(Quaternion from, Quaternion to, float duration, Action<Quaternion> onValue = null, Func<float, float> ease = null, float power = 1.0f)
        {
            var op = QLerpOperationPool<Quaternion>.Get(() => new QLerp_QuaternionOperation());
            op.Initialise(from, to, duration, onValue, ease, power);
            lerpOperations.Add(op);
            return this;
        }
        public QLerp Color(Color from, Color to, float duration, Action<Color> onValue = null, Func<float, float> ease = null, float power = 1.0f)
        {
            var op = QLerpOperationPool<Color>.Get(() => new QLerp_ColorOperation());
            op.Initialise(from, to, duration, onValue, ease, power);
            lerpOperations.Add(op);
            return this;
        }
    }
}
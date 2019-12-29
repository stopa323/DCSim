using System;
using System.Collections;
using UnityEngine;

public class DurationTimer
{
    private class WaitForDuration : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                return !predicate();
            }
        }

        private Func<bool> predicate;
        
        public WaitForDuration(Func<bool> predicate)
        {
            this.predicate = predicate;
        }
    }

    private float durationTime;
    private Action onFinishCallback;
    private Action onUpdateCallback;

    public float ElapsedTime { get; private set; }

    public DurationTimer(float durationTime, Action onUpdateCallback,
        Action onFinishCallback)
    {
        this.durationTime = durationTime;
        this.onFinishCallback = onFinishCallback;
        this.onUpdateCallback = onUpdateCallback;
    }

    public IEnumerator Start()
    {
        ElapsedTime = 0f;
        yield return new WaitForDuration(isFinished);

        onFinishCallback?.Invoke();
    }

    private bool isFinished()
    {
        onUpdateCallback?.Invoke();

        ElapsedTime = Mathf.Clamp(ElapsedTime + Time.deltaTime, 0, durationTime);
        return ElapsedTime >= durationTime;
    }
}

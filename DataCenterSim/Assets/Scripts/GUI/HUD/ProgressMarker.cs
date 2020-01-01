using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMarker : StaticMarker
{
    [SerializeField] private Image progressImage;

    private DurationTimer timer;
    private float duration;

    public void Populate(Transform target, float duration, Action onFinishCallback)
    {
        Populate(target);

        this.duration = duration;
        timer = new DurationTimer(duration, onUpdate, () => {
            onFinishCallback?.Invoke();
            Destroy(gameObject);
        });
    }

    public void Trigger()
    {
        StartCoroutine(timer.Start());
    }

    private void Awake()
    {
        progressImage.fillAmount = 0;
    }

    protected void onUpdate()
    {
        progressImage.fillAmount = timer.ElapsedTime / duration;
    }
}

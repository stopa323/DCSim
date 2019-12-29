using Game.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("GUI")]
    [SerializeField] private Image progressImage;

    private Transform target;
    private DurationTimer timer;
    private float duration;

    public void Populate(Transform target, float duration, Action onFinishCallback)
    {
        this.target = target;
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

    void LateUpdate()
    {
        if (null == progressImage) return;

        var camera = GameStateManager.Instance.MainCamera;
        transform.position = camera.WorldToScreenPoint(target.position);
    }

    protected void onUpdate()
    {
        progressImage.fillAmount = timer.ElapsedTime / duration;
    }
}

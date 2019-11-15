using UnityEngine;
using UnityEngine.UI;

public class OrderDeliveryProgress : MonoBehaviour
{
    public Image progressImage;
    public Text timeText;

    private bool started = false;
    private float remainingTime = 0;
    private float totalTime = 0;

    public void StartTimer(float elapsedTime)
    {
        started = true;
        totalTime = elapsedTime;
        remainingTime = elapsedTime;
    }

    private void LateUpdate()
    {
        if (!started) return;

        remainingTime = Mathf.Clamp(remainingTime - Time.deltaTime, 0, totalTime);
        timeText.text = remainingTime.ToString() + "s";  // TODO: use formatter

        progressImage.fillAmount = (totalTime - remainingTime) / totalTime;

        if (0 == remainingTime) { Destroy(gameObject); }
    }
}

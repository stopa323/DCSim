using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : Button
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip errorSound;
    public VoidEvent OnClickVoidEvent;

    public virtual void OnCursorEnter()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public virtual void OnCursorClick()
    {
        audioSource.PlayOneShot(clickSound);
        if (null != OnClickVoidEvent) OnClickVoidEvent.Raise();
    }

}

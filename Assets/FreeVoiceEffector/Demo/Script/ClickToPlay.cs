using FreeVoiceEffector;
using UnityEngine;
namespace FreeVoiceEffector
{
    public class ClickToPlay : MonoBehaviour
{
    FreeVoiceEffectController controller;
    private void Start()
    {
        GetComponent<FreeVoiceEffectController>();
    }
    void OnMouseDown()
    {
        gameObject.transform.localScale = Vector3.one;
        var audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
            else audio.Play();
        }
    }
    void OnMouseOver()
    {
        gameObject.transform.localScale = Vector3.one*1.2f;
    }
    private void OnMouseExit()
    {
        gameObject.transform.localScale = Vector3.one;
    }
}
}

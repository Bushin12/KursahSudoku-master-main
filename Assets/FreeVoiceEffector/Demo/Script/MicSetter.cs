using FreeVoiceEffector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FreeVoiceEffector
{


public class MicSetter : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {   var Mic = RealTimeMic.Instance;
        toggle = GetComponent<Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(Mic.SetMic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
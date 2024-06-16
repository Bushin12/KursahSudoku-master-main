using FreeVoiceEffector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace FreeVoiceEffector
{
    [RequireComponent(typeof(AudioSource))]

    public class RealTimeMic : MonoBehaviour
    {
        private static RealTimeMic instance;

        public static RealTimeMic Instance => instance;
        private AudioSource mic;
        public bool useMic = false;
        private string selectedMicName = null;
        string[] micDeviceNames;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

        }
        private void Start()
        {
            mic = GetComponent<AudioSource>();

            micDeviceNames = FreeVoiceEffector.Instance.micDeviceNames;

            if (micDeviceNames.Length > 0)
            {
                int micNumber = FreeVoiceEffector.Instance.setMicNumber;
                selectedMicName = micDeviceNames[micNumber];
                Debug.Log("Selected microphone: " + selectedMicName);
            }
            else
            {
                Debug.LogWarning("No microphone found.");
            }

            SetMic(useMic);
        }
        public void ChangeMic()
        {
            useMic = false;
            int micNumber = FreeVoiceEffector.Instance.setMicNumber;
            selectedMicName = micDeviceNames[micNumber];
            useMic = true;
            SetMic(useMic);

        }
        public void SetMic(bool toggle)
        {
            useMic = toggle;
            if (selectedMicName == null)
            {
                Debug.LogWarning("No microphone selected.");
                return;
            }

            if (!toggle)
            {
                if (mic != null)
                {
                    mic.clip = null;
                }
                return;
            }
            else
            {
                if (selectedMicName != "")
                {
                    StartCoroutine(RealTimeMicPlay());
                }
            }
        }
        IEnumerator RealTimeMicPlay()
        {
            while (useMic)
            {
                AudioClip clip = Microphone.Start(selectedMicName, true, 1, 44100);
                yield return new WaitForSeconds(1); // 1초 기다립니다.
                mic.clip = clip;
                mic.Play();
            }
            mic.clip = null;
            Microphone.End(selectedMicName);
        }
        private void OnDestroy()
        {
            if (selectedMicName != null)
            {
                Microphone.End(selectedMicName);
            }
        }
    }
}
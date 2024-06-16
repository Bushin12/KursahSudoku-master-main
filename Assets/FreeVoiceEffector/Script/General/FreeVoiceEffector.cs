using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace FreeVoiceEffector
{
    public class FreeVoiceEffector : MonoBehaviour
    {
        private static FreeVoiceEffector instance;

        public static FreeVoiceEffector Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("FreeVoiceEffectManager");
                    instance = obj.AddComponent<FreeVoiceEffector>();
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }
        public AudioMixer effectMixer;
        public List<AudioMixerGroup> directEffectBusContainer = new List<AudioMixerGroup>();
        public List<AudioMixerGroup> spaceEffectBusContainer = new List<AudioMixerGroup>();
        public List<AudioMixerGroup> etcEffectBusContainer = new List<AudioMixerGroup>();

        [Header("Graph Handle")]

        public AnimationCurve inSineCurve;
        public AnimationCurve squareCurve;
        public AnimationCurve cubicCurve;
        public AnimationCurve inCubicCurve;
        public AnimationCurve custumeCurve;
        [Header("Microphone List")]
        public string[] micDeviceNames;
        public int setMicNumber = 0;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            var directEffectBusGroup = effectMixer.FindMatchingGroups("DirectEffect");
            var spaceEffectBusGroup = effectMixer.FindMatchingGroups("SpaceEffect");
            var etcEffectBusGroup = effectMixer.FindMatchingGroups("ETC");

            for (int i = 1; i < directEffectBusGroup.Length; i++)
            {
                directEffectBusContainer.Add(directEffectBusGroup[i]);
            }
            for (int i = 1; i < spaceEffectBusGroup.Length; i++)
            {
                spaceEffectBusContainer.Add(spaceEffectBusGroup[i]);
            }
            for (int i = 1; i < etcEffectBusGroup.Length; i++)
            {
                etcEffectBusContainer.Add(etcEffectBusGroup[i]);
            }
            micDeviceNames = Microphone.devices;
            setMicNumber = PlayerPrefs.GetInt("UserMicSetting", 0);
        }
        private void Start()
        {
        }
        public void SetMicNumber(int num)
        {
            setMicNumber = num;
            PlayerPrefs.SetInt("UserMicSetting", num);
            RealTimeMic.Instance.ChangeMic();
        }
    }
}


using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace FreeVoiceEffector
{
    public class FreeVoiceEffectController : MonoBehaviour
    {
        //event change delegate
        public delegate void EffectPresetChangedHandler();
        public event EffectPresetChangedHandler EffectPresetChanged;

        //core
        public EffectPreset effectPreset;
        private EffectPreset oldPreset;
        private float lastValue;
        [Range(0, 1)]
        public float value;
        public bool isReverse = false;
        private bool isChangeReverse = false;
        [SerializeField] private AudioClip originClip;

        [Header("Debug & Test")]
        [SerializeField] Slider valueChanger;


        [Header("Audio Settings")]
        public AudioSource audioSource;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AnimationCurve inSineCurve;
        [SerializeField] private AnimationCurve squareCurve;
        [SerializeField] private AnimationCurve cubicCurve;
        [SerializeField] private AnimationCurve inCubicCurve;
        [SerializeField] private AnimationCurve custumeCurve;

        // change value by func
        [Header("SpetialAudio")]
        [SerializeField] private AudioSource invertedAudio;

        //need Audio sourceChange
        public bool needClipChange = false;

        //value change type
        public static float EvaluateGraphValueLinear(float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            return newMin + (value - oldMin) * (newMax - newMin) / (oldMax - oldMin);
        }
        public float EvaluateGraphValueWithEase(float value, float oldMin, float oldMax, float newMin, float newMax, AnimationCurve curve)
        {
            float normalizedValue = (value - oldMin) / (oldMax - oldMin);

            float easedValue = curve.Evaluate(normalizedValue);

            return newMin + easedValue * (newMax - newMin);
        }

        private void Start()
        {
            //if you want to using slider, unlock this and update
            //value = valueChanger.value;

            lastValue = value;
            oldPreset = effectPreset;
            mixer = FreeVoiceEffector.Instance.effectMixer;
            SetCurve();
            if (GetComponent<AudioSource>() == null)
                audioSource = gameObject.AddComponent<AudioSource>();
            else audioSource = GetComponent<AudioSource>();
            originClip = audioSource.clip;
            EffectPresetChanged += SetOriginClip;
            switch (effectPreset)
            {
                case EffectPreset.None:
                    audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];
                    audioSource.pitch = 1;
                    audioSource.volume = 1;
                    value = 0;
                    break;
                case EffectPreset.Cartoon:
                    //value = 0.5f;

                    audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Cartoon")[0];
                    mixer.SetFloat("CartoonVoiceMasterPitch", EvaluateGraphValueWithEase(value, 0, 1, 1, 6, inSineCurve));
                    mixer.SetFloat("CartoonChannelPitch", EvaluateGraphValueWithEase(value, 0, 1, 1.65f, 2, inSineCurve));
                    float fixedValue = EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve) > 0 ? 0 : EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve);
                    mixer.SetFloat("CartoonChorusSend", fixedValue);
                    break;
                case EffectPreset.Monster:
                    //value = 0.5f;

                    audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Monster")[0];
                    mixer.SetFloat("MonsterVoiceMasterPitch", EvaluateGraphValueWithEase(value, 0, 1, 0.8f, 0.2f, cubicCurve));

                    mixer.SetFloat("MonsterVoiceEchoDelay", EvaluateGraphValueWithEase(value, 0, 1, 30, 200, cubicCurve));
                    mixer.SetFloat("MonsterVoiceEchoDirect", EvaluateGraphValueWithEase(value, 0, 1, 1, 0.7f, cubicCurve));
                    mixer.SetFloat("MonsterVoiceEchoMix", EvaluateGraphValueWithEase(value, 0, 1, 0.5f, 0.8f, cubicCurve));

                    mixer.SetFloat("MonsterVoiceReverbSend", EvaluateGraphValueWithEase(value, 0, 1, -80, 0, cubicCurve));

                    mixer.SetFloat("MonsterVoiceDinamic", EvaluateGraphValueWithEase(value, 0, 1, 6, 12, cubicCurve));

                    break;
                case EffectPreset.Telephone:
                    //value = 0.5f;

                    audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Telephone")[0];
                    mixer.SetFloat("TelephoneVoiceIntencity", 1 + value * 0.5f);
                    mixer.SetFloat("TelephoneVoiceLowCut", EvaluateGraphValueWithEase(value, 0, 1, 2000, 4000, cubicCurve));
                    mixer.SetFloat("TelephoneVoiceHighCut", EvaluateGraphValueWithEase(value, 0, 1, 22000, 5000, cubicCurve));
                    break;

            }
        }
        public void SetCurve()
        {
            inSineCurve = FreeVoiceEffector.Instance.inSineCurve;
            inCubicCurve = FreeVoiceEffector.Instance.inCubicCurve;
            cubicCurve = FreeVoiceEffector.Instance.cubicCurve;
            squareCurve = FreeVoiceEffector.Instance.squareCurve;
            custumeCurve = FreeVoiceEffector.Instance.custumeCurve;
        }
        void SetOriginClip()
        {
            audioSource.clip = originClip;
        }
        public void ChangeOriginClip(AudioClip clip)
        {
            originClip = clip;
        }
        private void Update()
        {
            //value = valueChanger.value;

            if (oldPreset != effectPreset)
            {

                oldPreset = effectPreset;
                EffectPresetChanged?.Invoke();
                originClip = audioSource.clip;
                StopAllCoroutines();
                if (gameObject.transform.childCount > 0)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
                switch (effectPreset)
                {
                    case EffectPreset.None:

                        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];
                        audioSource.pitch = 1;
                        audioSource.volume = 1;
                        value = 0;
                        break;
                    case EffectPreset.Cartoon:
                        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Cartoon")[0];

                        mixer.SetFloat("CartoonVoiceMasterPitch", EvaluateGraphValueWithEase(value, 0, 1, 1, 6, inSineCurve));
                        mixer.SetFloat("CartoonChannelPitch", EvaluateGraphValueWithEase(value, 0, 1, 1.65f, 2, inSineCurve));
                        float fixedValue = EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve) > 0 ? 0 : EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve);
                        mixer.SetFloat("CartoonChorusSend", fixedValue);

                        break;

                    case EffectPreset.Monster:
                        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Monster")[0];

                        mixer.SetFloat("MonsterVoiceEchoDelay", EvaluateGraphValueWithEase(value, 0, 1, 30, 200, cubicCurve));
                        mixer.SetFloat("MonsterVoiceEchoDirect", EvaluateGraphValueWithEase(value, 0, 1, 1, 0.7f, cubicCurve));
                        mixer.SetFloat("MonsterVoiceEchoMix", EvaluateGraphValueWithEase(value, 0, 1, 0.5f, 0.8f, cubicCurve));

                        mixer.SetFloat("MonsterVoiceReverbSend", EvaluateGraphValueWithEase(value, 0, 1, -80, 0, cubicCurve));

                        mixer.SetFloat("MonsterVoiceDinamic", EvaluateGraphValueWithEase(value, 0, 1, 6, 12, cubicCurve));
                        break;

                    case EffectPreset.Telephone:
                        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Telephone")[0];
                        mixer.SetFloat("TelephoneVoiceIntencity", 1 + value * 0.5f);
                        mixer.SetFloat("TelephoneVoiceLowCut", EvaluateGraphValueWithEase(value, 0, 1, 2000, 4000, cubicCurve));
                        mixer.SetFloat("TelephoneVoiceHighCut", EvaluateGraphValueWithEase(value, 0, 1, 22000, 5000, cubicCurve));
                        break;
                }
            }
            isChangeReverse = isReverse;
            if (lastValue != value)
            {
                OnValueChange(value);
                lastValue = value;
            }
        }
        void OnValueChange(float value)
        {
            switch (effectPreset)
            {
                case EffectPreset.None:
                    break;
                case EffectPreset.Cartoon:
                    mixer.SetFloat("CartoonVoiceMasterPitch", EvaluateGraphValueWithEase(value, 0, 1, 1, 6, inSineCurve));
                    mixer.SetFloat("CartoonChannelPitch", EvaluateGraphValueWithEase(value, 0, 1, 1.65f, 2, inSineCurve));
                    float fixedValue = EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve) > 0 ? 0 : EvaluateGraphValueWithEase(value, 0, 1, -80, 160, inSineCurve);
                    mixer.SetFloat("CartoonChorusSend", fixedValue);
                    break;

                case EffectPreset.Monster:
                    mixer.SetFloat("MonsterVoiceMasterPitch", EvaluateGraphValueWithEase(value, 0, 1, 0.6f, 0.2f, cubicCurve));

                    mixer.SetFloat("MonsterVoiceEchoDelay", EvaluateGraphValueWithEase(value, 0, 1, 30, 200, cubicCurve));
                    mixer.SetFloat("MonsterVoiceEchoDirect", EvaluateGraphValueWithEase(value, 0, 1, 1, 0.7f, cubicCurve));
                    mixer.SetFloat("MonsterVoiceEchoMix", EvaluateGraphValueWithEase(value, 0, 1, 0.2f, 0.5f, cubicCurve));

                    mixer.SetFloat("MonsterVoiceReverbSend", EvaluateGraphValueWithEase(value, 0, 1, -30, -12, cubicCurve));

                    mixer.SetFloat("MonsterVoiceDinamic", EvaluateGraphValueWithEase(value, 0, 1, 6, 12, cubicCurve));
                    break;

                case EffectPreset.Telephone:
                    mixer.SetFloat("TelephoneVoiceIntencity", 1 + value * 0.4f);
                    mixer.SetFloat("TelephoneVoiceLowCut", EvaluateGraphValueWithEase(value, 0, 1, 20, 2000, cubicCurve));
                    mixer.SetFloat("TelephoneVoiceHighCut", EvaluateGraphValueWithEase(value, 0, 1, 22000, 5000, cubicCurve));

                    break;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace volumeController{
    public class volumeController : MonoBehaviour
    {
        public string volumeParametr = "MasterVolume";
        public AudioMixer mixer;

        private float _volumeValue;
        private const float _multiplier = 20f;

        private void Awake()
            => GetComponent<Slider>().onValueChanged.AddListener(HandSliderValueChanget);

        void Start()
        {
            _volumeValue = PlayerPrefs.GetFloat(volumeParametr, Mathf.Log10(GetComponent<Slider>().value) * _multiplier);
            GetComponent<Slider>().value = Mathf.Pow(10f, _volumeValue / _multiplier);
        }

        private void HandSliderValueChanget(float value)
        {
            _volumeValue = Mathf.Log10(value) * _multiplier;
            mixer.SetFloat(volumeParametr, _volumeValue);
        }

        private void Update()
            => GetComponent<Slider>().onValueChanged.AddListener(HandSliderValueChanget);

        private void OnDisable()
            => PlayerPrefs.SetFloat(volumeParametr, _volumeValue);
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TinkerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Audio clips")]
    private AudioClip failAudioClip;
    private AudioClip successAudioClip;
    [Header("Frequency")]
    public int maxFrequency;
    private int failCurrentFrequency;
    private int successCurrentFreqency;
    public TextMeshProUGUI failFrequencyText;
    public TextMeshProUGUI successFrequencyText;
    [Header("Sample rate")]
    public int maxSampleRate;
    private int failCurrentSampleRate;
    private int successCurrectSampleRate;
    public TextMeshProUGUI failSampleRateText;
    public TextMeshProUGUI successSampleRateText;
    [Header("Duration")]
    public float maxDurationSecs;
    private float failCurrentSampleDurationSecs;
    private float successCurrentSampleDurationSecs;
    public TextMeshProUGUI failDurationText;
    public TextMeshProUGUI successDurationText;
    [Header("Volume")]
    public TextMeshProUGUI volumeText;

    [Header("Sliders")]
    [SerializeField]
    private Slider failFrequencySlider;

    [SerializeField]
    private Slider successFrequencySlider;

    [SerializeField]
    private Slider successSampleRateSlider;

    [SerializeField]
    private Slider failSampleRateSlider;

    [SerializeField]
    private Slider successDurationSlider;

    [SerializeField]
    private Slider failDurationSlider;

    [SerializeField]
    private Slider volumeSlider;



    /// <summary>
    /// Sets up all the values for the sliders so that they are their max value
    /// This makes sure that the user can here a sound as soon as the user starts the project
    /// </summary>
    private void Start()
    {
        // Sets up the frequency sliders
        failCurrentFrequency = maxFrequency;
        successCurrentFreqency = maxFrequency;
        failFrequencySlider.value = 1;
        successFrequencySlider.value = 1;

        // Sets up the sample rate sliders
        failCurrentSampleRate = maxSampleRate;
        successCurrectSampleRate = maxSampleRate;
        successSampleRateSlider.value = 1;
        failSampleRateSlider.value = 1;

        // Sets up the duartion sliders
        failCurrentSampleDurationSecs = 0.5f;
        successCurrentSampleDurationSecs = 0.5f;
        failDurationSlider.value = 0.25f;
        successDurationSlider.value = 0.25f;

        // Sets the volume to 50%
        volumeSlider.value = 0.5f;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Checks for updates to the frequency sliders
        SliderUpdater();
    }

    /// <summary>
    /// Updates all the text and properties for the audio clips when the user edits the options for the sounds
    /// </summary>
    public void SliderUpdater()
    {
        // Fail Frequency
        failCurrentFrequency = Mathf.RoundToInt(failFrequencySlider.value * maxFrequency);  // Rounds the value to a full integer
        failFrequencyText.text = "Frequency: " + failCurrentFrequency.ToString();

        // Success Frequency
        successCurrentFreqency = Mathf.RoundToInt(successFrequencySlider.value * maxFrequency);  // Rounds the value to a full integer
        successFrequencyText.text = "Frequency: " + successCurrentFreqency.ToString();

        // Fail SampleRate
        failCurrentSampleRate = Mathf.RoundToInt(failSampleRateSlider.value * maxSampleRate); // Rounds the value to a full integer
        failSampleRateText.text = "Sample rate: " + failCurrentSampleRate.ToString();

        //Success SampleRate
        successCurrectSampleRate = Mathf.RoundToInt(successSampleRateSlider.value * maxSampleRate);  // Rounds the value to a full integer
        successSampleRateText.text = "Sample rate: " + successCurrectSampleRate.ToString();

        //Fail Duration
        failCurrentSampleDurationSecs = failDurationSlider.value * maxDurationSecs;
        failCurrentSampleDurationSecs = Mathf.Round(failCurrentSampleDurationSecs * 100) / 100;  // Rounds the duation to 2 decimal places
        failDurationText.text = "Duration: " + failCurrentSampleDurationSecs.ToString();

        // Success Duration
        successCurrentSampleDurationSecs = successDurationSlider.value * maxDurationSecs;
        successCurrentSampleDurationSecs = Mathf.Round(successCurrentSampleDurationSecs * 100) / 100;  // Rounds the duation to 2 decimal places
        successDurationText.text = "Duration: " + successCurrentSampleDurationSecs.ToString();

        // Volume
        audioSource.volume = volumeSlider.value;
        audioSource.volume = Mathf.Round(audioSource.volume * 10) / 10;  // Rounds the volume to 1 deciaml places
        volumeText.text = " Master volume: " + audioSource.volume.ToString();

    }


    /// <summary>
    /// This calls the function CreateFailToneAudioClip, giving it the required values that are needed to generate the sound
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the fail button is clicked
    /// </summary>
    public void PlayFailTone()
    {
        failAudioClip = CreateFailToneAudioClip(failCurrentFrequency, failCurrentSampleDurationSecs, failCurrentSampleRate);
        audioSource.PlayOneShot(failAudioClip);
    }

    /// <summary>
    /// This calls the function CreateSuccessToneAudioClip, giving it the required values that are needed to generate the sound
    /// After the audioclip has been generated it will then play the audio clip that was preduced
    /// when the success button is clicked
    /// </summary>
    public void PlaySuccessTone()
    {
        successAudioClip = CreateSuccessToneAudioClip(successCurrentFreqency, successCurrentSampleDurationSecs, successCurrectSampleRate);
        audioSource.PlayOneShot(successAudioClip);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void SaveFailAudio()
    {
        failAudioClip = CreateFailToneAudioClip(failCurrentFrequency, failCurrentSampleDurationSecs, failCurrentSampleRate);
        SaveWavUtil.Save("Fail Audio Clip", failAudioClip);
    }

    public void SaveSuccessAudio()
    {
        successAudioClip = CreateSuccessToneAudioClip(successCurrentFreqency, successCurrentSampleDurationSecs, successCurrectSampleRate);
        SaveWavUtil.Save("Success Audio Clip", successAudioClip);
    }

    /// <summary>
    /// Generates a sound from a sine wave by using different values
    /// </summary>
    /// <param name="frequency">
    /// used to set the frequency of the sound played
    /// </param>
    /// <param name="duration">
    /// used to set the duation of the sound played
    /// </param>
    /// <param name="sampleRate">
    /// used to set the sample rate of the sound played
    /// </param>
    /// <returns>
    /// The audio clip to be used for the fail button
    /// </returns>
    private AudioClip CreateFailToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    /// <summary>
    /// Generates a sound from a sine wave by using different values
    /// </summary>
    /// <param name="frequency">
    /// used to set the frequency of the sound played
    /// </param>
    /// <param name="duration">
    /// used to set the duation of the sound played
    /// </param>
    /// <param name="sampleRate">
    /// used to set the sample rate of the sound played
    /// </param>
    /// <returns>
    /// The audio clip to be used for the success button
    /// </returns>
    private AudioClip CreateSuccessToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

}
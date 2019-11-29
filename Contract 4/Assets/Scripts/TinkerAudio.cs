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

    [SerializeField]
    private TMP_Dropdown waveOptions;

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
        Debug.Log(waveOptions.value);
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
        audioSource.volume = Mathf.Round(audioSource.volume * 10) / 10;  // Rounds the volume to 1 decimal places
        volumeText.text = " Master volume: " + audioSource.volume.ToString();

    }



    /// <summary>
    /// Calls the GetWaveType function and sets the parameters up for a fail tone to be generated
    /// This will then play the generated tone
    /// </summary>
    public void PlayFailTone()
    {
        failAudioClip = GetWaveType(failCurrentFrequency, failCurrentSampleDurationSecs, failCurrentSampleRate);
        audioSource.PlayOneShot(failAudioClip);
    }

    /// <summary>
    /// Calls the GetWaveType function and sets the parameters up for a success tone to be generated
    /// This will then play the generated tone
    /// </summary>
    public void PlaySuccessTone()
    {
        successAudioClip = GetWaveType(successCurrentFreqency, successCurrentSampleDurationSecs, successCurrectSampleRate);
        audioSource.PlayOneShot(successAudioClip);
    }

    /// <summary>
    /// Calls the GetWaveType function and sets the parameters up for a fail tone to be generated
    /// This will then save the tone to the AudioClips folder
    /// </summary>
    public void SaveFailAudio()
    {
        failAudioClip = GetWaveType(failCurrentFrequency, failCurrentSampleDurationSecs, failCurrentSampleRate);
        SaveWavUtil.Save("Fail Audio Clip", failAudioClip);
    }


    /// <summary>
    /// Calls the GetWaveType function and sets the parameters up for a success tone to be generated
    /// This will then save the tone to the AudioClips folder
    /// </summary>
    public void SaveSuccessAudio()
    {
        successAudioClip = GetWaveType(successCurrentFreqency, successCurrentSampleDurationSecs, successCurrectSampleRate); 
        SaveWavUtil.Save("Success Audio Clip", successAudioClip);
    }

    /// <summary>
    /// Calls the corrrect wave function depending on the option selected in the drop down menu
    /// </summary>
    /// <param name="frequency">
    /// Used to get the correct frequency depending on whether the success or fail tone is being generated
    /// </param>
    /// <param name="sampleDurationSecs">
    /// Used to get the correct duration depending on whether the success or fail tone is being generated
    /// </param>
    /// <param name="sampleRate">
    /// Used to get the correct sample rate depending on whether the success or fail tone is being generated
    /// </param>
    /// <returns></returns>
    private AudioClip GetWaveType(int frequency, float sampleDurationSecs, int sampleRate)
    {
        AudioClip newAudioClip;
        if (waveOptions.value == 0)
        {
            newAudioClip = CreateSineWaveToneAudioClip(frequency, sampleDurationSecs, sampleRate);
            return newAudioClip;
        }

        if (waveOptions.value == 1)
        {
            newAudioClip = CreateSquareWaveToneAudioClip(frequency, sampleDurationSecs, sampleRate);
            return newAudioClip;
        }
        if (waveOptions.value == 2)
        {   
            newAudioClip = CreateTriangleWaveToneAudioClip(frequency, sampleDurationSecs, sampleRate);
            return newAudioClip;
        }

        return null;
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
    private AudioClip CreateSineWaveToneAudioClip(int frequency, float duration, int sampleRate)
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
    /// Generates a Square wave which will play if the user has selected it within the dropdown menu
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    /// <param name="sampleRate"></param>
    /// <returns></returns>
    private AudioClip CreateSquareWaveToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            if (s > 0)
            {
                s = 1;
                float v = s * maxValue;
                samples[i] = v;
            }

            if (s < 0)
            {
                s = -1;
                float v = s * maxValue;
                samples[i] = v;
            }

            
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    /// <summary>
    /// Generates a Triangle wave which will play if the user has selected it within the dropdown menu
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    /// <param name="sampleRate"></param>
    /// <returns></returns>
    private AudioClip CreateTriangleWaveToneAudioClip(int frequency, float duration, int sampleRate)
    {
        int sampleLength = Mathf.FloorToInt(sampleRate * duration);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float s = Mathf.PingPong(i * 2f * frequency / sampleRate, 1) * 2f - 1f;
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;

    }
}
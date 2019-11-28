using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Helper utility - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/

public class MelodyGenerator : MonoBehaviour
{
    /* Don't think I need these right now...
    public double frequency = 440.0; // The frequency in Hertz if the tone that the oscillator will produce.
    private double increment; // The amount of distance the wave will be moving in each frame.
    private double phase; // Actual location on the wave.
    private double sampling_frequency = 48000.0; // The default frequency of unity's audio engine.
    public float gain; // The power or volume of the oscillator.
    */

    public int tuneLength = 16;

    private AudioSource audioSource;
    private TinkeringAudio audioTinkerer;
    private List<AudioClip> audioClips = new List<AudioClip>();
    private int currentIndex = 0;

    // Dictionary containing the frequencies of each major note in the musical scale.
    private readonly Dictionary<string, int> frequencies = new Dictionary<string, int>()
    {
        { "A", 440 },
        { "B", 494 },
        { "C", 523 },
        { "D", 587 },
        { "E", 659 },
        { "F", 698 },
        { "G", 784 }
    };
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioTinkerer = GetComponent<TinkeringAudio>();

        // Using tuneLength var we can set the number of frequencies generated
        int[] randomFrequencies = GetRandomFrequencies(tuneLength);
        
        for(var i = 0; i < randomFrequencies.Length; i++)
        {
            var newClip = audioTinkerer.CreateToneAudioClip(randomFrequencies[i]);
            audioClips.Add(newClip);
        }
    }
    
    private void Update()
    {
        if (currentIndex >= tuneLength) return;

        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
            currentIndex += 1;
        }
    }

    // Randomly generating a melody.
    private int[] GetRandomFrequencies(int size)
    {
        int[] frequencyValues = new int[size];
        for (int i = 0; i < size; i++)
        {
            frequencyValues[i] = frequencies.ElementAt(Random.Range(0, frequencies.Count)).Value;
        }

        return frequencyValues;
    }
}
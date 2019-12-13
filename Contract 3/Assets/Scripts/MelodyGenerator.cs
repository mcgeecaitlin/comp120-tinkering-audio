using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Helper utility

// <copyright file = "MelodyGenerator.cs">
// MIT Licence Copyright (c) 2019.
// </copyright>
// <author> Caitlin McGee </author>

// Link to github: https://github.com/mcgeecaitlin/comp120-tinkering-audio
public class MelodyGenerator : MonoBehaviour
{

    public int tuneLength = 16; // Length of the melody played


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

    /// <summary>
    /// Randomly generate a melody using the notes stored within the dictionary.
    /// </summary>

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
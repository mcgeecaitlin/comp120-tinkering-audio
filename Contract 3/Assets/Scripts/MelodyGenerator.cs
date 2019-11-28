using System.Collections.Generic;
using UnityEngine;

public class MelodyGenerator : MonoBehaviour
{
    public double frequency = 440.0; // The frequency in Hertz if the tone that the oscillator will produce.
    private double increment; // The amount of distance the wave will be moving in each frame.
    private double phase; // Actual location on the wave.
    private double sampling_frequency = 48000.0; // The default frequency of unity's audio engine.

    public float gain; // The power or volume of the oscillator.
    public float volume = 0.1f;

    // Dictionary containing the frequencies of each major note in the musical scale.
    private readonly Dictionary<string, int> frequencies = new Dictionary<string, int>()
    {
        { "A", 440 },
        { "B", 494 },
        { "C", 523 },
        { "D", 587 },
        { "E", 659 },
        { "F", 698 },
        { "G", 784 },
        { "A", 880 }
    };

    private string[] noteValues = new string[] { "A", "B", "C", "D", "E", "F", "G", "A" };

    private void Start()
    {
        RandomMelody(8);
    }

    // Randomly generating a melody.
    private void RandomMelody(int randomAmount)
    {
        string[] pianoKeys = new string[randomAmount];
        for (int i = 0; i < randomAmount; i++)
        {
            string randomString = noteValues[Random.Range(0, noteValues.Length)]; // Getting a random value from 0 to the size of the dictionary
            pianoKeys[i] = frequencies.Keys.ElementAt(Random.Range(0, frequencies.Count));
            Debug.Log(pianoKeys[i]);
        }
    }
}
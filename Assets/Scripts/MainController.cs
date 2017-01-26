using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {
    public Text text;
    public Button button;
    public int pair;
    public int bonus;
    public List<AudioClip> audioClip = new List<AudioClip>();
    public AudioSource audioSource;
    private List<int> numbers = new List<int>();

    public void DecideShowAd() {
        if (Random.Range(0, 9) < 2) {
            ShowAd();
        } else {
            Generate();
        }
    }

    private void Generate() {
        audioSource.clip = audioClip[0];
        audioSource.Play();
        numbers.Clear();

        while (true) {
            if (numbers.Count == pair + 1)
                break;

            int n = Random.Range(1, 45);
            if (!numbers.Contains(n))
                numbers.Add(n);
        }

        numbers.Sort();

        ShowNumbers();
    }

    private void ShowNumbers() {
        text.text = string.Empty;

        for (int i = 0; i < pair; i++)
            text.text = text.text + " " + numbers[i];

        text.text = text.text + "\nBonus: " + numbers[pair];
    }

    private void ShowAd () {
        if (Advertisement.IsReady()) {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(null, options);
        }
    }

    private void HandleShowResult (ShowResult result) {
        switch (result) {
        case ShowResult.Finished:
            Generate();
            break;
        case ShowResult.Skipped:
            Generate();
            break;
        case ShowResult.Failed:
            Generate();
            break;
        }
    }
}

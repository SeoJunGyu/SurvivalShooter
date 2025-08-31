using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider slider;
    public GameObject gameOver;
    public GameObject option;

    int score = 0;

    public Image fadeImage;
    public float fadeInterval = 2f;
    private float fadeTime = 0f;
    //public bool IsFade = false;

    public TextMeshProUGUI gameOverText;
    private Vector3 startScale = Vector3.one * 0.1f;
    private Vector3 endScale = Vector3.one;

    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public bool IsPaused { get; set; }

    private void Awake()
    {
        score = 0;
        gameOver.SetActive(true);

        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        IsPaused = false;
        option.SetActive(IsPaused);
    }

    private void Start()
    {
        var findGo = GameObject.FindWithTag("Player");
        var playerHealth = findGo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += () => StartCoroutine(FadeIn());
        }

        gameOverText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            option.SetActive(IsPaused);

            Time.timeScale = IsPaused ? 0 : 1;
        }
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = $"Score : {score}";
    }

    public void SetActiveGameOverUi(bool gameover)
    {
        gameOver.SetActive(gameOver);
    }

    public IEnumerator FadeIn()
    {
        gameOverText.enabled = true;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        gameOverText.rectTransform.localScale = startScale;

        fadeTime = 0f;
        while (fadeTime < fadeInterval)
        {
            fadeTime += Time.deltaTime;
            float t = fadeTime / fadeInterval;

            color.a = Mathf.Lerp(0f, 1f, t);
            fadeImage.color = color;

            if (t < 0.8f)
            {
                float nt = t / 0.8f;
                gameOverText.rectTransform.localScale = Vector3.one * Mathf.Lerp(0.1f, 1.2f, nt);
            }
            else 
            {
                float nt = (t - 0.8f) / 0.2f;
                gameOverText.rectTransform.localScale = Vector3.one * Mathf.Lerp(1.2f, 1.0f, nt);
            }

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
        gameOverText.rectTransform.localScale = Vector3.one;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
    }

    public void Resume()
    {
        IsPaused = !IsPaused;
        option.SetActive(IsPaused);

        Time.timeScale = IsPaused ? 0 : 1;
    }
}

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Settings/Settings")]
public class Settings : MonoBehaviour
{
    [Header("Fps")]
    [SerializeField] int FPS = 30;
    [SerializeField] private Text fpsText;
    [SerializeField] private float updateInterval = 0.5f;
    private float accum = 0.0f;
    private int frames = 0;
    private float timeLeft;
    public Toggle Fps;

    private void Awake()
        => Application.targetFrameRate = FPS;

    public void FpsToggle()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (timeLeft <= 0.0f)
        {
            float fps = accum / frames;
            fpsText.text = $"FPS: {fps:0.}";

            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    private void Update()
        => FpsToggle();
}

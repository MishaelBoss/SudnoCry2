using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;

    [Header("UI")]
    [Tooltip("Getting data from player settings")] public TMP_Text TextVersion;
    [Tooltip("Text cost coin")] public Text[] TextCost;
    [Tooltip("Text display coin")] public Text TextDisplayCoin;
    [Tooltip("Text display name player")] public Text TextDisplayNamePlayer;
    [Tooltip("Text display earned")] public Text TextDisplayEarned;
    [Header("Panel"),Space(5)]
    [Tooltip("Panel error")] public GameObject PanelError;
    [Tooltip("Pages Langes")] public GameObject PageLanges;
    [Header("Button"), Space(5)]
    [Foldout("Button")] public Button ButtonRestartPlaying;
    [Foldout("Button")] public Button ButtonContinuePlaying;
    [Foldout("Button")] public Button ButtonStartPlay;
    [Foldout("Button")] public Button ButtonProfile;
    [Foldout("Button")] public Button ButtonAchive;
    [Header("InputField"), Space(5)]
    [Tooltip("InputField create name player")] public InputField InputFieldCreateName;

    private void Start() {
        Initialization();
        UpdateUIToStart();
    }

    private void Initialization() {
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {

    }

    public void UpdateUIToStart() {

    }

    public void UpdateUI(object value) {

    }

    public void UpdateUITProfile(string name) {
        TextDisplayNamePlayer.text = "Ваш ник: " + name;
    }

    public void UpdateCoin(int value) {
        TextDisplayCoin.text = value.ToString() + "$";
    }

    public void IsRegister(bool value) {
        if (value) {
            PageLanges.gameObject.SetActive(false);
            ButtonContinuePlaying.gameObject.SetActive(true);
            ButtonRestartPlaying.gameObject.SetActive(true);
            ButtonStartPlay.gameObject.SetActive(false);
            ButtonAchive.gameObject.SetActive(true);
            ButtonProfile.gameObject.SetActive(true);
        }
        else {
            PageLanges.gameObject.SetActive(true);
            ButtonRestartPlaying.gameObject.SetActive(false);
            ButtonContinuePlaying.gameObject.SetActive(false);
            ButtonStartPlay.gameObject.SetActive(true);
            ButtonAchive.gameObject.SetActive(false);
            ButtonProfile.gameObject.SetActive(false);
        }
    }

    public void ShowPanelError() {
        PanelError.SetActive(true);
    }
}
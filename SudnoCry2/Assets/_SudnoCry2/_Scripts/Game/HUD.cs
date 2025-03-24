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
    [Space(5)]
    [Tooltip("Panel error")] public GameObject PanelError;
    [Tooltip("Pages Langes")] public GameObject PageLanges;
    [Space(5)]
    [Foldout("Button")] public Button ButtonRestartPlaying;
    [Foldout("Button")] public Button ButtonContinuePlaying;
    [Foldout("Button")] public Button ButtonStartPlay;
    [Foldout("Button")] public Button ButtonProfile;
    [Foldout("Button")] public Button ButtonAchive;

    private void Start() {
        Initialization();
    }

    private void Initialization() {
        if (gameManager == null)
            gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI() {
        
    }

    public void UpdateUIToClick() { 
        
    }

    public void UpdateCoinToClick(int i) {
        TextDisplayCoin.text = i.ToString() + "$";
    }

    public void ShowPanelError() {
        PanelError.SetActive(true);
    }
}
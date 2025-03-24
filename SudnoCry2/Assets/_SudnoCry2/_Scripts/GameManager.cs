using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName = "Redmer/GameManager", fileName = "GameManager", order = 0)]
[DisallowMultipleComponent, AddComponentMenu("Redmer/Main Scripts/ GameManager")]
public class GameManager : MonoBehaviour
{
    public List<product> purchasedProducts = new List<product>();

    [Header("OutputCharacterName")]
    public Text TextName;
    [SerializeField, TextArea(1, 5)] private string _namePlayer;

    [Header("CreateNameAndCompani")]
    public InputField createNameInputField;
    public Text InputFieldName;
    [SerializeField, TextArea(1, 5)] private string _namePlayerInputField;

    [Header("Music")]
    public AudioSource musicSource;
    public List<AudioClip> clipsMusic = new List<AudioClip>();
    private int index = 0;

    [Header("PlayCoinAudio")]
    public AudioClip din;
    public AudioSource audioCoin;

    [Header("Mony")]
    public int coin;
    public int[] CostInt;
    private int ClickScore = 1;
    public int[] CostBonus;
    private int TotalBonus;

    [Space(5)]
    public GameObject ClickParent, PointTextClickScore;
    public ClickParent[] clickTextPool;

    public ViewListAchive listAchive;
    public HUD _hud;

    private Save sv = new Save();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV")) {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            _namePlayer = sv.namePlayer;
            coin = sv.coin;
            ClickScore = sv.ClickScore;

            foreach (product info in sv.savePurchasedProducts)
                purchasedProducts.Add(info);

            for (int i = 0; i < 1; i++)
            {
                CostBonus[i] = sv.CostBonus[i];
                TotalBonus += sv.CostBonus[i];
            }

            for (int i = 0; i < 1; i++)
            {
                CostInt[i] = sv.CostInt[i];
                _hud.TextCost[i].text = sv.CostInt[i] + "$";
            }
        }
    }

    public void Start()
    {
        Initialization();

        TextName.text = "Ваш ник: " + _namePlayer;

        DateTime dt = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
        TimeSpan ts = DateTime.Now - dt;

        StartCoroutine(BonusShop());
        _hud.TextVersion.text = Application.version;
        coin += (int)ts.TotalSeconds * TotalBonus;

        audioCoin = GetComponent<AudioSource>();
    }

    private void Initialization() {
        if (listAchive == null)
            listAchive = FindAnyObjectByType<ViewListAchive>();
        if (_hud == null)
            _hud = FindAnyObjectByType<HUD>();
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
            NextNewMusic();

        _hud.TextCost[0].text = "price: " + CostInt[0] + "$";

        if (coin >= 100)
            listAchive.Achieve(0);
    }

    public void ToClick()
    {
        coin += ClickScore;
        audioCoin.PlayOneShot(din);

        _hud.UpdateCoinToClick(coin);

        for (int i = 0; i < clickTextPool.Length; i++)
            clickTextPool[i] = Instantiate(PointTextClickScore, ClickParent.transform).GetComponent<ClickParent>();

        for (int i = 0; i < clickTextPool.Length; i++)
            clickTextPool[i].StartMotion(ClickScore);
    }

    public void BuyLevel() {
        if (coin >= CostInt[0])
        {
            coin -= CostInt[0];
            CostInt[0] *= 2;
            ClickScore += 1;
        }
        else _hud.ShowPanelError();
    }

    IEnumerator BonusShop()
    {
        while (true) {
            coin += CostBonus[0];
            yield return new WaitForSeconds(1);
        }
    }

    public void SetName()
    {
        _namePlayerInputField = InputFieldName.text;
        sv.namePlayer = _namePlayerInputField;
        PlayerPrefs.SetInt("Registr", 1);
    }

    private void NextNewMusic()
    {
        if (index < clipsMusic.Count)
        {
            musicSource.clip = clipsMusic[index];
            index++;
            musicSource.Play();
        }
        else index = 0;
    }

    public void DeletPlayerPrefs()
        => PlayerPrefs.DeleteAll();

    public void RestartGame() {
        if (PlayerPrefs.HasKey("Registr") && PlayerPrefs.GetInt("Registr", 1) == 1)
        {
            _hud.PageLanges.gameObject.SetActive(false);
            _hud.ButtonContinuePlaying.gameObject.SetActive(true);
            _hud.ButtonRestartPlaying.gameObject.SetActive(true);
            _hud.ButtonStartPlay.gameObject.SetActive(false);
            _hud.ButtonAchive.gameObject.SetActive(true);
            _hud.ButtonProfile.gameObject.SetActive(true);
        }
        else
        {
            _hud.PageLanges.gameObject.SetActive(true);
            _hud.ButtonRestartPlaying.gameObject.SetActive(false);
            _hud.ButtonContinuePlaying.gameObject.SetActive(false);
            _hud.ButtonStartPlay.gameObject.SetActive(true);
            _hud.ButtonAchive.gameObject.SetActive(false);
            _hud.ButtonProfile.gameObject.SetActive(false);
        }
    }

    public void URL(string url)
        => Application.OpenURL(url);

    public void RU()
    {
        string language = "RU";
        PlayerPrefs.SetString("language", language);
    }

    public void Eng()
    {
        string language = "Eng";
        PlayerPrefs.SetString("language", language);
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause) {
            sv.namePlayer = _namePlayerInputField;
            sv.coin = coin;
            sv.ClickScore = ClickScore;
            sv.CostBonus = new int[1];
            sv.CostInt = new int[2];

            for (int i = 0; i < 1; i++)
                sv.CostBonus[i] = CostBonus[i];

            for (int i = 0; i < 2; i++)
                sv.CostInt[i] = CostInt[i];

            foreach (product info in purchasedProducts)
                sv.savePurchasedProducts.Add(info);

            sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

            PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
        }
    }
#else
    private void OnApplicationQuit()
    {
        sv.namePlayer = _namePlayerInputField;
        sv.coin = coin;
        sv.ClickScore = ClickScore;
        sv.CostBonus = new int[1];
        sv.CostInt = new int[2];

        for (int i = 0; i < 1; i++)
            sv.CostBonus[i] = CostBonus[i];

        for (int i = 0; i < 2; i++)
            sv.CostInt[i] = CostInt[i];

        foreach (product info in purchasedProducts)
            sv.savePurchasedProducts.Add(info);

        sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
#endif
}

[Serializable]
public class Save
{
    public string namePlayer;
    public int coin;
    public int ClickScore;
    public List<product> savePurchasedProducts = new List<product>();
    public int[] CostInt;
    public int[] CostBonus;
    public int[] Date = new int[6];
}
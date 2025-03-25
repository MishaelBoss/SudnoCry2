using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Redmer/GameManager", fileName = "GameManager", order = 0)]
[DisallowMultipleComponent, AddComponentMenu("Redmer/Main Scripts/ GameManager")]
public class GameManager : MonoBehaviour
{
    public List<product> purchasedProducts = new List<product>();

    [Header("Data")]
    private string _namePlayer;

    [Header("Audio controller")]
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    public List<AudioClip> clipsMusic = new List<AudioClip>();
    public AudioClip din;
    private int index = 0;

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
        IsRegiter();
    }

    private void Initialization() {
        if (listAchive == null)
            listAchive = FindAnyObjectByType<ViewListAchive>();
        if (_hud == null)
            _hud = FindAnyObjectByType<HUD>();
    }

    private void IsRegiter() {
        if (sv.isRegister)
        {
            _hud.IsRegister(true);
            _hud.UpdateCoin(coin);

            DateTime dt = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
            TimeSpan ts = DateTime.Now - dt;

            StartCoroutine(BonusShop());
            _hud.TextVersion.text = Application.version;
            coin += (int)ts.TotalSeconds * TotalBonus;
        }
        else _hud.IsRegister(false);
    }

    private void Update()
    {
        CheckNextNewMusic();
        CheckAchieve();

        _hud.TextCost[0].text = "price: " + CostInt[0] + "$";
    }

    private void CheckNextNewMusic() {
        if (!audioSourceMusic.isPlaying)
            NextNewMusic();
    }

    private void CheckAchieve() {
        if (coin >= 100)
            listAchive.Achieve(0);
    }

    public void ToClick()
    {
        coin += ClickScore;
        _hud.UpdateCoin(coin);
        audioSourceSound.PlayOneShot(din);

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
        sv.namePlayer = _hud.InputFieldCreateName.text;
        sv.isRegister = true;
        IsRegiter();
    }

    private void NextNewMusic()
    {
        if (index < clipsMusic.Count)
        {
            audioSourceMusic.clip = clipsMusic[index];
            audioSourceMusic.Play();
            index++;
        }
        else index = 0;
    }

    public void OpenProfile()
        => _hud.UpdateUITProfile(_namePlayer);

    public void DeleatSave()
        => PlayerPrefs.DeleteAll();

    public void RestartScene()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
    public bool isRegister;
    public string namePlayer;
    public int coin;
    public int ClickScore;
    public List<product> savePurchasedProducts = new List<product>();
    public int[] CostInt;
    public int[] CostBonus;
    public int[] Date = new int[6];
}
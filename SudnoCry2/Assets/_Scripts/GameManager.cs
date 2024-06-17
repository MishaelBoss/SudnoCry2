using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip newMusic;

    [Header("CreateNameAndCompani")]
    public InputField createNameInputField;
    public Text InputFieldName;
    public string Name;

    [Header("PanelLanges")]
    public GameObject Langes;

    [Header("Button")]
    public Button RestartPlaying;
    public Button ContinuePlaying;
    public Button StartPlay;
    public Button achive;

    [Header("OutputCharacterName")]
    private string namePlayer;
    public Text TextName;

    public GameObject _create_name_achieve;

    [Header("Mony")]
    [SerializeField] int coin;
    public int[] CostInt;
    private int ClickScore = 1;
    public int[] CostBonus;
    private int TotalBonus;

    [Header("TextCoin")]
    public Text[] CostText;
    public Text displayCoin;

    [Header("PlayCoinAudio")]
    public AudioClip din;
    AudioSource audioCoin;

    [Header("Acihieve")]
    public Button reword1;
    public GameObject achieve1;

    public GameObject notMany;

    private Save sv = new Save();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV")) {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            coin = sv.coin;
            ClickScore = sv.ClickScore;

            for (int i = 0; i < 1; i++)
            {
                CostBonus[i] = sv.CostBonus[i];
                TotalBonus += sv.CostBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                CostInt[i] = sv.CostInt[i];
                CostText[i].text = sv.CostInt[i] + "$";
            }
        }
    }

    public void Start()
    {
        StartCoroutine(BonusShop());

        DateTime dt = new DateTime(sv.Date[0], sv.Date[1], sv.Date[2], sv.Date[3], sv.Date[4], sv.Date[5]);
        TimeSpan ts = DateTime.Now - dt;
        coin += (int)ts.TotalSeconds * TotalBonus;

        audioCoin = GetComponent<AudioSource>();
        audioCoin = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Registr") && PlayerPrefs.GetInt("Registr", 1) == 1)
        {
            Langes.gameObject.SetActive(false);
            ContinuePlaying.gameObject.SetActive(true);
            RestartPlaying.gameObject.SetActive(true);
            StartPlay.gameObject.SetActive(false);
            achive.gameObject.SetActive(true);
        }
        else
        {
            RestartPlaying.gameObject.SetActive(false);
            ContinuePlaying.gameObject.SetActive(false);
            StartPlay.gameObject.SetActive(true);
            Langes.gameObject.SetActive(true);
            achive.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("NamePlayer"))
        {
            _create_name_achieve.gameObject.SetActive(true);
            TextName.color = new Color(255, 255, 255);

            TextName.text = namePlayer = PlayerPrefs.GetString("NamePlayer");
            TextName.text = "Ваш ник:  " + namePlayer;
        }
        else _create_name_achieve.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!musicSource.isPlaying) 
        {
            musicSource.clip = newMusic; 
            musicSource.Play(); 
        }

        Name = createNameInputField.textComponent.text;

        displayCoin.text = coin + "$";
        if (coin == 0)
        {
            displayCoin.color = new Color(255, 0, 0);
        }
        else displayCoin.color = new Color(255, 255, 255);
        if (coin >= 1000000) displayCoin.text = coin.ToString() + "M" + "$";
        if (coin >= 100000000) displayCoin.text = coin.ToString() + "B" + "$";

        CostText[0].text = "price: " + CostInt[0] + "$";
        CostText[1].text = "price: " + CostInt[1] + "$";

        if (coin == 100)
        {
            reword1.interactable = true;
            achieve1.gameObject.SetActive(true);
        }
        else reword1.interactable = false;
    }

    public void ToClick()
    {
        coin += ClickScore;
        audioCoin.PlayOneShot(din);
    }

    public void BuyLevel() {
        if (coin >= CostInt[0])
        {
            coin -= CostInt[0];
            CostInt[0] *= 2;
            ClickScore *= 2;
        }
        else
            notMany.SetActive(true);
    }

    public void Shop()
    {
        if (coin >= CostInt[1])
        {
            coin -= CostInt[1];
            CostInt[1] *= 2;
            CostBonus[0] += 15;
        }
        else
            notMany.SetActive(true);
    }

    IEnumerator BonusShop()
    {
        while (true) {
            coin += CostBonus[0];
            yield return new WaitForSeconds(1);
        }
    }

    public void Achieve1() {
        coin += 150;
        reword1.interactable = false;
    }

    public void SetName()
    {
        PlayerPrefs.SetString("NamePlayer", InputFieldName.text);
        PlayerPrefs.SetInt("Registr", 1);
    }

    public void DeletPlayerPrefs()
        => PlayerPrefs.DeleteAll();

    public void RestartGame()
        =>SceneManager.LoadScene(0);

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

        for(int i = 0; i < 1; i++)
            sv.CostBonus[i] = CostBonus[i];

        for (int i = 0; i < 2; i++)
            sv.CostInt[i] = CostInt[i];

        sv.Date[0] = DateTime.Now.Year; sv.Date[1] = DateTime.Now.Month; sv.Date[2] = DateTime.Now.Day; sv.Date[3] = DateTime.Now.Hour; sv.Date[4] = DateTime.Now.Minute; sv.Date[5] = DateTime.Now.Second;

        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
#endif
}

[Serializable]
public class Save
{
    public int coin;
    public int ClickScore;
    public int[] CostInt;
    public int[] CostBonus;
    public int[] Date = new int[6];
}
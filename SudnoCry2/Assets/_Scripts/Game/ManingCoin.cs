using UnityEngine;
using UnityEngine.UI;

public class ManingCoin : MonoBehaviour
{
    [Header("Mony")]
    [SerializeField]private int coin;

    [Header("TextCoin")]
    public Text displayCoin;

    [Header("PlayCoinAudio")]
    public AudioClip din;
    AudioSource audioCoin;

    [Header("Animator")]
    Animator animator;

    [Header("SpawnCoin")]
    public GameObject coinImg;
    //public float distance = 3;
    public Transform spawnPoint;

    [Header("Images")]
    public Image img1;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioCoin = GetComponent<AudioSource>();
        coin = 0;
    }

    void Update()
    {
        if (PlayerPrefs.HasKey("Mony")) displayCoin.text = PlayerPrefs.GetInt("Mony") + "$";
        if (coin == 0) displayCoin.color = new Color(255, 0, 0);
        if (coin >= 1) displayCoin.color = new Color(255, 255, 255);
        if (coin == 100) img1.gameObject.SetActive(true);
        if (coin >= 1000000) displayCoin.text = coin.ToString() + "M" + "$";
        if (coin >= 100000000) displayCoin.text = coin.ToString() + "B" + "$";
    }

    public void ToClick()
    {
        coin++;
        PlayerPrefs.SetInt("Mony", coin);
        animator.SetTrigger("Click");
        audioCoin.PlayOneShot(din);
        Invoke("SpawnCoin", 1f);
    }

    void SpawnCoin()
    {
        //Instantiate(coinImg, Random.insideUnitCircle * distance, Quaternion.identity, transform);
        Instantiate(coinImg, spawnPoint.position, spawnPoint.rotation);
    }
}

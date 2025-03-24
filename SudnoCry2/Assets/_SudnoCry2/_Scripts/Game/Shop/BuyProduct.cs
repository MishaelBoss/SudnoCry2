using UnityEngine.UI;
using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    [Header("UI шаблона карточки")]
    [SerializeField] private Image Icon;
    [SerializeField] private Text Name;
    [SerializeField] private Text BasePriceText;
    [SerializeField] private Text IncomeText;
    public Button buyButton;

    [Header("Данные")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _income;

    [SerializeField] private product productInfo;

    private GameManager gameManager;
    private HUD _hud;

    private void Awake()
        => Initialization();

    private void Initialization() {
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (_hud == null)
            _hud = FindAnyObjectByType<HUD>();
    }

    private void Start()
        => buyButton.onClick.AddListener(() => Buy(productInfo));

    private void Update()
    {
        UpdateInformation();
        UpdateContent();
    }

    private void UpdateInformation()
    {
        _icon = productInfo.IconProduct;
        _name = productInfo.Name;
        _price = productInfo.BasePrice;
        _income = productInfo.Income;
    }

    private void UpdateContent() {
        Icon.sprite = _icon;
        IncomeText.text = _income.ToString() + "/в мин";
        Name.text = "Product: " + _name;
        BasePriceText.text = "Цена: " + _price.ToString();
    }

    public void SetInfo(product info) {
        productInfo.IconProduct = info.IconProduct;
        productInfo.Name = info.Name;
        productInfo.BasePrice = info.BasePrice;
        productInfo.Income = info.Income;
    }

    private void Buy(product info)
    {
        if (gameManager.coin >= info.BasePrice)
        {
            gameManager.coin -= info.BasePrice;
            info.BasePrice *= 2;
            gameManager.CostBonus[0] += _income;
            gameManager.purchasedProducts.Add(info);
        }
        else
            _hud.ShowPanelError();
    }
}
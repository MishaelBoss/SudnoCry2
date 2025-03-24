using UnityEngine;
using UnityEngine.UI;

public class SaleProduct : MonoBehaviour
{
    [Header("UI шаблона карточки")]
    [SerializeField] private Image Icon;
    [SerializeField] private Text Name;
    [SerializeField] private Text BasePriceText;
    [SerializeField] private Text IncomeText;
    public Button sellButton;

    [Header("Данные")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _income;

    [SerializeField] private product productInfo;

    private GameManager gameManager;

    private void Awake()
        => gameManager = FindObjectOfType<GameManager>();

    private void Start()
        => sellButton.onClick.AddListener(() => Sell(productInfo));

    private void Update()
    {
        UpdateInformation();
        UpdateContent();
    }

    private void UpdateInformation()
    {
        _icon = productInfo.IconProduct;
        _name = productInfo.Name;
        _price = productInfo.BasePrice / 3;
        _income = productInfo.Income;
    }

    private void UpdateContent()
    {
        Icon.sprite = _icon;
        IncomeText.text = _income.ToString() + "/в мин";
        Name.text = "Product: " + _name;
        BasePriceText.text = "Цена продажи: " + _price.ToString();
    }

    public void SetInfo(product info)
    {
        productInfo.IconProduct = info.IconProduct;
        productInfo.Name = info.Name;
        productInfo.BasePrice = info.BasePrice;
        productInfo.Income = info.Income;
    }

    public void Sell(product info)
    {
        gameManager.purchasedProducts.Remove(info);
        gameManager.coin += _price;
    }
}

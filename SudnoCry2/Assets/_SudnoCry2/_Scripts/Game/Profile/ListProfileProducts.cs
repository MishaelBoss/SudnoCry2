using System.Linq;
using UnityEngine;

public class ListProfileProducts : MonoBehaviour
{
    [Header("PanelProducts")]
    public SaleProduct itemPrefab;
    public Transform content;

    private GameManager gameManager;

    private void Awake()
        => gameManager = FindObjectOfType<GameManager>();

    private void OnEnable()
        => UpdateUI();

    private void UpdateUI() {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (product info in gameManager.purchasedProducts.ToList())
        {
            SaleProduct listItem = Instantiate(itemPrefab, content);
            if (listItem != null)
                listItem.SetInfo(info);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListShopProducts : MonoBehaviour
{
    public List<product> _product = new List<product>();

    [Header("PanelProducts")]
    [SerializeField] BuyProduct itemPrefab;
    [SerializeField] Transform content;

    private void Start()
    {
        foreach (product info in _product.ToList())
        {
            BuyProduct listItem = Instantiate(itemPrefab, content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
                _product.Add(info);
            }
        }
    }
}
[System.Serializable]
public class product
{
    public Sprite IconProduct;
    public string Name;
    public int BasePrice;
    public int Income;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ItemBasket : MonoBehaviour
{
    private StringBuilder itemstr = new StringBuilder();        // <! 임시

    [SerializeField] private Transform content;

    [SerializeField] private GameObject Contenttext;

    [SerializeField] private UnityEngine.UI.Text itemcost = null;        // <! 상품 가격

    [SerializeField] private GameObject uiActive;       // <! 장바구니 UI

    private Dictionary<string, int> item_Count = new Dictionary<string, int>();      // <! 아이템 이랑 개수

    private List<Item> itemlist = new List<Item>();     // <! 장바구니에 있는 아이템 리스트 (아이템 이름 비교하기 위해서 사용)

    private List<TextGroup> textGroupList = new List<TextGroup>();      // <! 아이템 이름, 갯수, 가격 표시해주는 listgroup

    public int result = 0;      // <!  장바구니에 들어있는 가격 측정

    public bool getUiActive
    {
        get { return uiActive.activeSelf; }
    }

    void Start()
    {
        GameMng.I.itembasket.Add(this);

        result = 0;
    }

    public void ActiveUI()
    {
        itemlist.Clear();
        result = 0;

        Cursor.lockState = CursorLockMode.None;
        uiActive.SetActive(true);

        foreach (var item in GameMng.I.basket)      // <! 장바구니의 아이템들
        {
            if (!item_Count.ContainsKey(item.Value.getName))
            {
                item_Count.Add(item.Value.getName, 1);      // <! 중복 개수 저장할 item_count
            }
            else
            {
                item_Count[item.Value.getName]++;
            }
            itemlist.Add(item.Value);
            result += item.Value.getPrice;
        }

        foreach (var item in item_Count)
        {
            for (int i = 0; i < itemlist.Count; i++)
            {
                if (itemlist[i].getName == item.Key)
                {
                    TextGroup temp = Instantiate(Contenttext, Vector3.zero, Quaternion.identity).GetComponent<TextGroup>();
                    textGroupList.Add(temp);
                    temp.transform.parent = content;
                    temp.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
                    temp.transform.localRotation = Quaternion.identity;
                    temp.setName = item.Key;
                    temp.setCount = item.Value.ToString();
                    temp.setPrice = (itemlist[i].getPrice * item.Value).ToString();
                    break;
                }
            }
        }

        itemcost.text = "총 가격 : " + result.ToString();
    }

    /**
     * @brief 상품 정보창 닫기
     */
    public void UnActiveUI()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        
        for(int i = 0; i < textGroupList.Count; i++)
        {
            Destroy(textGroupList[i].gameObject);
        }
        item_Count.Clear();
        textGroupList.Clear();

        uiActive.SetActive(false);
    }
}

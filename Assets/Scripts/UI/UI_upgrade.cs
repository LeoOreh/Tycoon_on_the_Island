using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_upgrade : MonoBehaviour
{
    Transform upgrade_TR;
    public static Dictionary<string, Upgrde_cls> upgrades;

    void Start()
    {
        upgrade_TR = transform.Find("Upgrade");

        upgrades = new Dictionary<string, Upgrde_cls>();
        string _pth = "/panel/Scroll View/Viewport/Content";

        foreach (GAME_STATE.Land_cls lnd in GL.state.lands)
        {
            if (lnd.name == GL.state.active_land)
            {
                foreach (GAME_STATE.Land_cls.BLDG_cls bld in lnd.buildings)
                {
                    /*
                    Transform tr = Instantiate(Resources.Load<GameObject>("UI/UI_upgrade_BLK")).transform;
                    tr.SetParent(upgrade_TR);
                    tr.localScale = Vector3.one;
                    tr.name = bld.name;
                    tr.gameObject.SetActive(false);*/

                    foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls upgr in bld.upgrades)
                    {
                        upgrades.Add(bld.name + upgr.name, new Upgrde_cls(bld.name, upgr.name, upgrade_TR.Find(bld.name + _pth)));
                    }
                }
            }
        }
    }




    public class Upgrde_cls
    {
        public string build;
        public string typ;

        public Transform TR;
        public Image icon;
        public Button btn;
        public TextMeshProUGUI TXT_price;
        public TextMeshProUGUI TXT_info;
        public int point_upgrade;

        public Upgrde_cls(string _build, string _typ, Transform _content)
        {
            build = _build;
            typ = _typ;

            TR = Instantiate(Resources.Load<GameObject>("UI/UI_upgrade")).transform;
            TR.SetParent(_content);
            TR.name = _typ;
            TR.localScale = Vector3.one;

            icon        = TR.transform.Find("Icon").GetComponent<Image>();
            btn         = TR.transform.Find("Button_Upgrade").GetComponent<Button>();
            TXT_price   = TR.transform.Find("Button_Upgrade/TXT").GetComponent<TextMeshProUGUI>();
            TXT_info    = TR.transform.Find("info").GetComponent<TextMeshProUGUI>();

            point_upgrade = Numbers_M.Get_point_upgrade(typ);

            TXT_price.text = Numbers_M.Get_Price_Upgrade(build, typ).ToString();
            btn.onClick.AddListener(() => Upgrade(build, typ));
        }



        // UI кнопок апгрейд
        void Upgrade(string bld, string typ)
        {
            Debug.Log("Upgrade() > " + typ);
            Upgrade_M.LVL_up(bld, typ);
        }
    }
}

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
        upgrade_TR.gameObject.SetActive(false);

        upgrades = new Dictionary<string, Upgrde_cls>();


        foreach (GAME_STATE.Land_cls lnd in GL.state.lands)
        {
            if (lnd.name == GL.state.active_land)
            {
                foreach (GAME_STATE.Land_cls.BLDG_cls bld in lnd.buildings)
                {
                    ADD(bld);
                }
            }
        }
    }




    void ADD(GAME_STATE.Land_cls.BLDG_cls bld)
    {
        string _pth_content = "/panel/Scroll View/Viewport/Content";

        Transform tr    = Instantiate(upgrade_TR.Find("_default"), upgrade_TR);
        tr.localScale   = Vector3.one;
        tr.name         = bld.name;
        tr.gameObject.SetActive(false);


        foreach (Transform t in tr.Find("icon").GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
        tr.Find("icon").gameObject.SetActive(true);
        tr.Find("icon/" + bld.name).gameObject.SetActive(true);


        int _lvl = bld.lvl; 
        if (_lvl <= 0) { _lvl = 1; }
        tr.Find("icon/" + bld.name + "/" + _lvl).gameObject.SetActive(true);


        Debug.Log(bld.name);

        foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls upgr in bld.upgrades)
        {
            upgrades.Add(bld.name + upgr.name, new Upgrde_cls(bld.name, upgr.name, upgrade_TR.Find(bld.name + _pth_content)));
        }
    }




    public class Upgrde_cls
    {
        public string build;
        public string typ;

        public Transform TR;
        public Transform icon;
        public Button btn;
        public TextMeshProUGUI TXT_price;
        public TextMeshProUGUI TXT_info;
        public int point_upgrade;

        public Upgrde_cls(string _build, string _typ, Transform _content)
        {
            build = _build;
            typ = _typ;


            TR = Instantiate(_content.Find("_default") , _content);
            TR.gameObject.SetActive(true);
            TR.name = _typ;
            TR.localScale = Vector3.one;


            icon        = TR.transform.Find("Icon");
            btn         = TR.transform.Find("Button_Upgrade").GetComponent<Button>();
            TXT_price   = TR.transform.Find("Button_Upgrade/TXT").GetComponent<TextMeshProUGUI>();
            TXT_info    = TR.transform.Find("info").GetComponent<TextMeshProUGUI>();


            foreach (Transform t in icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            icon.gameObject.SetActive(true);
            icon.Find(typ).gameObject.SetActive(true);


            point_upgrade = Numbers_M.Get_point_upgrade(typ);

            TXT_price.text = Numbers_M.Get_Price_Upgrade(build, typ).ToString();
            btn.onClick.AddListener(() => Upgrade(build, typ));

            _content.Find("_default").gameObject.SetActive(false);
        }



        // UI кнопок апгрейд
        void Upgrade(string bld, string typ)
        {
            Debug.Log("Upgrade() > " + typ);
            Upgrade_M.LVL_up(bld, typ);
        }
    }
}

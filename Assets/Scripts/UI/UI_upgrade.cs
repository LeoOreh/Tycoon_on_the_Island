using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_upgrade : MonoBehaviour
{
    Transform upgrade_TR;
    public static Dictionary<string, BLDG_cls> bldg;


    void Start()
    {
        upgrade_TR = transform.Find("Upgrade");
        upgrade_TR.gameObject.SetActive(false);


        bldg = new Dictionary<string, BLDG_cls>();


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
        Transform tr    = Instantiate(upgrade_TR.Find("_default"), upgrade_TR);
        bldg[bld.name] = new BLDG_cls(bld.name, tr, bld);
    }



    public class BLDG_cls
    {
        public string name;
        public Transform tr;
        public Transform icon;
        public Transform content;

        public TextMeshProUGUI TXT_summ;
        public TextMeshProUGUI TXT_lvl;

        public Dictionary<string, Upgrde_cls> upgrades;

        public BLDG_cls(string _name, Transform _tr, GAME_STATE.Land_cls.BLDG_cls bld)
        {
            tr = _tr;
            name = _name;

            tr.localScale = Vector3.one;
            tr.name = name;
            tr.gameObject.SetActive(false);

            icon = tr.Find("icon");
            foreach (Transform t in icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            icon.gameObject.SetActive(true);
            icon.Find(name).gameObject.SetActive(true);
            icon.Find(name + "/" + 1).gameObject.SetActive(true);

            string _pth_content = "panel/Scroll View/Viewport/Content";
            content = tr.Find(_pth_content);


            upgrades = new Dictionary<string, Upgrde_cls>();
            foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls upgr in bld.upgrades)
            {
                upgrades.Add(name + upgr.name, new Upgrde_cls(name, upgr.name, content));
            }
        }
    }



    public class Upgrde_cls
    {
        public string build;
        public string up_name;

        public Transform up_TR;
        public Transform up_icon;
        public Button up_btn;
        public TextMeshProUGUI up_TXT_price;
        public TextMeshProUGUI up_TXT_point;

        public int up_point_upgrade { get { return up_point_upgrade_GET_SET; } 
                                      set { up_point_upgrade_GET_SET = value; up_TXT_point.text = value.ToString(); } }
        int up_point_upgrade_GET_SET;

        public Upgrde_cls(string _build, string _up_name, Transform _content)
        {
            build = _build;
            up_name = _up_name;


            up_TR = Instantiate(_content.Find("_default"), _content);
            up_TR.gameObject.SetActive(true);
            up_TR.name = up_name;
            up_TR.localScale = Vector3.one;


            up_icon        = up_TR.transform.Find("Icon");
            up_btn         = up_TR.transform.Find("Button_Upgrade").GetComponent<Button>();
            up_TXT_price   = up_TR.transform.Find("Button_Upgrade/TXT").GetComponent<TextMeshProUGUI>();
            up_TXT_point    = up_TR.transform.Find("info").GetComponent<TextMeshProUGUI>();


            foreach (Transform t in up_icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            up_icon.gameObject.SetActive(true);
            up_icon.Find(up_name).gameObject.SetActive(true);


            up_point_upgrade = Numbers_M.Get_point_upgrade(up_name);
            up_TXT_point.text = up_point_upgrade.ToString();

            up_TXT_price.text = Numbers_M.Get_Price_Upgrade(build, up_name).ToString();
            up_btn.onClick.AddListener(() => Upgrade(build, up_name));

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

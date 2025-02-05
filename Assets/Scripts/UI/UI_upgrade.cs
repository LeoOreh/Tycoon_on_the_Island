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
        public string typ;
        public Transform tr;
        public Transform icon;
        public Transform content;

        public Transform summ_icon;
        public TextMeshProUGUI TXT_summ;
        public TextMeshProUGUI TXT_lvl;

        public Transform fill;

        public Dictionary<string, Upgrde_cls> upgrades;

        public BLDG_cls(string _name, Transform _tr, GAME_STATE.Land_cls.BLDG_cls bld)
        {
            tr = _tr;
            typ = _name;

            tr.localScale = Vector3.one;
            tr.name = typ;
            tr.gameObject.SetActive(false);

            icon = tr.Find("icon");
            foreach (Transform t in icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            icon.gameObject.SetActive(true);
            icon.Find(typ).gameObject.SetActive(true);
            icon.Find(typ + "/" + 1).gameObject.SetActive(true);

            string _pth_content = "panel/Scroll View/Viewport/Content";
            content = tr.Find(_pth_content);


            upgrades = new Dictionary<string, Upgrde_cls>();
            foreach (GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls upgr in bld.upgrades)
            {
                upgrades.Add(upgr.name, new Upgrde_cls(typ, upgr.name, content, upgr.points));
            }


            TXT_summ = tr.Find("state/summ/TXT").GetComponent<TextMeshProUGUI>();
            TXT_summ.text = Numbers_M.summ_upgrade(typ).ToString();


            summ_icon = tr.Find("state/summ/icon");
            foreach (Transform t in summ_icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            summ_icon.gameObject.SetActive(true);
            summ_icon.Find(typ).gameObject.SetActive(true);


            fill = tr.Find("state/progress/fill");
            int summ = Numbers_M.summ_upgrade(typ);
            fill.localScale = new Vector3(Numbers_M.upgrade_fill(typ, summ), 1, 1);


            TXT_lvl = tr.Find("state/lvl/TXT").GetComponent<TextMeshProUGUI>();
            TXT_lvl.text = Numbers_M.upgrade_lvl(typ, summ).ToString();
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
        public int up_point_upgrade;

        public Transform up_lock;

        public Upgrde_cls(string _build, string _up_name, Transform _content, int _point)
        {
            build = _build;
            up_name = _up_name;


            up_TR = Instantiate(_content.Find("_default"), _content);
            up_TR.gameObject.SetActive(true);
            up_TR.name = up_name;
            up_TR.localScale = Vector3.one;


            up_icon = up_TR.transform.Find("Icon");
            foreach (Transform t in up_icon.GetComponentsInChildren<Transform>()) { t.gameObject.SetActive(false); }
            up_icon.gameObject.SetActive(true);
            up_icon.Find(up_name).gameObject.SetActive(true);


            up_TXT_point = up_TR.transform.Find("info").GetComponent<TextMeshProUGUI>();
            up_point_upgrade = _point;
            up_TXT_point.text = up_point_upgrade.ToString();


            up_btn = up_TR.transform.Find("Button_Upgrade").GetComponent<Button>();
            up_TXT_price = up_TR.transform.Find("Button_Upgrade/TXT").GetComponent<TextMeshProUGUI>();
            int price = Numbers_M.Get_Price_Upgrade(build, up_name);
            up_TXT_price.text = price.ToString();
            up_btn.onClick.AddListener(() => Upgrade(build, up_name));


            up_lock = up_TR.transform.Find("lock"); 
            if(GL.state.money >= price) { up_lock.gameObject.SetActive(false); } else { up_lock.gameObject.SetActive(true);}


            _content.Find("_default").gameObject.SetActive(false);
        }



        // UI кнопок апгрейд
        void Upgrade(string bld, string typ)
        {
            //Debug.Log("Upgrade() > " + typ);
            Upgrade_M.LVL_up(bld, typ);
        }
    }


    float TS_lock;
    void Update()
    {
        if (Time.time > TS_lock) 
        { 
            TS_lock = Time.time + 1;

            UPD_lock_upgrde();
        }
    }




    void UPD_lock_upgrde()
    {
        if (upgrade_TR.gameObject.activeInHierarchy)
        {
            foreach (KeyValuePair<string, BLDG_cls> b in bldg)
            {
                foreach (KeyValuePair<string, Upgrde_cls> u in b.Value.upgrades)
                {
                    int price = Numbers_M.Get_Price_Upgrade(b.Key, u.Key);
                    if (GL.state.money >= price) { u.Value.up_lock.gameObject.SetActive(false); } else { u.Value.up_lock.gameObject.SetActive(true); }
                }
            }
        }
    }
}

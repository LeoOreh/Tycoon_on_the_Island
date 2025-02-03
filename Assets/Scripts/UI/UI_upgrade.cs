using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_upgrade : MonoBehaviour
{
    GameObject upgrade_TR;
    public static Dictionary<string, Upgrde_cls> upgrades;

    void Start()
    {
        upgrade_TR = transform.Find("Upgrade").gameObject;
        upgrades = new Dictionary<string, Upgrde_cls>();

        // init UI кнопок апгрейд
        Button[] upgrd_btns = upgrade_TR.GetComponentsInChildren<Button>(true);
        foreach (Button btn in upgrd_btns)
        {
            if (btn.name == "Button_Upgrade" && btn.transform.parent.parent.name == "Content")
            {
                btn.onClick.AddListener(() => Upgrade(btn.transform.parent.name));
                Set_price(btn.transform.Find("TXT").GetComponent<TextMeshProUGUI>());
            }
        }
    }




    //---------------------------------------------------------------------------------------------------
    // UI кнопок апгрейд
    void Upgrade( string typ)
    {
        Debug.Log("Upgrade() > " + typ);
        Upgrade_M.LVL_up(UI_bldg.active_build_UI, typ);
    }
    //---------------------------------------------------------------------------------------------------


    void Set_price(TextMeshProUGUI txt)
    {
        string build = txt.transform.parent.parent.parent.parent.parent.parent.parent.name;
        string typ = txt.transform.parent.parent.name;

        txt.text = Numbers_M.Get_Price_Upgrade(build,typ ).ToString();

        upgrades.Add(build + typ, new Upgrde_cls(build, typ, txt));
    }






    public class Upgrde_cls
    {
        public string build;
        public string typ;
        public TextMeshProUGUI TXT;
        public Upgrde_cls(string _build, string _typ, TextMeshProUGUI _TXT)
        {
            build = _build;
            typ = _typ;
            TXT = _TXT;
        }
    }
}

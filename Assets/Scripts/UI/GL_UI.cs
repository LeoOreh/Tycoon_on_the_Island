using TMPro;
using UnityEngine;

public class GL_UI : MonoBehaviour
{
    public static Transform coins;
    public static TextMeshProUGUI coins_txt;
    static int coins_count_GET_SET;


    void Start()
    {
        coins = transform.Find("Coins");
        coins_txt = coins.Find("TXT").GetComponent<TextMeshProUGUI>();
        coins_count = GL.state.money;
    }

    public static int coins_count
    {
        get { return coins_count_GET_SET; }
        set
        {
            coins_count_GET_SET = value;
            coins_txt.text = coins_count.ToString();
            GL.state.money = value;
        }
    }
}

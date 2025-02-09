using TMPro;
using UnityEngine;

public class GL_UI : MonoBehaviour
{
    public static Transform coins_TR;
    public static TextMeshProUGUI coins_txt;
    public static int coins { get; set; }


    void Start()
    {
        coins_TR = transform.Find("Coins");
        coins_txt = coins_TR.Find("TXT").GetComponent<TextMeshProUGUI>();
        coins = GL.state.money;
        coins_txt.text = coins.ToString();
    }

    public static void coins_count (int c)
    {            
        coins += c;
        coins_txt.text = coins.ToString();
        GL.state.money += c;       
    }
}

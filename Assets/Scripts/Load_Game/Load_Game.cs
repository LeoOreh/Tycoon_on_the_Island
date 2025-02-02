using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Game : MonoBehaviour
{
    void Awake()
    {
        if ( GL.state == null)
        {
            GameObject go_state = new GameObject("Game State", typeof(GAME_STATE));
            GL.state = go_state.GetComponent<GAME_STATE>();
            DontDestroyOnLoad(go_state);
        }
        JSON_M.Load();
    }
}

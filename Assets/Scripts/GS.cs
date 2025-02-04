using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS : MonoBehaviour
{

    public static Dictionary<string, LAND_cls> land;

    public GS()
    {
        land = new Dictionary<string, LAND_cls>();

        foreach (var l in GL.state.lands)
        {
            land[l.name] = new LAND_cls(l);
        }     
    }

    public class LAND_cls
    {
        public GAME_STATE.Land_cls lnd;
        public static Dictionary<string, BLD_cls> bld;

        public LAND_cls(GAME_STATE.Land_cls _lnd)
        {
            lnd = _lnd;

            bld = new Dictionary<string, BLD_cls>();
            foreach (var b in lnd.buildings)
            {
                bld[b.name] = new BLD_cls(b);
            }
        }     
    }


    public class BLD_cls
    {
        public GAME_STATE.Land_cls.BLDG_cls bld;
        public static Dictionary<string, UPGRD_cls> up;

        public BLD_cls(GAME_STATE.Land_cls.BLDG_cls _bld)
        {
            bld = _bld;

            up = new Dictionary<string, UPGRD_cls>();
            foreach (var u in bld.upgrades)
            {
                up[u.name] = new UPGRD_cls(u);
            }
        }
    }



    public class UPGRD_cls
    {
        public GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls up;

        public UPGRD_cls(GAME_STATE.Land_cls.BLDG_cls.Upgrade_cls _up)
        {
            up = _up;
        }
    }
}

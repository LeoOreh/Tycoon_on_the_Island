using UnityEngine;

[System.Serializable]
public class GAME_STATE: MonoBehaviour
{
    public int money;
    public int points;
    public Resources resources = new Resources();
    public Scene_Land_1_stone scene_Land_1_stone = new Scene_Land_1_stone();
    public Scene_Land_2_tree scene_Land_2_stone = new Scene_Land_2_tree();

    [System.Serializable]
    public class Resources
    {
        public int stone;
        public int iron;

        public int silver;
        public int gold;
        public int diamond;
    }

    [System.Serializable]
    public class Scene_Land_1_stone
    {
        public Upgrade upgrade = new Upgrade();


        [System.Serializable]
        public class Upgrade
        {
            public Upgrade_energy energy = new Upgrade_energy();
            public Upgrade_cave cave = new Upgrade_cave();
            public Upgrade_warehouse warehouse = new Upgrade_warehouse();

            [System.Serializable]
            public class Upgrade_energy
            {
                public int val1;
                public int val2;
                public int val3;
            }
            [System.Serializable]
            public class Upgrade_cave
            {
                public int val1;
                public int val2;
                public int val3;
            }
            [System.Serializable]
            public class Upgrade_warehouse
            {
                public int val1;
                public int val2;
                public int val3;
            }
        }
    }


    [System.Serializable]
    public class Scene_Land_2_tree
    {
        public Upgrade upgrade = new Upgrade();


        [System.Serializable]
        public class Upgrade
        {
            public Upgrade_energy energy = new Upgrade_energy();

            [System.Serializable]
            public class Upgrade_energy
            {
                public int val1;
                public int val2;
                public int val3;
            }

        }
    }
}

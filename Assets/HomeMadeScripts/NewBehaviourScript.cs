using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public List<int> eligibles = new List<int>();
    public List<int> cases = new List<int>();
    public List<int> revealed = new List<int>();

    public GameObject ShowCard;
    public SpriteRenderer sr;

    public GameObject CardStart;

    /*
    public jauges lifebar;
    public jauges hungerbar;
    public jauges moralbar;
    */

    public GameObject CardT;
    public GameObject CardP;
    public GameObject CardR;

    public int nbreT = 0;
    public int nbreR = 0;
    public int nbreP = 0;
    public List<int> possibilities = new List<int>();

    public GameObject TCard1;
    public GameObject TCard2;
    public GameObject TCard3;

    public GameObject RCard1;
    public GameObject RCard2;
    public GameObject RCard3;

    public GameObject PCard1;
    public GameObject PCard2;
    public GameObject PCard3;
    public GameObject PCard4;
    public GameObject PCard5;

    public List<GameObject> TCardlist = new List<GameObject>();
    public List<GameObject> RCardlist = new List<GameObject>();
    public List<GameObject> PCardlist = new List<GameObject>();

    public GameObject floor;

    public GameObject camera;
    public GameObject camrotator;

    public GameObject inventory;

    public Inventory inventoryscript = new Inventory();

    public GameObject go;
    public GameObject bossCard;
    public GameObject token;

    public bool debut = true;
    private bool moving = false;
    public bool canMove = true;


    public int ax = 0;
    public int az = 0;

    public int hitx = 0;
    public int hitz = 0;

    public float lastposx = 40;
    public float lastposz = 30;

    public int currentx = 0;
    public int currentz = 10;

    public int floorlvl = 6;

    private int decalagex = 4;
    private int decalagez = 3;


    public bool isFighting = false;
    public switchCamera sC;
    private textScript tS;

    //fiche perso
    public GameObject fichePerso;
    // ressources
    public int level = 1;
    public int life;
    public int lifemax;
    public int hunger;
    public int hungermax;
    public int moral;
    public int moralmax;
    public int xp;
    public int xpmax;
    public int gold;
    public Compteur life_compt;
    public Compteur hunger_compt;
    public Compteur moral_compt;
    public Compteur gold_compt;

    //caractéristiques du perso
    public string Name;
    public int strenght;
    public int agility;
    public int intel;
    public int charisma;
    public int luck;


    private bool[] Abilities;
    public List<string> allAbilities = new List<string>();
    
    //stats de combat
    public int idArme;
    public int idArme2;

    public int idTalisman1;
    public int idTalisman2;
    public int idTalisman3;

    public int BladeDmg;
    public int BluntDmg;
    public int MagicalDmg;
    public int ElementalDmg;
    public int aspeed;
    public int armor;
    public int mana;
    public int stamina;

    //monstres
    private Vector3 spawn;
    public GameObject Gobelin;



    // Use this for initialization
    void Start()
    {
        spawn = Gobelin.transform.position;
        Abilities = new bool[20];
        for (int i = 0; i < 19; i++)
        {
            Abilities[i] = false;
        }
        allAbilities = new List<string>
    {
        "Force Surhumaine\n+3 points de force",
        "Reflexes Eclair\n+3 points d'agilité",
        "Intellect Superieur\n+3 points d'intelligence",
        "Eloquence\n+3 points de charisme",
        "Veinard\n+3 points de chance",
        "Colporteur\nCompleter une carte Rencontre vous octroie 3 pièces d'or",
        "Campeur des Hautes-Terres\nEn passant à la zone suivante, récuperez 25 points de nourriture",
        "Roi des Mendiants\n+5 points de charisme tant que vous possedez moins de 3 pièces d'or",
        "Coeur Simple\n+10% Dégats lorsque vous avez moins de 50% nourriture, -10% sinon",
        "9\n",
        "10\n",
        "11\n",
        "12\n",
        "13\n",
        "14\n",
        "15\n",
        "16\n",
        "17\n",
        "18\n",
        "19\n"
    };



        inventoryscript = inventory.GetComponent<Inventory>();
        SetFightAttributes();
        SpriteRenderer sr = ShowCard.GetComponent<SpriteRenderer>();
        tS = fichePerso.GetComponent<textScript>();
        iniLvl(6);
    }

    public void iniLvl(int nbreCases)
    {
        System.Random rnd = new System.Random();

        int nbreCards = nbreCases - 1;

        TCardlist = new List<GameObject> { TCard1, TCard2, TCard3 };
        RCardlist = new List<GameObject> { RCard1, RCard2, RCard3 };
        PCardlist = new List<GameObject> { PCard1, PCard2, PCard3, PCard4, PCard5 };

        // set nbreT et nbre R

        nbreT = rnd.Next(1, nbreCases / 4 + 1);
        nbreR = rnd.Next(1, nbreCases / 4 + 1);
        nbreP = nbreCases - nbreR - nbreT - 1;

        if (nbreT > 0)
            possibilities.Add(1);
        if (nbreR > 0)
            possibilities.Add(2);
        if (nbreP > 0)
            possibilities.Add(3);


        //fin set nbre T et set nbreR




        revealed.Add(1010);
        int bossPos = CreateLvl(nbreCases);
        Instantiate(CardStart, new Vector3(10 * decalagex, 1, 10 * decalagez), Quaternion.identity, floor.transform);


        //reveal initiale

        revealIni(nbreT, nbreR, nbreP);



        //pose des cartes face cachée

        foreach (int i in cases)
        {
            Instantiate(go, new Vector3((i / 100) * decalagex, 1, (i % 100) * decalagez), Quaternion.identity, floor.transform);
        }
        Instantiate(bossCard, new Vector3((bossPos / 100) * decalagex, 1, (bossPos % 100) * decalagez), Quaternion.identity, floor.transform);
        revealed.Add(bossPos);


        cameraPos(cases);

        token.transform.position = (new Vector3(10 * decalagex, 1, 10 * decalagez));

        //fin pose des cartes face cachée


    }

    public bool isAdj(GameObject card1, RaycastHit card2)
    {
        bool a = (card1.transform.position.x == card2.transform.position.x + decalagex) || (card1.transform.position.x == card2.transform.position.x - decalagex);
        bool b = (card1.transform.position.z == card2.transform.position.z + decalagez) || (card1.transform.position.z == card2.transform.position.z - decalagez);
        return (a ^ b) && Math.Abs((card1.transform.position.x - card2.transform.position.x)) < decalagex + 1 && Math.Abs((card1.transform.position.z - card2.transform.position.z)) < decalagez + 1;
    }

    public void cameraPos(List<int> cases)
    {
        int sumx = 1;
        int sumz = 1;

        foreach (int i in cases)
        {
            sumx += i / 100;
            sumz += i % 100;



        }
        int L = cases.Count;

        camera.transform.position = new Vector3(sumx * decalagex / L + 16, 11, sumz * decalagez / L);




    }


    public static bool isIn(int e, List<int> list)
    {
        bool ret = false;
        foreach (int i in list)
        {
            if (e == i)
            {
                ret = true;
            }
        }

        return ret;
    }


    public int CreateLvl(int nbreCases)
    {
        int pos = 1010;
        int L = 0;
        int tirage = 0;
        System.Random rnd = new System.Random();

        cases.Add(1010);

        while (nbreCases > 0)
        {


            if (!isIn(pos - 1, cases))
            {
                eligibles.Add(pos - 1);
            }
            if (!isIn(pos + 1, cases))
            {
                eligibles.Add(pos + 1);
            }
            if (!isIn(pos + 100, cases))
            {
                eligibles.Add(pos + 100);
            }
            if (!isIn(pos - 100, cases))
            {
                eligibles.Add(pos - 100);
            }

            L = eligibles.Count;


            tirage = rnd.Next(L - 1);

            if (!isIn(eligibles[tirage], cases))
            {

                pos = eligibles[tirage];
                if (nbreCases > 1)
                {
                    cases.Add(eligibles[tirage]);
                }
                nbreCases--;
            }



        }
        cases.Remove(1010);
        return pos;
    }

    public void revealIni(int T, int R, int P)
    {
        int casesCount = cases.Count;
        int nbreCasesAReveal = floorlvl / 4;
        int elu;
        char tirage;

        int tirageCases = 0;

        int PossCount = possibilities.Count;

        System.Random rnd = new System.Random();
        GameObject TRP = CardStart;

        while (nbreCasesAReveal > 0 && PossCount > 0)
        {
            tirage = TRPchoose();
            tirageCases = rnd.Next(casesCount);
            elu = cases[tirageCases];

            revealed.Add(elu);
            cases.Remove(elu);
            casesCount--;
            nbreCasesAReveal--;

            switch (tirage)
            {
                case ('T'):
                    Instantiate(CardT, new Vector3(elu / 100 * decalagex, 1, elu % 100 * decalagez), Quaternion.identity, floor.transform);
                    PossCount--;
                    break;
                case ('R'):
                    Instantiate(CardR, new Vector3(elu / 100 * decalagex, 1, elu % 100 * decalagez), Quaternion.identity, floor.transform);
                    PossCount--;
                    break;
                case ('P'):
                    Instantiate(CardP, new Vector3(elu / 100 * decalagex, 1, elu % 100 * decalagez), Quaternion.identity, floor.transform);
                    PossCount--;
                    break;



            }

            //   Instantiate(TRP, new Vector3(elu / 100 * 4, 1, elu % 100 * 2), Quaternion.identity, floor.transform);


        }

    }


    public char TRPchoose()
    {
        System.Random rnd = new System.Random();


        int tirage = rnd.Next(possibilities.Count);

        switch (possibilities[tirage])
        {
            case (1):
                nbreT--;
                if (nbreT < 1)
                    possibilities.Remove(1);
                return 'T';
            case (2):

                nbreR--;
                if (nbreR < 1)
                    possibilities.Remove(2);
                return 'R';
            case (3):

                nbreP--;
                if (nbreP < 1)
                    possibilities.Remove(3);
                return 'P';


        }
        return ' ';

    }


    public bool reveal(int x, int z)
    {
        int tirage = 0;
        char TRPtirage = ' ';
        GameObject newcard = CardStart;

        if (isIn(x * 100 + z, revealed))
        {
            return false;
        }
        else
        {
            System.Random rnd = new System.Random();


            tirage = rnd.Next(1, 3);
            TRPtirage = TRPchoose();



            switch (TRPtirage)
            {
                case 'T':

                    //reveal T
                    tirage = rnd.Next(TCardlist.Count);

                    newcard = TCardlist[tirage];

                    TCardlist.Remove(newcard);
                    break;

                case 'R':
                    //reveal R
                    tirage = rnd.Next(RCardlist.Count);

                    newcard = RCardlist[tirage];

                    RCardlist.Remove(newcard);


                    break;

                case 'P':
                    //reveal P
                    tirage = rnd.Next(PCardlist.Count);

                    newcard = PCardlist[tirage];

                    PCardlist.Remove(newcard);
                    break;
            }


        }
        Instantiate(newcard, new Vector3(x * decalagex, 1, z * decalagez), Quaternion.identity, floor.transform);

        revealed.Add((int)(x * 100F + z));
        return true;
    }


    public void launchFight(int[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            while (enemies[i] > 0)
            {
                Instantiate(Gobelin, spawn, new Quaternion(0, 90, 0, 0));
                enemies[i]--;
            }
        }

        sC.changeCamera();
        isFighting = true;

    }

    public void endFight()
    {
        sC.changeCamera();
        isFighting = false;
    }



    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            token.transform.Translate(new Vector3(ax / decalagex, 0, az / decalagez));
            if (token.transform.position.x == hitx && token.transform.position.z == hitz)
            {
                moving = false;
            }

        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    Ray CheckBelowHit = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;



        if (!isFighting)
        {

            if (Input.GetMouseButtonDown(0))
            {


                if (Physics.Raycast(ray, out hit, 100.0F))
                {
                   

                    if (isAdj(token, hit) && canMove)
                    {

                        lastposx = token.transform.position.x;
                        lastposz = token.transform.position.z;

                        hitx = (int)hit.transform.position.x;
                        hitz = (int)hit.transform.position.z;



                        ax = hitx - (int)token.transform.position.x;
                        az = hitz - (int)token.transform.position.z;

                        moving = true;

                        GainRessource(0, 0, -1, 0);
                        canMove = false;

                        if (!isIn((int)(hit.transform.position.x) * 100 / decalagex + (int)(hit.transform.position.z) / decalagez, revealed))
                        {

                            float x = hit.transform.position.x / decalagex;
                            float z = hit.transform.position.z / decalagez;
                            bool a = reveal((int)x, (int)z);

                            if (a)
                            {
                                hit.collider.gameObject.SetActive(false);
                            }
                        }


                    }
                    
                    else
                    {
                        inventoryscript.Clicked(hit);
                    }


                }

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in floor.transform) children.Add(child.gameObject);
                children.ForEach(child => Destroy(child));

                eligibles.Clear();
                cases.Clear();
                revealed.Clear();
                possibilities.Clear();

                if (floorlvl < 14)
                    floorlvl += 2;



                iniLvl(floorlvl);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                LevelUp();
            }

            else if (Input.GetKeyDown(KeyCode.P))
            {
                fichePerso.SetActive(!fichePerso.gameObject.activeInHierarchy);
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isFighting)
            {
                launchFight(new int[] { 1 });
            }
            else
            {
                endFight();
            }
        }
        }
    

    public void LevelUp()
    {
        level++;
        tS.gameObject.SetActive(true);
        tS.LevelUp();

    }

    public void getAbility(string str)
    {
        int index = allAbilities.FindIndex(x => x == str);

        if (index > 0 && index < 20)
            Abilities[allAbilities.FindIndex(x => x == str)] = true;
        
    }


    public void GainRessource(int or, int vie, int faim, int karma)
    {

        gold += or;
        life += vie;
        moral += karma;
        hunger += faim;

        life = life > lifemax ? lifemax : life;
        moral = moral > moralmax ? moralmax : moral;
        hunger = hunger > hungermax ? hungermax : hunger;

        moral_compt.setInt(moral);
        life_compt.setInt(life, lifemax);
        hunger_compt.setInt(hunger, hungermax);
        gold_compt.setInt(gold);

        textScript tS = fichePerso.GetComponent<textScript>();
        tS.change_text();
    }

    public void SetFightAttributes()
    {
        List<InventaireSlot> equipement = inventoryscript.equipSlots;

        idArme = 0;
        idArme2 = 0;

        idTalisman1 = 0;
        idTalisman2 = 0;
        idTalisman3 = 0;
        BladeDmg = 0;
        BluntDmg = 0;
        MagicalDmg = 0;
        ElementalDmg = 0;
        armor = 0;
        aspeed = 0;
        mana = 0;
        stamina = 0;

        //application effets uniques // attributs principaux

        // calcul initial de mana et stamina en fct de force, agilité, intelligence

        mana = 25 + (intel * 15);
        stamina = 25 + (agility * 5) + (strenght * 5);

        //traitement des objets
        foreach (InventaireSlot item in equipement)
        {

            
            switch(item.id)
            {
                case 2: //épée en acier
                    {
                        idArme = 2;
                        BladeDmg += (int)(5 + agility * 0.2);
                        aspeed = 50; //?
                    }
                    break;


            }
            //on augmente l'AS en fonction de l'agilité (un peu)
            aspeed += (int)(agility * (0.2));

            //application des effets uniques (reste)

            //update fiche perso
            tS.change_text();
        }
    }


    

    }






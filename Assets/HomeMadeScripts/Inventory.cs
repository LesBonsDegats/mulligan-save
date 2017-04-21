using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public int compteur = 0;

    public GameObject cam;
    public NewBehaviourScript s;

    public InventaireSlot Inventory1;
    public InventaireSlot Inventory2;
    public InventaireSlot Inventory3;
    public InventaireSlot Inventory4;
    public InventaireSlot Inventory5;
    public InventaireSlot Inventory6;
    public InventaireSlot Inventory7;
    public InventaireSlot Inventory8;
    public InventaireSlot Inventory9;

    public InventaireSlot InventoryHelmet;
    public InventaireSlot InventoryLeftHand;
    public InventaireSlot InventoryChest;
    public InventaireSlot InventoryRightHand;
    public InventaireSlot InventoryGreaves;

    public InventaireSlot InventoryTalisman1;
    public InventaireSlot InventoryTalisman2;
    public InventaireSlot InventoryTalisman3;

    private List<InventaireSlot> stashSlots = new List<InventaireSlot>();

    public List<InventaireSlot> equipSlots = new List<InventaireSlot>();

    public bool holding = false;
    public int holdid = 0;
    public InventaireSlot chosen;

    public bool isYes;
    public bool clicked = false;


    public GameObject AreYouSure;
    public Button Button1;
    public Button Button2;



    /*
    public GameObject followCursor;
    public SpriteRenderer spriteFollowCursor;
    */
    //private Vector3 mousePosition;


    // Use this for initialization
    void Start()
    {
        Button1.onClick.AddListener(onButtonYesClick);
        Button2.onClick.AddListener(onButtonNoClick);

        chosen = Inventory1;
        s = cam.GetComponent<NewBehaviourScript>();
        //  spriteFollowCursor = followCursor.GetComponent<SpriteRenderer>();

        stashSlots = new List<InventaireSlot>
        {
            Inventory1,    Inventory2,    Inventory3,
            Inventory4,    Inventory5,    Inventory6,
            Inventory7,    Inventory8,    Inventory9,
        };

        equipSlots = new List<InventaireSlot>()
        {
            InventoryHelmet,     InventoryLeftHand,   InventoryChest,
            InventoryRightHand,  InventoryGreaves,    InventoryTalisman1,
            InventoryTalisman2,  InventoryTalisman3
        };

        AddItem(2);
        InventoryLeftHand.setId(2);
        s.SetFightAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        //     followCursor.transform.position = Input.mousePosition;


    }

    public void Clicked(RaycastHit hit)
    {
        if (hit.transform.name.Length > 9 && hit.transform.name.Substring(0, 9) == "Inventory")
        {

            string prefixe = "";
            prefixe = hit.transform.name.Substring(9, hit.transform.name.Length - 9);



            switch (prefixe)
            {
                case "1":
                    chosen = Inventory1;
                    break;
                case "2":
                    chosen = Inventory2;
                    break;
                case "3":
                    chosen = Inventory3;
                    break;
                case "4":
                    chosen = Inventory4;
                    break;
                case "5":
                    chosen = Inventory5;
                    break;
                case "6":
                    chosen = Inventory6;
                    break;
                case "7":
                    chosen = Inventory7;
                    break;
                case "8":
                    chosen = Inventory8;
                    break;
                case "9":
                    chosen = Inventory9;
                    break;
                case "_Helmet":
                    chosen = InventoryHelmet;
                    break;
                case "_RightHand":
                    chosen = InventoryRightHand;
                    break;
                case "_Chest":
                    chosen = InventoryChest;
                    break;
                case "_LeftHand":
                    chosen = InventoryLeftHand;
                    break;
                case "_Greaves":
                    chosen = InventoryGreaves;
                    break;
                case "_Talisman1":
                    chosen = InventoryTalisman1;
                    break;
                case "_Talisman2":
                    chosen = InventoryTalisman2;
                    break;
                case "_Talisman3":
                    chosen = InventoryTalisman3;
                    break;
            }
            if (chosen.id == 1)
            {

                foreach (InventaireSlot slot in stashSlots)
                {
                    if (slot.id == 1)
                    {
                        slot.setId(0);
                    }
                }
                foreach (InventaireSlot slot in equipSlots)
                {
                    if (slot.id == 1)
                    {
                        slot.setId(0);
                    }
                }


                chosen.setId(holdid);
                holdid = 0;
                s.canMove = true;
                if (equipSlots.GetType() != null)
                {
                    s.SetFightAttributes();
                }

            }
            else if (holdid == 0 && chosen.id > 1)
            {
                holdid = chosen.id;
                chosen.setId(0);
                s.canMove = false;

                foreach (InventaireSlot slot in stashSlots)
                {
                    if (slot.id == 0)
                    {
                        slot.setId(1);
                    }
                }
                foreach (InventaireSlot slot in equipSlots)
                {
                    if (slot.id == 0 && slot.hasSameType(slot.type, holdid))
                    {
                        slot.setId(1);
                    }
                }
            }
        }
        else
        {
            if (holdid > 1)
            {
                AreYouSure.gameObject.SetActive(true);
                // remove object
                StartCoroutine(WaitForCLick());
            }

        }
    }



    public bool AddItem(int itemId)
    {

        chosen = null;
        foreach (InventaireSlot slot in stashSlots)
        {
            if (slot.id == 0)
            {
                chosen = slot;
                break;
            }

        }

        if (chosen == null)
        {
            return false;
        }
        else
        {
            chosen.setId(itemId);
            return true;
        }
    }

    public void removeItem()
    {
        holdid = 0;
        foreach (InventaireSlot slot in stashSlots)
        {
            if (slot.id == 1)
            {
                slot.setId(0);
            }
        }

        foreach (InventaireSlot slot in equipSlots)
        {
            if (slot.id == 1)
            {

                slot.setId(0);
            }
        }
    }

    public void onButtonYesClick()
    {
        isYes = true;
        clicked = true;
    }

    public void onButtonNoClick()
    {
        isYes = false;
        clicked = true;
    }

    IEnumerator WaitForCLick()
    {
        while (true)
        {
            if (clicked)
            {
                clicked = false;
                if (isYes)
                {
                    removeItem();
                }
                AreYouSure.SetActive(false);
                StopAllCoroutines();
            }

            yield return new WaitForEndOfFrame();
        }
    }

}



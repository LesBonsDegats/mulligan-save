using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCard : MonoBehaviour
{

    public Sprite Image;       //CHANGE NAME

    public GameObject ShowCard;
    public GameObject cam;
    public SpriteRenderer sr;
    public GameObject Player;
    public string cardName;

    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;

    public Text B1text;
    public Text B2text;
    public Text B3text;
    public Text B4text;

    public Text RewardText;

    private bool a = false;
    private bool b = false;
    private bool c = false;
    private bool d = false;
    public string path = "";
    private float relation = 0f;
    private bool swtch;

    public GameObject Parchemin;
    public Text Recit;
    public Text Titre;

    public bool isDone = false;
    private bool cardPlaying;
    public NewBehaviourScript s;
    private bool stch = false;
    public bool clicked = false;

    private NewBehaviourScript mainscript;
    public Inventory inventory;

    
    public textScript tS;
    //public Inventory inventory;
    

    // Use this for initialization
    void Start()
    {
        Button1.onClick.AddListener(PressButton1);
        Button2.onClick.AddListener(PressButton2);
        Button3.onClick.AddListener(PressButton3);
        Button4.onClick.AddListener(PressButton4);

        s = cam.GetComponent<NewBehaviourScript>();
        SpriteRenderer sr = ShowCard.GetComponent<SpriteRenderer>();
        mainscript = cam.GetComponent<NewBehaviourScript>();

    }


    private void playCard()
    {
        RewardText.text = "";
        cardPlaying = true;
        clicked = false;
        a = false;
        b = false;
        c = false;
        d = false;
        relation = 0;

        switch (cardName)
        {

            case "TheGate":
                path = "TheGate ";
                Titre.text = "La Porte";
                break;

            case "Gobelins!":
                path = "Gobelins! ";
                Titre.text = "Gobelins!";
                break;

            case "LaSource":
                path = "LaSource ";
                Titre.text = "La Source";
                break;

            case "HordeSquelette":
                path = "HordeSquelette ";
                Titre.text = "Les Hordes du Fléau";
                break;

            case "Silhouette":
                path = "Silhouette ";
                Titre.text = "Silhouette";
                break;

            case "LeCol":
                path = "LeCol ";
                Titre.text = "LeCol";
                break;

            case "LaDame":
                path = "LaDame ";
                Titre.text = "La Dame";
                break;
            

            

        }

        nextDialogue(path);
    }


    IEnumerator WaitForClick()
    {
        while (true)
        {
            
            if (clicked)
            {
                Dial();
                nextDialogue(path);
                clicked = false;
                StopCoroutine("WaitForClick");
            }
 
            yield return new WaitForEndOfFrame();
        }
    }


    public string paragraph(string str)
    {
        int L = str.Length;
        int index = 0;
        int pas = 0;
        string paragraph = "";


        while (L - index > 67)
        {
            pas = 67;

            while (str[pas + index] != ' ')
            {
                pas--;
            }
            paragraph += str.Substring(index, pas) + '\n';
            index += pas;

        }
    
        paragraph += str.Substring(index, L - index);
        return paragraph;
    }

    public void nextDialogue(string pathto)
    {

        System.Random rnd = new System.Random();
        int tirage = 0;

        a = false;
        b = false;
        c = false;
        d = false;

        Button1.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);
        Button3.gameObject.SetActive(true);
        Button4.gameObject.SetActive(true);

        Button1.enabled = true;
        Button2.enabled = true;
        Button3.enabled = true;
        Button4.enabled = true;

        Button1.transform.localScale = new Vector3(0.09f, 0.25f, 0.25f);
        Button2.transform.localScale = new Vector3(0.09f, 0.25f, 0.25f);
        Button3.transform.localScale = new Vector3(0.09f, 0.25f, 0.25f);
        Button4.transform.localScale = new Vector3(0.09f, 0.25f, 0.25f);



        B1text.text = "";
        B2text.text = "";
        B3text.text = "";
        B4text.text = "";
        RewardText.text = "";

        switch (this.cardName)
        {
            case "TheGate":    

                switch(pathto)
                {
                    case "TheGate ":

                        Recit.text = paragraph("Alors que la pluie battante vous extenue dans vos perégrinations sur les routes sans fin de Landskøm, vous apercevez les larges portes d'une ville. Une atmosphère inquiétante se dégage des lieux. Vous voyez de mornes soldats pénétrant l'enceinte, probablement de retour de quelques pillages.. Vous ne savez si les habitants de la ville vous sont hostiles ou non, mais vous sentez que vous n'êtes pas le bienvenu.. Que voulez vous faire ?");

                        B1text.text = "S'approcher discretement de la ville..";
                        B2text.text = "Sortir de votre cachette et heler le garde";
                        B3text.text = "Rester caché pour quelques temps..";
                        B4text.text = "Poursuivre votre périple";

                    break;

                    case "TheGate 1":

                        tirage = rnd.Next(4);

                        if (tirage < 3)
                        {
                            Recit.text = paragraph("Alors que vous vous avancez furtivement de votre but, une voix sévère vient briser vos rêves de tavernes et de boisson. \"Qui va la ?\" Il semblerait que les gardes ne fassent pas mince affaire de leur besogne, ce soir.. ");

                            B1text.text = "Vous excuser de ce terrible malentendu et expliquer vos intentions pacifiques";
                            B2text.text = "Demander la permission de rentrer dans la ville";
                            B3text.text = "Vous présenter fièrement";
                            B4text.text = "S'enfuir tant qu'il est encore temps!";
                        }
                        else
                        {
                            path += '#';
                            Recit.text = paragraph("Un groupe de voyageurs s'approche de la ville, une occasion pour vous de vous mêler à la foule!");
                            B1text.text = "Continuer";
                            B2text.text = "Poursuivre votre voyage";
                        }
                    break;

                    case "TheGate 11":
                        path = "TheGate 1>";
                        Recit.text = paragraph("Les gardes ne semblent pas d'humeur à tergiverser. Sans doute avez vous interrompu une trépidante partie de cartes ?");
                        B1text.text = "Continuer";
                        break;

                    


                    case "TheGate 12":
                        Recit.text = paragraph("Un rictus se dessine sur le visage d'un des gardes. Entrer dans la ville ? Ah! Un r'negat comm' toi ? T'sais c'qu'on risque pour ça ? Par pour moins qu'deux écus! ");
                        B1text.text = "Accepter l'offre (coût : 2 or)";
                        Button1.enabled = s.gold >= 2;
                        B2text.text = "Refuser et poursuivre votre périple";

                        break;

                    case "TheGate 121":
                        Recit.text = paragraph("Non sans réchigner, vous donnez au garde le prix exigé en soupirant à l'idée qu'il dépense en argent et en filles de joie votre argent si dûrement gagné..");
                        basicReward(-2, 0, 0, 0);
                        path = "TheGate 1#";
                        B1text.text = "Continuer";
                        break;

                    case "TheGate 122":
                        Endcard(true);
                        return;

                    case "TheGate 13":
                        path = "TheGate 1>";
                        Recit.text = paragraph("Même dans l'embarras où vous êtes plongés, vous vous efforcez de ne pas perdre la face. Vous expliquez posément aux gardes ce qui vous ammène ici, en espérant qu'ils soient recéptifs à vos malheurs.");
                        B1text.text = "Continuer";

                        break;

                    case "TheGate 14":
                        Recit.text = paragraph("Voyant les gardes s'approcher, vous vous enfuyez à grandes enjambées. Ces derniers ne prennent pas le temps de poursuivre la vermine que vous êtes à leur yeux, et reprennent leur partie de cartes..");
                        B1text.text = "Poursuivre votre périple";

                        break;

                    case "TheGate 141":
                        Endcard(true);
                        break;


                    case "TheGate 1>1":
                        Recit.text = paragraph("Les gardes vous dévisagent avec méfiance.. Jouer la carte de la sincérité suffira-t-il à faire oublier ces présentations peu avantageuses ? Pour l'heure, la seule réponse que vous obtenez de la garde est un silence glacial. Un des vigiles brise finalement l'attente: \"Pourquoi qu'on vous laisserait v'nir, vagabond ? Vous autres n'semez qu'des problèmes sur vot' chemin");
                        B1text.text = "Tenter d'attirer l'empathie du garde";
                        B2text.text = "Intimider les gardes";
                        B3text.text = "Payer l'entrée dans la ville (coût : 1 or)";
                        B4text.text = "Renoncer à entrer dans la ville et pousuivre votre voyage";
                        break;

                    case "TheGate 1>11":
                        swtch = Jet(s.charisma, 20);
                    
                        path = "TheGate 1#1";
                        nextDialogue(path);
                        return;
                        

                    case "TheGate 1>12":

                        // jet de charisme + force

                        //réussite -> 

                        path = "TheGate 1#1";
                        nextDialogue(path);
                        return;


                    case "TheGate 1>13":

                        Recit.text = paragraph("Non sans réchigner, vous donnez au garde le prix exigé en soupirant à l'idée qu'il dépense en argent et en filles de joie votre argent si dûrement gagné..");
                        basicReward(-1, 0, 0, 0);

                        path = "TheGate 1#";
                        B1text.text = "Entrer dans la ville";
                        return;

                    case "TheGate 1>14":
                        Endcard(true);
                        return;

                    case "TheGate 2":
                        Recit.text = paragraph("Attendant, seul dans la lumière, l'apparition de votre interlocuteur, un vent glacial vous secoue de toutes parts. Des bruits de pas. En rythme. Un régiment de gardes sort de la grande porte, et vient s'approcher de vous..");
                        B1text.text = "Expliquer vos intentions pacifiques";
                        B2text.text = "Demander la permission de rentrer dans la ville";
                        B3text.text = "Vous présenter fièrement";
                        B4text.text = "S'enfuir tant qu'il est encore temps!";

                        break;

                    case "TheGate 21":
                        path = "TheGate 11";
                        nextDialogue(path);
                        return;

                    case "TheGate 22":
                        path = "TheGate 12";
                        nextDialogue(path);
                        return;

                    case "TheGate 23":
                        path = "TheGate 13";
                        nextDialogue(path);
                        return;

                    case "TheGate 24":
                        path = "TheGate 14";
                        nextDialogue(path);
                        return;

                    case "TheGate 3":

                        tirage = rnd.Next(2);

                        if (tirage == 0)
                        {

                            Recit.text = paragraph("Après une attente, qui sous la pluie vous parut bien longue, vous entendez le bruit rocailleux d'une cariole sur la route. Vous risquez un regard hors de votre cachette. Une caravane de marchand. Les chevaux s'arretent soudain dans un enfer de henissements.  \"Une roue a du sauter\", vous pensez.");

                            B1text.text = "Attendre qu'il descende remettre la roue pour se cacher dans les marchandises";
                            B2text.text = "Proposer votre aide";
                            B3text.text = "Le poignarder tant que ses yeux sont occupés..";
                            B4text.text = "Ne rien faire";
                        }
                        else
                        {
                            path += '#';
                            Recit.text = paragraph("Un rongeur, une relève, deux gardes parlant de femmes.. Vous ne ressentez que le froid et l'impression d'avoir attendu pour rien.");
                            basicReward(0, 0, -1, 0);
                            B1text.text = "Continuer";
                        }
                        break;


                    case "TheGate 31":
                        //jet agi
                        // -> réussite
                        path = "TheGate 1#";
                        B1text.text = "Entrer dans la ville";
                        break;

                    case "TheGate 32":
                        //jet char
                        // -> réussite
                        path = "TheGate 1#";
                        B1text.text = "Entrer dans la ville";
                        break;

                    case "TheGate 33":
                        Recit.text = paragraph("Votre méfait accompli, vous pillez le cadavre encore chaud du marchand, et prenez la fuite avant que la caravane immobile attire l'attention des gardes..");
                        basicReward(6, 0, 0, -4);
                        B1text.text = "Continuer";
                        break;

                    case "TheGate 34":
                        Recit.text = paragraph("Le marchand remet sa route en place et reprend son chemin. Vous avez manqué l'occasion.. Il est temps de reprendre votre voyage");
                        B1text.text = "Poursuivre votre périple";

                        break;

                    case "TheGate 341":
                        Endcard(true);
                        return;

                    case "TheGate 331":
                        Endcard(true);
                        break;

                    case "TheGate 3#1":
                        playCard();
                        break;

                    case "TheGate 1#1":
                        Recit.text = paragraph("Vous parvenez à entrer dans la ville sans encombre, mais souhaitez ne pas vous y éterniser.. Que voulez vous faire ?");
                        Button1.enabled = (s.gold >= 3);

                        B1text.text = "Aller à la taverne: (coût: 3 or)";
                        B2text.text = "Vous rendre au temple pour vous recueillir";
                        B3text.text = "Profiter de l'occasion pour panser vos blessures";
                        B4text.text = "Aller à l'armurerie pour affuter votre arme";

                        break;

                    case "TheGate 1#11":
                        Recit.text = paragraph("L'alcool vient réchauffer votre sang durement éprouvé, vous voyez dans l'écume des pintes le reflet joyeux de vos dernières aventures.");
                        basicReward(-3, 0, 50, 10);

                        B1text.text = "Sortir de la ville et poursuivre votre voyage";
                        path = "TheGate 1#1>";

                        break;

                    case "TheGate 1#12":
                        Recit.text = paragraph("Dans l'intimité de l'autel, vous trouvez le réconfort et une présence bienfaitrice. Est-ce un message des Grands, porteur de fortune ? Seul l'avenir vous le dira.");
                        basicReward(0, 5, 0, 30);

                        B1text.text = "Sortir de la ville et poursuivre votre voyage";
                        path = "TheGate 1#1>";

                        break;

                    case "TheGate 1#13":
                        Recit.text = paragraph("Assis à l'abri sous l'arche d'une poterne, vous entreprenez de confectionner un pansement pour couvrir vos plaies. ");
                        B1text.text = "Sortir de la ville et poursuivre votre voyage";
                        path = "TheGate 1#1>";
                        break;

                    case "TheGate 1#14":
                        Recit.text = paragraph("L'armurier vous laisse utiliser sa meule par vous même. Vous vous donnez à coeur joie de ranimer le tranchant des beaux jours de votre arme.");
                        B1text.text = "Sortir de la ville et poursuivre votre voyage";

                        path = "TheGate 1#1>";
                        break;

                    case "TheGate 1#1>1":
                        Endcard(true);
                        break; 

                    case "TheGate 4":
                        Endcard(false);
                        return;


                }
                break;



            case "Gobelins!":
                switch (path)
                {
                    case "Gobelins! ":
                        Recit.text = paragraph("Une horde de gobelins vous attaque! Aux armes !");
                        B1text.text = "Combattre!";
                        break;
                    case "Gobelins! 1":
                        s.launchFight(new int[] { 3 });

                        Recit.text = paragraph("Victorieux des créatures infâmes, vous finissez de piller les dépouilles, espérant trouver quelques écus entre deux colifichets d'os et et de silex.");
                        basicReward(2, 0, 0, 0);
                        B1text.text = "Reprendre votre périple";

                        break;

                    case "Gobelins! 11":
                        Endcard(true);
                        return;
                }
                break;

            case "LaSource":
                switch (pathto)
                {
                    case "LaSource ":
                        Recit.text = paragraph("Vous vous approchez d'un point d'eau où vous esperez vous resourcer. Alors que vous commencez à monter votre campement, vous distinguez des étincelles dans l'azur des flots. Un objet se trouve sous l'eau.. Vous estimez qu'une dizaine de mètres vous sépare du trésor.. Que souhaitez-vous faire ?");

                        B1text.text = "Laisser votre armure de coté et plonger dans l'eau";
                        B2text.text = "Vous reposer au bord de l'eau quelques instants";
                        B3text.text = "Boire l'eau du lac";
                        B4text.text = "Poursuivre votre périple";


                        break;

                    case "LaSource 1":
                        Recit.text = paragraph("Votre estimation était bien optimiste.. Le trésor est quelques mètres plus bas. L'air commence à manquer et vous sentez votre pouls s'emballer. Le bon sens vous pousse à remonter, l'avidité à descendre plus profondément.. ");

                        B1text.text = "Remonter à la surface";
                        B2text.text = "Descendre plus bas";

                        Button3.enabled = false;
                        Button4.enabled = false;
                        break;


                    case "LaSource 11":
                        Recit.text = paragraph("De retour à la surface, vous voyez un bandit s'en prenant à votre bivouac. Vous voyant de retour, il dégaine un coutelas et vous charge comme un bélier. ");

                        B1text.text = "Se défendre!";
                        break;

                    case "LaSource 111":
                        Recit.text = paragraph("La vermine terassée, vous recupérer quelques babioles de sa bourse, qui vous l'esperez auront quelque valeur que ce soit aux yeux d'un marchand");
                        basicReward(2, 0, 0, 0);

                        B1text.text = "Poursuivre votre périple";
                        path = "LaSource 121";
                        break;

                    case "LaSource 12":
                        Recit.text = paragraph("A chaque mètre que vous parcourez il vous semble que le trésor est un peu plus loin.. Vous devez vous rendre à l'évidence, vous ne parviendez pas au but, il est temps de faire demi-tour..");

                        B1text.text = "Remonter à la surface..";
                        break;

                    case "LaSource 121":
                        Recit.text = paragraph("De retour à la surface, vous n'avez pas le temps de reprendre votre souffle que vous constatez que votre bivouac a été pillé.. Animaux ? Bandits ? Qui sait. Pour l'heure, vous devez reprendre votre route.");

                        B1text.text = "Poursuivre votre périple";
                        break;

                    case "LaSource 1211":
                        Endcard(true);
                        return;

                    case "LaSource 2":
                        Recit.text = paragraph("Le fond de l'air est doux et le bruit blanc de l'eau baigne les lieux dans une tranquilité infinie. Ressourcé, vous reprenez votre chemin.");

                        B1text.text = "Reprendre votre périple";
                        break;

                    case "LaSource 21":
                        Endcard(true);
                        return;

                    case "LaSource 3":
                        Recit.text = paragraph("Fraîche et amère, l'eau vient rincer vos entrailles assechées par l'effort. Le liquide coule désormais dans vos veines, et vous emplie d'une force nouvelle. Il est temps de reprendre votre périple !");
                        B1text.text = "Reprendre votre route";
                        break;

                    case "LaSource 31":
                        Endcard(true);
                        return;

                    case "LaSource 4":
                        Endcard(false);
                        break;

                   

                }
                break;

            case "HordeSquelette":

                switch(pathto)
                {
                    case "HordeSquelette ":
                        Recit.text = paragraph("Le Fléau s'est déja étendu sur ses terres. L'air empeste la mort et les seules plantes poussant ici ne survivent que grace à la peste, charriant les dépouilles torturées.. Alors que vous vous efforcez de quitter les lieux au plus vite, une horde de squelettes animés vous attaque!");

                        B1text.text = "Combattre!";
                        break;

                    case "HordeSquelette 1":
                        Recit.text = paragraph("Les os des squelettes redeviennent poussières, et leurs armes s'ensevelissent sous la terre. Vous sentez une présence s'élever vers le ciel, et le vent vous apporte des murmures d'outretombe: \"Merci.\"");
                        B1text.text = "Reprendre votre périple";
                        break;

                    case "HordeSquelette 11":
                        Endcard(true);

                        break;
                }

                break;

            case "Silhouette":

                switch(pathto)
                {
                    case "Silhouette ":
                        Recit.text = paragraph("Vous arrivez à l'orée d'une forêt, où vous voyez une ombre se dresser entre les arbres. Une silhouette. Probablement d'une femme? Vous avez l'impression qu'elle vous appelle.. Une envie irresistible de la suivre dans la forêt vous prend.Que désirez vous faire?");
                        B1text.text = "Poursuivre la silhouette";
                        B2text.text = "Resister à l'appel et continuer votre chemin";
                        Button3.enabled = false;
                        Button4.enabled = false;
                        break;

                    case "Silhouette 1":
                        Recit.text = paragraph("Vous vous enfoncez dans les sous-bois, où vous perdez la silhouette de vue. Une intuition irrépressible vous incite à poursuivre tout droit à travers les fourrés. L'endroit grouille de vie et vous pesez chaque pas de peur d'écraser une créature rampante qui aurait été au mauvais endroit au mauvais moment..");
                        B1text.text = "Suivre votre instinct";
                        B2text.text = "Abandonner la poursuite et chasser";
                        B3text.text = "Abandonner la piste et se reposer";
                        B4text.text = "Faire demi-tour";
                        break;

                    case "Silhouette 2":
                        Endcard(true);
                        break;

                    case "Silhouette 11":
                        Recit.text = paragraph("Votre coeur vous mène jusqu'à un énorme bosquet, orné de feuilles noires et de fleurs bleues minuit. vous trouvez, posé sur le sol, un artefact enchanté.. Etait-ce donc lui qui vous appelait tout ce temps ? Désirez vous le prendre ? Vous décidez de vous frayer un passage à travers la forêt avant que la nuit ne tombe.");

                        B1text.text = "Prendre l'artefact et sortir de la forêt";
                        B2text.text = "Laisser l'artefact et sortir de la forêt";

                        Button3.enabled = false;
                        Button4.enabled = false;
                        break;

                    case "Silhouette 111":
                        Endcard(true);
                        return;

                    case "Silhouette 112":
                        Endcard(true);
                        return;

                    case "Silhouette 12":
                        //jet agi
                        Recit.text = paragraph("A quatres pattes dans la terre vous passez une heure ou deux à traquer la bête. Butin de chasse: quelques rongeurs, quelques insectes, de quoi survivre une ou deux nuits de plus..");
                        B1text.text = "Sortir de la forêt et reprendre votre voyage";
                        basicReward(0, 0, 5, 0);
                        break;

                    case "Silhouette 121":
                        Endcard(true);
                        return;

                    case "Silhouette 13":
                        Recit.text = paragraph("Vous vous asseyez sur une buche à l'ombre d'un grand chêne ou vous appreciez cette matinée ensoleillée. Un vent d'ouest vient carresser votre joue , emplissant vos narines d'odeurs d'érable et de fenouil. Il est l'heure de reprendre votre voyage!");
                        B1text.text = "Sortir de la forêt et reprendre votre voyage";
                        basicReward(0, 2, 0, 0);
                        
                        break;

                    case "Silhouette 131":
                        Endcard(true);
                        break;

                    case "Silhouette 14":
                        Recit.text = "Essouflé et las d'avoir couru en vain, vous commencez à rebrousser chemin. Vous sentez une grande aggressivité dans les lieux. Les arbres se racornissent et les racines se ploient. Vous pressez le pas avant que les choses prennent une tournure autrement plus inquiétante..";
                        B1text.text = "Sortir de la forêt et reprendre votre voyage";
                        basicReward(0, 0, 0, -2);
                        break;

                    case "Silhouette 141":
                        Endcard(true);
                        break;
                }

                break;

            case "LeCol":
                switch (pathto)
                {

                    case "LeCol ":
                        Recit.text = paragraph("Votre route s'envole vers des cîmes enneigées dont vous ignorez les dangers. Vous estimez que la traversée du col ne sera pas aisée.. Vous avez encore la possibilité de faire demi-tour pour chercher un autre chemin..");

                        B1text.text = "Braver le col";
                        B2text.text = "Faire demi-tour";
                        Button3.enabled = false;
                        Button4.enabled = false;
                        break;

                    case "LeCol 1":
                        Recit.text = paragraph("La traversée dure deux jours est en semée d'embûches. A plusieurs reprises vous échappez à des avalanches et éboulements qui auraient eu raison de moins chanceux que vous. Vous arrivez finalement de l'autre coté, extenué, mais heureux d'avoir fini la traversée.");
                        B1text.text = "Poursuivre votre périple";
                        basicReward(0, 0, -10, 0);

                        break;

                    case "LeCol 2":
                        Endcard(false);
                        DemiTour();
                        return;


                    case "LeCol 11":
                        Endcard(false);
                        return;

                }
                break;

            case "LaDame":

                switch(pathto)
                {
                    case "LaDame ":
                       Recit.text = paragraph("Vous arrivez près d'un lac où une étrange femme vous attend. Des ronces et des racines entourent ses membres et il vous semble que des bourgeons éclosent dans ses cheveux. Elle tient dans ses mains une majestueuse épée avec une revérence sans nom..");
        
                       B1text.text = "S'approcher de la dame";
                       B2text.text = "Poursuivre votre périple";
                       Button3.enabled = false;
                       Button4.enabled = false;
                  break;

                    case "LaDame 1":
                        Recit.text = paragraph("La dame vous voit et un sourire s'esquisse sur son visage. Sans dire un mot, elle vous donne l'épée et, les yeux pleins de mélancolie, elle se laisse tomber dans le lac qui, par quelque sortilège, se recouvre de ronces, de roses et de chardon.");
                        //ajouter une épée.
                        B1text.text = "Poursuivre votre périple";
                        itemReward(2);
                        break;

                    case "LaDame 2":
                        Endcard(false);
                        break;

                    case "LaDame 11":
                        Endcard(true);
                        break;
                }
                break;
        }

        ButtonDisplay(Button1);
        ButtonDisplay(Button2);
        ButtonDisplay(Button3);
        ButtonDisplay(Button4);


        clicked = false;
        StartCoroutine(WaitForClick());


    }


    public void Endcard(bool a)
    {
        s.canMove = true;
        Parchemin.SetActive(false);
        path = "";
        isDone = a;
        
    }


    public bool Dial()
    {

                if (a) 
                {
            path += "1";
                }
                else if (b)
                {
            path += "2";
                }
                else if (c)
                {
            path += "3";
                }
                else if (d) 
                {
            path += "4";
                }


        a = false;
        b = false;
        c = false;
        d = false;

        return true;
              
    }
    



    // Update is called once per frame'


        // Update is called once per frame
        void Update()
    {
        if (this.transform.position.x == Player.transform.position.x && this.transform.position.z == Player.transform.position.z)
        {

            sr.sprite = Image;
            if (isDone && !stch)
            {
                s.canMove = true;
                stch = true;
                Parchemin.gameObject.SetActive(false);
            }

            //STAPLE ENDS

            else if (!isDone && !cardPlaying)
            {
                Parchemin.gameObject.SetActive(true);
                playCard();

            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                Endcard(true);
            }    
        }        
        else
        {
            cardPlaying = false;
            stch = false;
        }
    }

    private bool Jet(int attribut, int difficulté)
    {
        System.Random rnd = new System.Random();
        int tirage = rnd.Next(difficulté + (attribut / 10));

        return (attribut > difficulté);
    }
    
    
    
    //STAPLE

    


    private void OnMouseEnter()
    {
         sr.sprite = Image; // CHANEGE NAME
    }
    private void OnMouseExit()
    {
          sr.sprite = null;
    }


    //STAPLE ENDS

    private void PressButton1()
    {
        if (cardPlaying)
        {
            a = true;
            clicked = true;
        }
    }
    private void PressButton2()
    {
        if (cardPlaying)
        {
            b = true;
            clicked = true;
        }
    }
    private void PressButton3()
    {
        if (cardPlaying)
        {
            c = true;
            clicked = true;
        }
    }
    private void PressButton4()
    {
        if (cardPlaying)
        {
            d = true;
            clicked = true;
        }
    }


    private void basicReward (int gold, int life, int hunger, int moral)
    {
        s.GainRessource(gold, life, hunger, moral);

        if (gold != 0)
        {
            if (gold > 0)
                RewardText.text += "Vous avez gagné " + gold.ToString() + " écus\n";
            else
                RewardText.text += "Vous avez perdu " + (-gold).ToString() + " écus\n";

          //  s.gold += gold;
        }

        if (life != 0)
        {
            if (life > 0)
                RewardText.text += "Vous avez gagné " + life.ToString() + " points de vie\n";
            else
                RewardText.text += "Vous avez perdu " + (-life).ToString() + " points de vie\n";

//            s.life += life;
           // s.lifebar.update();
        }

        if (hunger != 0)
        {
            if (hunger > 0)
                RewardText.text += "Vous avez gagné " + hunger.ToString() + " points de faim\n";
            else
                RewardText.text += "Vous avez perdu " + (-hunger).ToString() + " points de faim\n";

  //          s.hunger += hunger;

            //s.hungerbar.update();
        }

        if (moral != 0)
        {
            if (moral > 0)
                RewardText.text += "Vous avez gagné " + moral.ToString() + " points de morale\n";
            else
                RewardText.text += "Vous avez perdu " + (-moral).ToString() + " points de morale\n";

    //        s.moral += moral;
            //s.moralbar.update();
        }

     //   tS.change_text();
       }

    public void DemiTour()
    {
        Player.transform.position = new Vector3(s.lastposx, 1, s.lastposz);
    }


    public void ButtonDisplay(Button b)
    {
        Text t = b.GetComponentInChildren<Text>();
        if (t.text == "")
        {
            b.gameObject.SetActive(false);
        }
        else if (b.enabled == false)
        {
            b.image.color = Color.gray;
        }
        else if (b.enabled == true)
        {

            b.image.color = Color.white;
        }
    }

    
    public void itemReward(int id)
    {
        if (id > 1)
        {
          //  if (!inventory.AddItem(id) )
        //    {
        //        StartCoroutine(WaitForPlace(id));
        //    }

        }

        inventory.AddItem(id);

    }


    /*
    IEnumerator WaitForPlace(int id)
    {
        while(true)
        {
            if (inventory.RemoveObject())
            {
                itemReward(id);
                StopCoroutine(WaitForPlace(id));
            }

            yield return new WaitForEndOfFrame();
        }
    }
    */

}

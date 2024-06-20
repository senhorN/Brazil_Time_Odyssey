using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class Cutscene_fora : MonoBehaviour
{   
    public GameObject Npc;
    public GameObject Hint;
    public GameObject Slime1;
    public GameObject Slime2;
    public GameObject Slime3;
    public GameObject EfeitoEx1;
    public GameObject EfeitoEx2;
    public GameObject EfeitoEx3;
    public int nextScene = 0;
    //public GameObject dBox;
    public Vector2 destinoNpcX;
    public Vector2 destinoNpcY;
    public Vector2 destinoSlime1;
    public Vector2 destinoSlime2;
    public Vector2 destinoSlime3;
    private Animator npcAnimator;
    private Animator slimeAnimator;
    private Animator slimeAnimator2;
    private Animator slimeAnimator3;
    private bool slimePosition;
    private bool npcPosition;
    public bool dialogRun;
    private float delayEf = 0.13f;
    private float delaySl = 0.8f;
    private DialogSystem_C dialogSystem_c;

    //Velocidade de movimento do NPC
    public float velocidade = 5f;
    void Start()
    {
        //dialogSystem = dialogSystem.GetComponent<DialogSystem>();
        npcAnimator = Npc.GetComponent<Animator>();
        slimeAnimator = Slime1.GetComponent<Animator>();
        slimeAnimator2 = Slime2.GetComponent<Animator>();
        slimeAnimator3 = Slime3.GetComponent<Animator>();
        dialogSystem_c = FindObjectOfType<DialogSystem_C>();


        if (Npc != null && Hint != null && Slime1 != null && Slime2 != null && Slime3 != null)
        {
            slimePosition = false;
            npcPosition = false;
            dialogRun = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (slimePosition)
        {
            if (npcPosition)
            {
                Confronto();
            }
            else
            {
                NpcMoves();
            }
        }
        else
        {
            SlimeMoves();
        }

        if (dialogRun)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogSystem_c.Next();
            }
        }

        if(nextScene == 1)
        {
            SceneManager.LoadScene("House_Internal");
        }

    }

    public void NpcMoves()
    {
        // Calcula o vetor de dire��o para o destino
        Vector2 direcaoNpcX = (destinoNpcX - (Vector2)Npc.transform.position).normalized;

        // Move o NPC na dire��o calculada
        Npc.transform.Translate(direcaoNpcX * velocidade * Time.deltaTime);
        npcAnimator.SetInteger("Movimento", 1);


        // Verifica se o NPC chegou ao destino
        if (Vector2.Distance(Npc.transform.position, destinoNpcX) < 0.1f)
        {
            Hint.SetActive(false);
            npcAnimator.SetInteger("Movimento", 0);
            npcPosition = true;
        }
    }

    public void SlimeMoves()
    {
        Vector2 direcaoSlime1 = (destinoSlime1 - (Vector2)Slime1.transform.position).normalized;
        Vector2 direcaoSlime2 = (destinoSlime2 - (Vector2)Slime2.transform.position).normalized;
        Vector2 direcaoSlime3 = (destinoSlime3 - (Vector2)Slime3.transform.position).normalized;

        Slime1.transform.Translate(direcaoSlime1 * velocidade * Time.deltaTime);
        Slime2.transform.Translate(direcaoSlime2 * velocidade * Time.deltaTime);
        Slime3.transform.Translate(direcaoSlime3 * velocidade * Time.deltaTime);
        slimeAnimator.SetInteger("MoveSlime", 1);
        slimeAnimator2.SetInteger("MoveSlime", 1);
        slimeAnimator3.SetInteger("MoveSlime", 1);

        if (Vector2.Distance(Slime1.transform.position, destinoSlime1) < 0.1f &&
            Vector2.Distance(Slime2.transform.position, destinoSlime2) < 0.1f &&
            Vector2.Distance(Slime3.transform.position, destinoSlime3) < 0.1f)
        {
            Hint.SetActive(true);
            slimePosition = true;
        }
    }

    public void Confronto()
    {
        if (EfeitoEx1 != null && EfeitoEx2 != null && EfeitoEx3 != null)
        {
            EfeitoEx1.SetActive(true);
            EfeitoEx2.SetActive(true);
            EfeitoEx3.SetActive(true);

            Destroy(EfeitoEx1, delayEf);
            Destroy(EfeitoEx2, delayEf);
            Destroy(EfeitoEx3, delayEf);

            slimeAnimator.SetBool("die", true);
            slimeAnimator2.SetBool("die", true);
            slimeAnimator3.SetBool("die", true);

            Destroy(Slime1, delaySl);
            Destroy(Slime2, delaySl);
            Destroy(Slime3, delaySl);

            npcAnimator.SetInteger("Movimento", 3);
            Dialogo();

        }
    }

    public void Dialogo()
    {
        dialogRun = true;
        dialogSystem_c.Next();
    }

}

//NPC
//x=123.13 - y=-206.28 X
//x=123.13 - y=-205.8 Y

//Slime
// X=114.45 Y=-203.43
// X=123.21 Y=-202.06
// X=130.49 Y=-203.81

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE_C
{
    DISABLED,
    WAITING,
    TYPING
}
public class DialogSystem_C : MonoBehaviour
{
    public DialogueData cutscene_01;
    public Cutscene_fora cutscene_Fora;
    private int currentText = 0;
    public GameObject UI;
    private TypeTextAnimation_C typeText_c;
    private DialogUI_C dialogueUi_c;
    private STATE_C state_c;

    void Awake()
    {
        typeText_c = GetComponent<TypeTextAnimation_C>();
        typeText_c.TypeFinished_C += OnTypeFinished;
        dialogueUi_c = FindObjectOfType<DialogUI_C>();
        cutscene_Fora.GetComponent<Cutscene_fora>();
    }

    void Start()
    {        
        state_c = STATE_C.DISABLED;
    }

    void Update()
    {
        if (state_c == STATE_C.DISABLED) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state_c == STATE_C.TYPING)
            {
                typeText_c.SkipTyping();
            }
            else if (state_c == STATE_C.WAITING)
            {
                Next();
            }
        }
    }

    public void Next()
    {
        if (cutscene_Fora.dialogRun)
        {
            if (currentText < cutscene_01.talkScript.Count)
            {
                dialogueUi_c.SetName(cutscene_01.talkScript[currentText].name);
                typeText_c.fullText = cutscene_01.talkScript[currentText].text;
                currentText++;

                UI.SetActive(true);
                typeText_c.StartTyping();
                state_c = STATE_C.TYPING;
            }
            else
            {
                EndDialogue();
                cutscene_Fora.nextScene = 1;
            }

        }
    }

        void OnTypeFinished()
        {
            state_c = STATE_C.WAITING;
        }

        void EndDialogue()
        {
            state_c = STATE_C.DISABLED;
            currentText = 0;
            UI.SetActive(false);
        }
    }

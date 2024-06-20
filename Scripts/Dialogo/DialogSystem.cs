using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}

public class DialogSystem : MonoBehaviour
{
    public DialogueData dialogueData;

    private int currentText = 0;
    public GameObject UI;
    private TypeTextAnimation typeText;
    private DialogueUi dialogueUi;
    private STATE state;

    void Awake()
    {
        typeText = GetComponent<TypeTextAnimation>();
        typeText.TypeFinished += OnTypeFinished;
        dialogueUi = FindObjectOfType<DialogueUi>();
    }

    void Start()
    {
        state = STATE.DISABLED;
    }

    void Update()
    {
        if (state == STATE.DISABLED) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state == STATE.TYPING)
            {
                typeText.SkipTyping();
            }
            else if (state == STATE.WAITING)
            {
                Next();
            }
        }
    }

    public void Next()
    {
        if (currentText < dialogueData.talkScript.Count)
        {
            dialogueUi.SetName(dialogueData.talkScript[currentText].name);
            typeText.fullText = dialogueData.talkScript[currentText].text;
            currentText++;

            UI.SetActive(true);
            typeText.StartTyping();
            state = STATE.TYPING;
        }
        else
        {
            EndDialogue();
        }
    }

    void OnTypeFinished()
    {
        state = STATE.WAITING;
    }

    void EndDialogue()
    {
        state = STATE.DISABLED;
        currentText = 0;
        UI.SetActive(false);
    }
}

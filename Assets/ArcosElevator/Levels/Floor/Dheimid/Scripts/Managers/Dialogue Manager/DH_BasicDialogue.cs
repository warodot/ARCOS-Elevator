using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DH_BasicDialogue : MonoBehaviour
{
    //Clases serializadas que contentrán los componentes necesarios para funcionar.
    [Serializable]
    public class ComponentsRelated
    {
        //Componentes de TextMeshPro y la velocidad de muestreo char por char
        [Header("TextMeshPro related")]
        public TextMeshProUGUI t_name; 
        public TextMeshProUGUI t_dialogue;
        public float t_timeNetxChar = 0.02f;

        //Los componentes relacionado al background y el tiempo en el que aparecerán
        [Space]
        [Header("Background Related")]
        public CanvasGroup b_backrgorund;
        public CanvasGroup b_imageNextDialogue;
        public float timeAppear;
    }

    [Header("Relacionado al texto y sus componentes")]
    public ComponentsRelated m_necessaryComponents;

    //Relacionado al audio, los sonidos que mostrará y cada cuantas letras sonarán.
    [Space]
    [Header ("Audio Related")]
    public AudioSource m_source;
    public List<AudioClip> m_clips;
    public int m_numDivisor;

    //A la rápida mientras pienso en otra solución...
    public DH_Inventory m_inventory;
    public List<DH_DialogueActions> m_actionsList;
    void Start()
    {
        m_actionsList = FindObjectsOfType<DH_DialogueActions>().ToList();
    }

    //Inicia la conversación desde fuera, ya sea de un evento o de lo que sea...
    bool m_inConversation;
    public void StartConversation(DHSO_Conversation conversations)
    {
        if (!m_inConversation) StartCoroutine(StartBackground(conversations));
    }

    //Inicia el comportamiento del Background
    DHSO_Conversation m_currentConversation;
    int m_currentDialogue;
    IEnumerator StartBackground(DHSO_Conversation SOConversation)
    {
        //Cambia el estado de juego a Dialogue
        DH_GameManager.State = GameStates.Dialogue;

        //Marcamos el inicio de la conversación
        m_inConversation = true;
        
        //Corrutina para el fade
        #region Corrutina que cambia el background y su fade

        float initial = m_necessaryComponents.b_backrgorund.alpha;
        float target = 1;

        for (float i = 0; i < m_necessaryComponents.timeAppear; i+=Time.deltaTime)
        {
            float t = i / m_necessaryComponents.timeAppear;
            m_necessaryComponents.b_backrgorund.alpha = Mathf.Lerp(initial, target, t);
            yield return null;
        }

        m_necessaryComponents.b_backrgorund.alpha = target;

        #endregion
        
        //Analizamos los dialogos mostrados
        AnalizedShowedDialogues(SOConversation);

        //Iniciamos el que debería ser el diálogo actual.
        StartCoroutine(StartDialogues(m_currentConversation, m_currentDialogue));
    }

    //Finaliza el background
    IEnumerator EndBackground()
    {
        //Prepara el dialogo para finalizar
        PrepareDialogue();

        #region Finaliza el background y el fade y todo eso...
        float initial = m_necessaryComponents.b_backrgorund.alpha;

        for (float i = 0; i < m_necessaryComponents.timeAppear; i+=Time.deltaTime)
        {
            float t = i / m_necessaryComponents.timeAppear;
            m_necessaryComponents.b_backrgorund.alpha = Mathf.Lerp(initial, 0, t);
            yield return null;
        }

        m_necessaryComponents.b_backrgorund.alpha = 0;

        #endregion
       
        //Termina la conversación
        m_inConversation = false;

        //Return game state to Gameplay
        DH_GameManager.State = GameStates.Gameplay;
    }

    //Relacionado al guardar ciertos carácteres en una bolsa, como los tags, para que no sean mostrados.
    bool insideTag;
    string currentTag;

    //Preparamos los componentes y vaciamos los textos que sea necesario vaciar
    void PrepareDialogue()
    {
        m_necessaryComponents.t_name.text = ""; 
        m_necessaryComponents.t_dialogue.text = "";
        m_necessaryComponents.b_imageNextDialogue.alpha = 0;
        currentTag = "";
    }

    void AnalizedShowedDialogues(DHSO_Conversation conversation)
    {
        //Analizar si la lista de los dialogos que han sido mostrados no contiene el dialogo actual.
        if (!m_showedConv.Contains(conversation))
        {
            //Si no contiene el dialogo, entonces asignale el scriptable por defecto al current.
            m_currentConversation = conversation;
            m_currentDialogue = 0;
        }
        else
        {
            if (conversation.m_nextConversation.Count > 0)
            {
                for (int i = 0; i < conversation.m_nextConversation.Count; i++)
                {
                    if (!m_showedConv.Contains(conversation.m_nextConversation[i]))
                    {
                        m_currentConversation = conversation.m_nextConversation[i];
                        m_currentDialogue = 0;
                        break;
                    }
                    else
                    {
                        m_currentConversation = conversation.m_nextConversation[i];
                        m_currentDialogue = conversation.m_nextConversation[i]._conversation.Count - 2;
                    }
                }
            }
            else
            {
                m_currentConversation = conversation;
                m_currentDialogue = conversation._conversation.Count - 2;
            }
        }
    }

    //Inicia el diálogo
    bool inDialogue;
    IEnumerator StartDialogues(DHSO_Conversation conversation, int numDialogue)
    {
        //Se prepara el dialogo para mostrar el texto
        PrepareDialogue();

        //Marcamos que el diálogo ha comenzado
        inDialogue = true;

        //Asignamos el nombre y el dialogo del scripable object al TextMeshPro
        m_necessaryComponents.t_name.text = conversation._conversation[numDialogue].nameCharacter + ":";
        m_necessaryComponents.t_dialogue.text = conversation._conversation[numDialogue].dialogue;

        //Creamos un arreglo de Char por cada letra de la conversación
        char [] _characters = conversation._conversation[numDialogue].dialogue.ToCharArray();

        //Se inicia la corrutina para tener la posibilidad de saltar el diálogo.
        StartCoroutine(SkipDialogue());
        m_canSkip = true;

        #region Todo el tema de pasar letra por letra y ocultar etiquetas y todo eso...

        for (int i = 0; i < _characters.Length; i++)
        {
            if (_characters[i] == '<') insideTag = true;
            else if (_characters[i] == '>') insideTag = false;

            if (insideTag) currentTag += _characters[i];
            else 
            {
                if (IsMultiplo(i, m_numDivisor))
                {
                    int randomValue = UnityEngine.Random.Range(0, m_clips.Count);
                    if(m_canSkip) m_source.PlayOneShot(m_clips[randomValue]);
                }
                m_necessaryComponents.t_dialogue.text = currentTag += _characters[i];
                if (m_canSkip) yield return new WaitForSeconds(m_necessaryComponents.t_timeNetxChar);
            }
        }

        #endregion

        yield return null;

        //Marcamos que el diálogo ha terminado 
        inDialogue = false;

        //Iniciamos la corrutina para poder avanzar al siguiente diálogo
        StartCoroutine(WaitNextDialogue(conversation, numDialogue));
    }

    public bool IsMultiplo(int num, int divisor)
    {
        return num % divisor == 0;
    }

    //Relacionado a skipear el dialogo.
    bool m_canSkip;
    IEnumerator SkipDialogue()
    {
        //Asignamos que estamos skipeeando
        m_canSkip = false;

        //Analizamos que mientras estemos en el diálogo, podremos saltar, de lo contrario, pues ya nop.
        while(inDialogue)
        {
            //Si no hemos saltado aún, saltaremos el diálogo y le avisaremos a la corrutina que hemos saltado el dialogo.
            if (Input.GetKeyDown(KeyCode.Mouse0) && m_canSkip)
            {
                m_canSkip = false;
                inDialogue = false;
            }
            yield return null;
        }
    }

    IEnumerator WaitNextDialogue(DHSO_Conversation conversation, int currentDialogue)
    {
        StartCoroutine(FlickerAnimation(m_necessaryComponents.b_imageNextDialogue));
        int maxDialogue = conversation._conversation.Count;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                StopAllCoroutines();
                
                if (currentDialogue < maxDialogue - 1)
                {
                    int nextdialogue = currentDialogue + 1;
                    StartCoroutine(StartDialogues(conversation, nextdialogue));
                }
                else
                {
                    if (conversation.m_showOneTime) AddShowedConversation(conversation);
                    
                    for (int i = 0; i < m_actionsList.Count; i++)
                    {
                        if (m_actionsList[i].m_name == conversation.actionName) m_actionsList[i].m_action?.Invoke();
                    }

                    Debug.Log("Se acabo");
                    StartCoroutine(EndBackground());
                }
            }

            yield return null;
        }
    }

    IEnumerator FlickerAnimation(CanvasGroup m_warning)
    {
        bool active = true;
        float target, initial, timer;

        while (true)
        {
            target = active ? 1 : 0;
            initial = active ? 0 : 1;
            timer = 0;

            while (timer < 0.5f)
            {
                m_warning.alpha = Mathf.Lerp(initial, target, timer / 0.5f);
                timer += Time.deltaTime;
                yield return null;
            }

            m_warning.alpha = target;
            active = !active;
            yield return new WaitForSeconds(0.2f);
        }
    }

    List<DHSO_Conversation> m_showedConv = new List<DHSO_Conversation>();
    public void AddShowedConversation(DHSO_Conversation m_conversation)
    {
        if (!m_showedConv.Contains(m_conversation)) m_showedConv.Add(m_conversation);
    }
}
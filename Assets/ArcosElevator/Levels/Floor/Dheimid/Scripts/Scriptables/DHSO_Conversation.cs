using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "DHScriptables/conversation")]
public class DHSO_Conversation : ScriptableObject
{
    [Serializable]
    public class Dialogues
    {
        public string nameCharacter;
        [TextArea (3, 3)] public string dialogue;
    }

    [Header("Referencia de color para los nombres de cada personaje")]
    public Color colorReference;

    [Space]
    [Space]
    [Header("Lista de conversaciones y sus extras")]
    public List<Dialogues> _conversation;
    public List<DHSO_Conversation> m_nextConversation;
    public bool m_showOneTime;
    public string actionName;
}

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
//using FMODUnity;

namespace LucasRojo
{
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField] string lineActual;
        [SerializeField] int index;
        public TextMeshProUGUI texto;
        public bool BlockText;
        public float speed = 0.02f;

        public AudioSource audioSource;
        //public StudioEventEmitter emitter;
        [SerializeField, TextArea(4, 6)] string[] dialogue;
        public UnityEvent[] evento;


        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine(Dialogo());
        }

        // Update is called once per frame
        void Update()
        {
            texto.text = lineActual;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                if (lineActual == dialogue[index])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    lineActual = dialogue[index];
                }
            }

        }

        public IEnumerator Dialogo()
        {
            lineActual = string.Empty;
            if (evento.Length > index)
            {
                evento[index]?.Invoke();
            }

            bool skipChar = false;
            int charCount = 0; 
            foreach (char c in dialogue[index])
            {

                if (c == '<' || skipChar)
                {
                    skipChar = true;
                    if (c == '>') skipChar = false;
                    else
                    {
                        lineActual += c;
                        continue;
                    }
                }
                lineActual += c;

                // Solo reproducir el sonido cada 2 ciclos
                if (charCount % 2 == 0)
                {
                    audioSource.pitch = Random.Range(0.9f, 1.0f);
                    audioSource.Play();
                    // emitter.Play();
                }

                charCount++; 
                yield return new WaitForSeconds(speed);
            }
        }

        public void NextDialogueLine()
        {
            index++;
            if (index < dialogue.Length)
            {
                StartCoroutine(Dialogo());
            }
            else
            {
                evento[0]?.Invoke();
            }
        }

        public void CambiarDialogue(string _dialogue)
        {
            dialogue[index + 1] = _dialogue;
        }

        public void ChangeBlockText()
        {
            if (BlockText == false)
            {
                BlockText = true;
            }
            else
            {
                BlockText = false;
            }
        }
    }

}


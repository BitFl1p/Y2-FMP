using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    bool done = true;
    bool faster = false;
    Animator anim;
    public TMP_Text nameText1;
    public TMP_Text nameText2;
    public TMP_Text dialogueText;
    [HideInInspector] public Queue<Dialogue.Sentence> sentences = new Queue<Dialogue.Sentence>();
    public GameObject charPrefab;
    public GameObject charParent;
    List<GameObject> instantiatedCharacters = new List<GameObject> { };
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!done && Input.anyKeyDown) faster = true;
        if (Input.anyKeyDown && anim.GetBool("Dialoguing") && done) DisplayNextSentence();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        faster = false;
        anim.SetBool("Dialoguing", true);
        sentences.Clear();

        foreach (Dialogue.Sentence line in dialogue.lines) sentences.Enqueue(line);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!done) return;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        var sentence = sentences.Dequeue();
        nameText1.text = sentence.name1;
        nameText2.text = sentence.name2;
        StopAllCoroutines(); 
        SetCharacters(sentence);
        StartCoroutine(TypeSentence(sentence.sentence));
    }
    void SetCharacters(Dialogue.Sentence sentence)
    {
        foreach (var character in sentence.characters)
        {
            if (instantiatedCharacters.Count == 0)
            {
                GameObject prefab = Instantiate(charPrefab, charParent.transform);
                prefab.transform.localPosition = character.position;
                prefab.transform.localScale = character.scale;
                prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                instantiatedCharacters.Add(prefab);
            }
            else
            {
                bool alreadyExists = false;
                foreach (GameObject currentChar in instantiatedCharacters)
                {
                    if (character.sprite == currentChar.GetComponentInChildren<Image>().sprite) 
                    {
                        currentChar.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                        alreadyExists = true; 
                    }
                    if (alreadyExists) break;
                }
                if (!alreadyExists)
                {
                    GameObject prefab = Instantiate(charPrefab, charParent.transform);
                    prefab.transform.localPosition = character.position;
                    prefab.transform.localScale = character.scale;
                    prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                    prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                    instantiatedCharacters.Add(prefab);
                }
            }
        }
    }

    void EndDialogue()
    {
        foreach(GameObject character in instantiatedCharacters)
        {
            character.GetComponentInChildren<Animator>().SetInteger("Animation", -1);
        }
        instantiatedCharacters.Clear();
        done = true;
        anim.SetBool("Dialoguing", false);
    }
    IEnumerator TypeSentence(string sentence)
    {
        done = false;
        faster = false;
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            if (!faster) yield return new WaitForSeconds(0.05f);
            else yield return new WaitForSeconds(0.01f);
        }
        done = true;
    }
}

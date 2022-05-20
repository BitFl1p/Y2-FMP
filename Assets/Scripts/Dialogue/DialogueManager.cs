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
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    [HideInInspector] public Queue<Dialogue.Sentence> sentences = new Queue<Dialogue.Sentence>();
    public GameObject charPrefab;
    public GameObject charParent;
    List<GameObject> instantiatedCharacters = new List<GameObject> { };
    public List<AudioSource> audioSources;
    public AudioSource letterAudio;
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
        nameText.text = sentence.name;
        nameText.transform.parent.localPosition = new Vector2(sentence.namePos, nameText.transform.parent.localPosition.y);
        StopAllCoroutines(); 
        foreach(var audio in audioSources) audio.Stop();
        SetCharacters(sentence);
        for (int i = 0; i < sentence.sounds.Count; i++) if (audioSources[i])
        {
            audioSources[i].clip = sentence.sounds[i];
            audioSources[i].Play();
        }
        StartCoroutine(TypeSentence(sentence.sentence));
    }
    void SetCharacters(Dialogue.Sentence sentence)
    {
        instantiatedCharacters.RemoveAll(item => item == null);
        foreach (var character in sentence.characters)
        {
            if (instantiatedCharacters.Count == 0)
            {
                GameObject prefab = Instantiate(charPrefab, charParent.transform);
                prefab.transform.localPosition = character.position;
                prefab.transform.localEulerAngles = character.rotation;
                prefab.transform.localScale = character.scale;
                prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                prefab.name = character.name;
                prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                prefab.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                prefab.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
                instantiatedCharacters.Add(prefab);
            }
            else
            {
                bool alreadyExists = false;
                foreach (GameObject currentChar in instantiatedCharacters)
                {
                    if (character.name == currentChar.name) 
                    {
                        currentChar.transform.localPosition = character.position;
                        currentChar.transform.localScale = character.scale;
                        currentChar.transform.localEulerAngles = character.rotation;
                        currentChar.GetComponentInChildren<Image>().sprite = character.sprite;
                        currentChar.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                        currentChar.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
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
                    prefab.transform.localEulerAngles = character.rotation;
                    prefab.GetComponentInChildren<Image>().sprite = character.sprite;
                    prefab.name = character.name;
                    prefab.GetComponentInChildren<Animator>().SetInteger("Animation", (int)character.animation);
                    prefab.transform.GetChild(0).GetChild(0).transform.localPosition = character.emotionPosition;
                    prefab.transform.GetChild(0).GetChild(1).transform.localPosition = character.emotionPosition;
                    instantiatedCharacters.Add(prefab);
                }
            }
        }
        foreach(var instance in instantiatedCharacters)
        {
            bool exists = false;
            foreach(var character in sentence.characters)
            {
                if (character.sprite == instance.GetComponentInChildren<Image>().sprite)
                {
                    exists = true;
                }
                if(exists) break;
            }
            if(!exists) instance.GetComponentInChildren<Animator>().SetInteger("Animation", -1);
        }
    }

    void EndDialogue()
    {
        foreach(GameObject character in instantiatedCharacters)
        {
            if(character) character.GetComponentInChildren<Animator>().SetInteger("Animation", -1);
        }
        foreach (var audio in audioSources) audio.Stop();
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
            letterAudio.Play();
            if (!faster) yield return new WaitForSeconds(0.03f);
            else yield return new WaitForSeconds(0.01f);
        }
        done = true;
    }
}

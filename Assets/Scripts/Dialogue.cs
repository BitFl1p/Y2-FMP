using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public class Sentence
    {
        [System.Serializable]
        public class Character
        {
            public string name;
            public Sprite sprite;
            public Transition animation;
        }
        public enum Transition
        {
            idle,
            IdleBack,
            Enter,
            Shake,
            Sad
        }


        public Character character1;
        public Character character2;

        public string sentence;

    }
    public List<Sentence> dialogue;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class Sentence
    {
        [System.Serializable]
        public class Character
        {
            public Sprite sprite;
            public Transition animation;
            public Vector2 position;
            public Vector2 scale = Vector2.one;
            public enum Transition
            {
                Enter,
                Idle,
                IdleBack,
                Shake,
                Sad,
                Angry
            }
        }
        
        public string name1;
        public string name2;
        public bool name1Active;
        [TextArea(3, 10)]
        public string sentence;
        public List<Character> characters;

    }
    public List<Sentence> lines;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Special/Dialogue")]
public class Dialogue : ScriptableObject
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
            public Vector2 position;
            public Vector3 rotation;
            public Vector2 scale = Vector2.one;
            public Vector2 emotionPosition = new Vector2(90, 575);
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
        public string name;
        public float namePos;
        public bool name1Active;
        [TextArea(3, 10)]
        public string sentence; public List<AudioClip> sounds;
        [Header(""), Header(""), Header(""), Header(""), Header("")]
        public List<Character> characters;
        

    }
    public List<Sentence> lines;
}

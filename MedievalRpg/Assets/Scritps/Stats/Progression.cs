using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClasses = null;
        public float GetHealth(CharacterClass characterClass,int level)
        {
            foreach (ProgressionCharacterClass progressionClass in progressionCharacterClasses)
            {
                if (progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }
            return 0f;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}

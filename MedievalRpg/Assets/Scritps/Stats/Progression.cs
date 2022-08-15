using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookUpTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();
            float[] levels = lookUpTable[characterClass][stat];
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level - 1];
        }

        private void BuildLookup()
        {
            if (lookUpTable != null) return;
            lookUpTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach(ProgressionCharacterClass progressionClass in progressionCharacterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookUpTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        public int GetLevels(Stat stat,CharacterClass characterClass)
        {
            float[] levels = lookUpTable[characterClass][stat];
            return levels.Length;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	#region Stat

	[Serializable]
	public class Stat
	{
		public int level;
		public int maxHp;
		public int attack;
		public int totalExp;
	}

	[Serializable]
	public class StatData : ILoader<int, Stat>
	{
		public List<Stat> stats = new List<Stat>();

		public Dictionary<int, Stat> MakeDict()
		{
			Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
			foreach (Stat stat in stats)
				dict.Add(stat.level, stat);
			return dict;
		}
	}

	#endregion

	#region Weapon

	[Serializable]
	public class WeaponData
    {
        public int weaponID;
        public string weaponName;
        public List<WeaponLevelData> weaponLevelData = new List<WeaponLevelData>();

	}

	[Serializable]
	public class WeaponLevelData
	{
		public int level;
		public int damage;
		public float movSpeed;
		public float Force;
		public float cooldown;
		public int createPerCount;
	}

	[Serializable]
	public class WeaponDataLoader : ILoader<int, WeaponData>
	{
		public List<WeaponData> weapons = new List<WeaponData>();
        public Dictionary<int, WeaponData> MakeDict()
        {
            Dictionary<int, WeaponData> dict = new Dictionary<int, WeaponData>();
            foreach (WeaponData weapon in weapons)
                dict.Add(weapon.weaponID, weapon);
            return dict;
        }
    }
	#endregion

}
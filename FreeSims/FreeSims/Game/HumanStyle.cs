using System;
﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Technochips.FreeSims.Game
{
	public class HumanStyle
	{
		public string name;
        public bool female;
        public Color pants;
        public Color hair;
        public int hairStyle;
        public Color eyes;
        public Color shirt;
        public Color shoes;
        public Color skin;
		public float walkSpeed;
		public HumanStyle(string name, bool female, Color pants, Color hair, int hairStyle, Color eyes, Color shirt, Color shoes, Color skin, float walkSpeed)
		{
			this.name = name;
			this.female = female;
			this.pants = pants;
			this.hair = hair;
			this.hairStyle = hairStyle;
			this.eyes = eyes;
			this.shirt = shirt;
			this.shoes = shoes;
			this.skin = skin;
			this.walkSpeed = walkSpeed;
		}
		public static string[] GetName(HumanStyle[] style)
		{
			List<string> r = new List<string>();
			foreach(HumanStyle s in style)
				r.Add(s.name);
			return r.ToArray();
		}
		public static bool[] GetFemale(HumanStyle[] style)
		{
			List<bool> r = new List<bool>();
			foreach(HumanStyle s in style)
				r.Add(s.female);
			return r.ToArray();
		}
		public static Color[] GetPants(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.pants);
			return r.ToArray();
		}
		public static Color[] GetHair(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.hair);
			return r.ToArray();
		}
		public static int[] GetHairStyle(HumanStyle[] style)
		{
			List<int> r = new List<int>();
			foreach(HumanStyle s in style)
				r.Add(s.hairStyle);
			return r.ToArray();
		}
		public static Color[] GetEyes(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.eyes);
			return r.ToArray();
		}
		public static Color[] GetShirt(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.shirt);
			return r.ToArray();
		}
		public static Color[] GetShoes(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.shoes);
			return r.ToArray();
		}
		public static Color[] GetSkin(HumanStyle[] style)
		{
			List<Color> r = new List<Color>();
			foreach(HumanStyle s in style)
				r.Add(s.skin);
			return r.ToArray();
		}
		public static float[] GetWalkSpeed(HumanStyle[] style)
		{
			List<float> r = new List<float>();
			foreach(HumanStyle s in style)
				r.Add(s.walkSpeed);
			return r.ToArray();
		}
	}
}

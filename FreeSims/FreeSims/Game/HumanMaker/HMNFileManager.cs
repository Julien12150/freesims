using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace Technochips.FreeSims.Game.HumanMaker
{
    public class HMNFileManager
    {
        private static string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}humans.hmn";
        public static bool Read(out string[] names, out bool[] female, out Color[] eyes, out Color[] hair, out int[] hairStyle, out Color[] pants, out Color[] shirt, out Color[] shoes, out Color[] skin, out float[] walkSpeed)
        {
			if (Read(filePath, out female, out names, out eyes, out hair, out hairStyle, out pants, out shirt, out shoes, out skin, out walkSpeed))
                return true;
            else
                return false;
        }
        public static bool Read(string path, out bool[] female, out string[] names, out Color[] eyes, out Color[] hair, out int[] hairStyle, out Color[] pants, out Color[] shirt, out Color[] shoes, out Color[] skin, out float[] walkSpeed)
        {
            BinaryReader file = null;
            try
            {
                file = new BinaryReader(File.Open(path, FileMode.Open));
                bool isName = true;

                int num = 0;

                List<string> namesList = new List<string>();
                List<bool> femaleList = new List<bool>();
                List<Color> eyesList = new List<Color>();
                List<Color> hairList = new List<Color>();
                List<int> hairStyleList = new List<int>();
                List<Color> pantsList = new List<Color>();
                List<Color> shirtList = new List<Color>();
                List<Color> shoesList = new List<Color>();
                List<Color> skinList = new List<Color>();
				List<float> walkSpeedList = new List<float>();

                while (true)
                {
                    try
                    {
                        byte b = file.ReadByte();
                        if (isName)
                        {
                            if (b != 0x00)
                            {
                                try
                                {
                                    namesList[num] += Convert.ToChar(b);
                                }
                                catch
                                {
                                    namesList.Add(Convert.ToChar(b).ToString());
                                }
                            }
                            else
                                isName = false;
                        }
                        else
                        {
                            byte b1 = b;
							if(b1 == 0x00)
								femaleList.Add(false);
							else
								femaleList.Add(true);
							hairStyleList.Add(file.ReadInt32());
                            b1 = file.ReadByte();
                            byte b2 = file.ReadByte();
                            byte b3 = file.ReadByte();
                            pantsList.Add(new Color(b1, b2, b3));
                            b1 = file.ReadByte();
                            b2 = file.ReadByte();
                            b3 = file.ReadByte();
                            hairList.Add(new Color(b1, b2, b3));
                            b1 = file.ReadByte();
                            b2 = file.ReadByte();
                            b3 = file.ReadByte();
                            eyesList.Add(new Color(b1, b2, b3));
                            b1 = file.ReadByte();
                            b2 = file.ReadByte();
                            b3 = file.ReadByte();
                            shirtList.Add(new Color(b1, b2, b3));
                            b1 = file.ReadByte();
                            b2 = file.ReadByte();
                            b3 = file.ReadByte();
                            shoesList.Add(new Color(b1, b2, b3));
                            b1 = file.ReadByte();
                            b2 = file.ReadByte();
                            b3 = file.ReadByte();
                            skinList.Add(new Color(b1, b2, b3));
							float f = file.ReadSingle();
							walkSpeedList.Add(f);

                            if (file.PeekChar() == 0x00)
                                break;
                            else
                                isName = true;

                            num++;
                        }
                    }
                    catch (EndOfStreamException)
                    { break; }
                }
                names = namesList.ToArray();
                eyes = eyesList.ToArray();
                hair = hairList.ToArray();
                hairStyle = hairStyleList.ToArray();
                pants = pantsList.ToArray();
                shirt = shirtList.ToArray();
                shoes = shoesList.ToArray();
                skin = skinList.ToArray();
                female = femaleList.ToArray();
				walkSpeed = walkSpeedList.ToArray();

                file.Close();

                return true;
            }
            catch (DirectoryNotFoundException)
            {
                //Console.WriteLine("reading the file failed or something");
            }
            catch (FileNotFoundException)
            {
                //Console.WriteLine("reading the file failed or something");
            }
            catch
            {
                //Console.WriteLine("reading the file failed or something");
                file.Close();
            }

            names = null;
            female = null;
            eyes = null;
            hair = null;
            hairStyle = null;
            pants = null;
            shirt = null;
            shoes = null;
            skin = null;
			walkSpeed = null;

            return false;
        }
        public static void Write(string[] names, bool[] female, Color[] eyes, Color[] hair, int[] hairStyle, Color[] pants, Color[] shirt, Color[] shoes, Color[] skin, float[] walkSpeed)
        {
			Write(filePath, names, female, eyes, hair, hairStyle, pants, shirt, shoes, skin, walkSpeed);
        }
        public static void Write(string path,  string[] names, bool[] female, Color[] eyes, Color[] hair, int[] hairStyle, Color[] pants, Color[] shirt, Color[] shoes, Color[] skin, float[] walkSpeed)
        {
            //List<byte> bytelist = new List<byte>();
			BinaryWriter file = new BinaryWriter(File.Open(path, FileMode.Create));
            for(int i = 0; i < names.Length; i++)
            {
                foreach(byte bc in names[i])
                {
                    file.Write(bc);
                    //Console.WriteLine(Convert.ToChar(bc));
                }
                file.Write((byte)0x00);
				//Console.WriteLine((byte)0x00);
				file.Write(female[i]);
                /*if (female[i])
                {
                    file.Write((byte)0x01);
                    //Console.WriteLine((byte)0x01);
                }
                else
                {
                    file.Write((byte)0x00);
                    //Console.WriteLine((byte)0x00);
                }*/
                file.Write(hairStyle[i]);
                //Console.WriteLine((byte)hairStyle[i]);
                file.Write(pants[i].R);
                //Console.WriteLine(pants[i].R);
                file.Write(pants[i].G);
                //Console.WriteLine(pants[i].G);
                file.Write(pants[i].B);
                //Console.WriteLine(pants[i].B);
                file.Write(hair[i].R);
                //Console.WriteLine(hair[i].R);
                file.Write(hair[i].G);
                //Console.WriteLine(hair[i].G);
                file.Write(hair[i].B);
                //Console.WriteLine(hair[i].B);
                file.Write(eyes[i].R);
                //Console.WriteLine(eyes[i].R);
                file.Write(eyes[i].G);
                //Console.WriteLine(eyes[i].G);
                file.Write(eyes[i].B);
                //Console.WriteLine(eyes[i].B);
                file.Write(shirt[i].R);
                //Console.WriteLine(shirt[i].R);
                file.Write(shirt[i].G);
                //Console.WriteLine(shirt[i].G);
                file.Write(shirt[i].B);
                //Console.WriteLine(shirt[i].B);
                file.Write(shoes[i].R);
                //Console.WriteLine(shoes[i].R);
                file.Write(shoes[i].G);
                //Console.WriteLine(shoes[i].G);
                file.Write(shoes[i].B);
                //Console.WriteLine(shoes[i].B);
                file.Write(skin[i].R);
                //Console.WriteLine(skin[i].R);
                file.Write(skin[i].G);
                //Console.WriteLine(skin[i].G);
                file.Write(skin[i].B);
				file.Write(walkSpeed[i]);
            }
            //Console.WriteLine("---");
            //file.Write(bytelist.ToArray());
            file.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Technochips.FreeSims.Game;

namespace Technochips.FreeSims.Game.HumanMaker
{
    public class HMNFileManager
    {
        private static string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar}Technochips{Path.DirectorySeparatorChar}FreeSims{Path.DirectorySeparatorChar}humans.hmn";
        public static bool Read(out HumanStyle[] style)
        {
			if (Read(filePath, out style))
                return true;
            else
                return false;
        }
        public static bool Read(string path, out HumanStyle[] style)
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
				List<HumanStyle> styleList = new List<HumanStyle>();

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
                for(int i = 0; i < namesList.Count; i++)
                {
                	styleList.Add(new HumanStyle(namesList[i], femaleList[i], pantsList[i], hairList[i], hairStyleList[i], eyesList[i], shirtList[i], shoesList[i], skinList[i], walkSpeedList[i]));
                }
                style = styleList.ToArray();

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

            style = null;

            return false;
        }
        public static void Write(HumanStyle[] style)
        {
			Write(filePath, style);
        }
        public static void Write(string path, HumanStyle[] style)
        {
            //List<byte> bytelist = new List<byte>();
			BinaryWriter file = new BinaryWriter(File.Open(path, FileMode.Create));
            for(int i = 0; i < style.Length; i++)
            {
                foreach(byte bc in style[i].name)
                {
                    file.Write(bc);
                    //Console.WriteLine(Convert.ToChar(bc));
                }
                file.Write((byte)0x00);
				//Console.WriteLine((byte)0x00);
				file.Write(style[i].female);
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
                file.Write(style[i].hairStyle);
                //Console.WriteLine((byte)hairStyle[i]);
                file.Write(style[i].pants.R);
                //Console.WriteLine(pants[i].R);
                file.Write(style[i].pants.G);
                //Console.WriteLine(pants[i].G);
                file.Write(style[i].pants.B);
                //Console.WriteLine(pants[i].B);
                file.Write(style[i].hair.R);
                //Console.WriteLine(hair[i].R);
                file.Write(style[i].hair.G);
                //Console.WriteLine(hair[i].G);
                file.Write(style[i].hair.B);
                //Console.WriteLine(hair[i].B);
                file.Write(style[i].eyes.R);
                //Console.WriteLine(eyes[i].R);
                file.Write(style[i].eyes.G);
                //Console.WriteLine(eyes[i].G);
                file.Write(style[i].eyes.B);
                //Console.WriteLine(eyes[i].B);
                file.Write(style[i].shirt.R);
                //Console.WriteLine(shirt[i].R);
                file.Write(style[i].shirt.G);
                //Console.WriteLine(shirt[i].G);
                file.Write(style[i].shirt.B);
                //Console.WriteLine(shirt[i].B);
                file.Write(style[i].shoes.R);
                //Console.WriteLine(shoes[i].R);
                file.Write(style[i].shoes.G);
                //Console.WriteLine(shoes[i].G);
                file.Write(style[i].shoes.B);
                //Console.WriteLine(shoes[i].B);
                file.Write(style[i].skin.R);
                //Console.WriteLine(skin[i].R);
                file.Write(style[i].skin.G);
                //Console.WriteLine(skin[i].G);
                file.Write(style[i].skin.B);
				file.Write(style[i].walkSpeed);
            }
            //Console.WriteLine("---");
            //file.Write(bytelist.ToArray());
            file.Close();
        }
    }
}

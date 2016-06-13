using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace Julien12150.FreeSims.Game.HumanMaker
{
    public class HMNFileManager
    {
        private static string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/Julien12150/FreeSims/humans.hmn";
        public static bool Read(out string[] names, out Color[] eyes, out Color[] hair, out Color[] pants, out Color[] shirt, out Color[] shoes, out Color[] skin)
        {
            if (Read(filePath, out names, out eyes, out hair, out pants, out shirt, out shoes, out skin))
                return true;
            else
                return false;
        }
        public static bool Read(string path, out string[] names, out Color[] eyes, out Color[] hair, out Color[] pants, out Color[] shirt, out Color[] shoes, out Color[] skin)
        {
            BinaryReader file = null;
            try
            {
                file = new BinaryReader(File.Open(path, FileMode.Open));
                bool isName = true;

                    int num = 0;

                    List<string> namesList = new List<string>();
                    List<Color> eyesList = new List<Color>();
                    List<Color> hairList = new List<Color>();
                    List<Color> pantsList = new List<Color>();
                    List<Color> shirtList = new List<Color>();
                    List<Color> shoesList = new List<Color>();
                    List<Color> skinList = new List<Color>();

                    while (true)
                    {
                        try
                        {
                            byte b = file.ReadByte();
                            if (isName)
                            {
                                if (b != 0x01)
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
                    pants = pantsList.ToArray();
                    shirt = shirtList.ToArray();
                    shoes = shoesList.ToArray();
                    skin = skinList.ToArray();

                    file.Close();

                    return true;
            }
            catch (FileNotFoundException e) {
                Console.WriteLine("reading the file failed or something");
            }
            catch
            {
                Console.WriteLine("reading the file failed or something");
                file.Close();
            }

            names = null;
            eyes = null;
            hair = null;
            pants = null;
            shirt = null;
            shoes = null;
            skin = null;

            return false;
        }
        public static void Write(string[] names, Color[] eyes, Color[] hair, Color[] pants, Color[] shirt, Color[] shoes, Color[] skin)
        {
            Write(filePath, names, eyes, hair, pants, shirt, shoes, skin);
        }
        public static void Write(string path, string[] names, Color[] eyes, Color[] hair, Color[] pants, Color[] shirt, Color[] shoes, Color[] skin)
        {
            BinaryWriter file = new BinaryWriter(File.Open(path, FileMode.Create));

            for(int i = 0; i < names.Length; i++)
            {
                foreach(byte bc in names[i])
                {
                    file.Write(bc);
                }
                file.Write((byte)0x01);
                file.Write(pants[i].R);
                file.Write(pants[i].G);
                file.Write(pants[i].B);
                file.Write(hair[i].R);
                file.Write(hair[i].G);
                file.Write(hair[i].B);
                file.Write(eyes[i].R);
                file.Write(eyes[i].G);
                file.Write(eyes[i].B);
                file.Write(shirt[i].R);
                file.Write(shirt[i].G);
                file.Write(shirt[i].B);
                file.Write(shoes[i].R);
                file.Write(shoes[i].G);
                file.Write(shoes[i].B);
                file.Write(skin[i].R);
                file.Write(skin[i].G);
                file.Write(skin[i].B);
            }
            file.Write(0x00);
            file.Close();
        }
    }
}

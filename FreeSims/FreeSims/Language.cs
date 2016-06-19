using System;
using System.IO;

namespace Julien12150.FreeSims
{
    public class Language
    {
        private const string defaultLanguage = "en_US";
        public string language;

        StreamReader file;
        StreamReader defaultFile;

        public string lang;
        public string menu_play;
        public string menu_option;
        public string menu_humanmaker;
        public string menu_quit;
        public string option_chooselang;
        public string option_restartnote;

        public Language(string language)
        {
            if (File.Exists($"Language\\{language}.lng"))
                this.language = language;
            else
                this.language = defaultLanguage;

            file = new StreamReader($"Language\\{this.language}.lng");
            defaultFile = new StreamReader($"Language\\{defaultLanguage}.lng");

            string[] sFile = file.ReadToEnd().Split(Environment.NewLine.ToCharArray());
            if (language != "en_US")
            {
                string[] sDefaultFile = defaultFile.ReadToEnd().Split(Environment.NewLine.ToCharArray());
                GetStringVar(sDefaultFile);
            }
            GetStringVar(sFile);
        }
        public void ChangeLanguage(string language)
        {
            if (File.Exists($"Language\\{language}.lng"))
            {
                file.Close();
                this.language = language;
                file = new StreamReader($"Language\\{this.language}.lng");

                string[] sFile = file.ReadToEnd().Split(Environment.NewLine.ToCharArray());
                if (language != "en_US")
                {
                    string[] sDefaultFile = defaultFile.ReadToEnd().Split(Environment.NewLine.ToCharArray());
                    GetStringVar(sDefaultFile);
                }
                GetStringVar(sFile);
            }
        }
        public string GetLang(string language)
        {
            StreamReader file = new StreamReader($"Language\\{language}.lng");
            foreach (string s in file.ReadToEnd().Split(Environment.NewLine.ToCharArray()))
            {
                if (s.Split('=')[0] == "lang")
                {
                    file.Close();
                    return s.Split('=')[1];
                }
            }
            file.Close();
            return null;
        }
        private void GetStringVar(string[] file)
        {
            foreach (string s in file)
            {
                if (s.Split('=')[0] == "lang") lang = s.Split('=')[1];
                else if (s.Split('=')[0] == "menu_play") menu_play = s.Split('=')[1];
                else if (s.Split('=')[0] == "menu_option") menu_option = s.Split('=')[1];
                else if (s.Split('=')[0] == "menu_humanmaker") menu_humanmaker = s.Split('=')[1];
                else if (s.Split('=')[0] == "menu_quit") menu_quit = s.Split('=')[1];
                else if (s.Split('=')[0] == "option_chooselang") option_chooselang = s.Split('=')[1];
                else if (s.Split('=')[0] == "option_restartnote") option_restartnote = s.Split('=')[1];
            }
        }
    }
}

using SoftwareDesignPatternsProject.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject
{
    public sealed class LanguageSingleton 
    {
        private static LanguageSingleton instance = null;
        private static readonly object padlock = new object();
        private static string language = "en"; 

        LanguageSingleton()
        {
        }

        public static LanguageSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)   
                    {
                        instance = new LanguageSingleton();
                    }
                    return instance;
                }
            }
        }

        public void ToBulgarian()
        {
            language = "bg"; 
        }

        public void ToEnglish()
        {
            language = "en"; 
        }

        public string GetTranslation(string name)
        {
            if (language == "en")
            {
                return English.ResourceManager.GetString(name); 
            }
            else if (language == "bg")
            {
                return Bulgarian.ResourceManager.GetString(name);
            } 
            else
            {
                return "Error..."; 
            }
        }

    }
}

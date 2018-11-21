using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadEnglish
{
    public class Monitor
    {
        public int textRate;
        public int semestrRate;
        public int wordsRate;

        public Monitor(int text, int semestr)
        {
            textRate = text;
            semestrRate = semestr;
            wordsRate = text / 200;
        }

        public Monitor()
        {
            textRate = 5000;
            semestrRate = 20000;
            wordsRate = 25;
        }

        public int countSymbol(string text)
        {
            int countNotSeparatorSymbol = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsSeparator(text[i]) == false && text[i] != '\n')
                {
                    countNotSeparatorSymbol++;
                }
            }

            return countNotSeparatorSymbol;
        }
        public int percentText(string text)
        {
            return (countSymbol(text) * 100) / textRate;
        }

        public int percentSemester(string directory)
        {
            int result = 0;

            return result;
        }

        public int percentWord(int countWords)
        {
            return (countWords * 100) / wordsRate;
        }

        public void save(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file, Encoding.Unicode);

            writer.WriteLine(textRate);
            writer.WriteLine(wordsRate);

            writer.Close();
            file.Close();
        }

        public void open(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file, Encoding.Unicode);

            textRate = int.Parse(reader.ReadLine());
            wordsRate = int.Parse(reader.ReadLine());

            reader.Close();
            file.Close();
        }
    }
}

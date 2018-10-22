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
        public int normText;
        public int normSemestr;
        public int normWords;

        public Monitor(int text, int semestr)
        {
            normText = text;
            normSemestr = semestr;
            normWords = text / 200;
        }

        public Monitor()
        {
            normText = 5000;
            normSemestr = 20000;
            normWords = 25;
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
            return (countSymbol(text) * 100) / normText;
        }

        public int percentSemester(string directory)
        {
            int result = 0;

            return result;
        }

        public int percentWord(int countWords)
        {
            return (countWords * 100) / normWords;
        }

        public void save(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file, Encoding.Unicode);

            writer.WriteLine(normText);
            writer.WriteLine(normWords);

            writer.Close();
            file.Close();
        }

        public void open(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file, Encoding.Unicode);

            normText = int.Parse(reader.ReadLine());
            normWords = int.Parse(reader.ReadLine());

            reader.Close();
            file.Close();
        }
    }
}

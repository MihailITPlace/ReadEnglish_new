using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ReadEnglish
{
    public class Dictionary
    {
        public struct dictionaryEntry
        {
            public string word;
            public string transcriptionAmerican;
            public string transcriptionBritish;
            public string translation;
        }

        List<dictionaryEntry> entries = new List<dictionaryEntry>();

        string parserHtml(string pattern, string htmlPage)
        {
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex regex = new Regex(pattern, options);

            Match match = regex.Match(htmlPage);

            string result = "";
            while (match.Success)
            {
                result += match.Groups["val"].Value;
                match = match.NextMatch();
            }

            if (result == "")
            {
                return "not_founded";
            }

            return result;
        }

        public int entryCount()
        {
            return entries.Count;
        }

        public string entryGetString(int i)
        {
            return entries[i].word + " " + entries[i].transcriptionBritish + " " + entries[i].translation;
        }

        public dictionaryEntry entryGet(int i)
        {
            return entries[i];
        }

        public void entryAdd(string word)
        {
            word = word.Trim();
            word = word.ToLower();

            string url = @"http://wooordhunt.ru/word/" + word;
            string htmlPage = "";
            try
            {
                System.Net.WebRequest reqGET = System.Net.WebRequest.Create(url);
                System.Net.WebResponse resp = reqGET.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                htmlPage = sr.ReadToEnd();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка подключения");
                return;
            }


            dictionaryEntry newEntry = new dictionaryEntry();

            newEntry.word = word;
            newEntry.transcriptionAmerican = parserHtml(@"<span title=""американская транскрипция слова " + word + '"' + @" class=""transcription"">(?<val>.*?)<\/span>", htmlPage).Trim();
            newEntry.transcriptionBritish = parserHtml(@"<span title=""британская транскрипция слова " + word + '"' + @" class=""transcription"">(?<val>.*?)<\/span>", htmlPage).Trim();
            newEntry.translation = parserHtml(@"<span class=""t_inline_en"">(?<val>.*?)<\/span>", htmlPage).Trim();

            entries.Add(newEntry);
        }

        public bool isEmpty()
        {
            if (entries.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void entryDelete(int i)
        {
            entries.RemoveAt(i);
        }

        public void save(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file, Encoding.Unicode);

            writer.WriteLine(entries.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                writer.WriteLine(entries[i].word);
                writer.WriteLine(entries[i].transcriptionAmerican);
                writer.WriteLine(entries[i].transcriptionBritish);
                writer.WriteLine(entries[i].translation);
            }

            writer.Close();
            file.Close();
        }

        public void open(string fileName)
        {
            entries.Clear();
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file, Encoding.Unicode);

            int count = int.Parse(reader.ReadLine());
            for (int i = 0; i < count; i++)
            {
                dictionaryEntry tmp;
                tmp.word = reader.ReadLine();
                tmp.transcriptionAmerican = reader.ReadLine();
                tmp.transcriptionBritish = reader.ReadLine();
                tmp.translation = reader.ReadLine();

                entries.Add(tmp);
            }

            reader.Close();
            file.Close();
        }
    }
}

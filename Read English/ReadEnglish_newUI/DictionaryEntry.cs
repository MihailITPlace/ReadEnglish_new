using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadEnglish_newUI
{
    class DictionaryEntry
    {
        public string Word { get; set; }
        public string TranscriptionAmerican { get; set; }
        public string TranscriptionBritish { get; set; }
        public string Translation { get; set; }

        public DictionaryEntry()
        {
            Word = string.Empty;
            TranscriptionAmerican = string.Empty;
            TranscriptionBritish = string.Empty;
            Translation = string.Empty;
        }

        public DictionaryEntry(string word, string translation, string transcriptionBritish, string transcriptionAmerican = "")
        {
            Word = word;
            Translation = translation;
            TranscriptionBritish = transcriptionBritish;
            TranscriptionAmerican = transcriptionAmerican;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadEnglish_newUI
{
    public class StatisticsMonitor
    {
        public event UpdateValueMonitor UpdateMonitors;

        private int requiredNumberOfCharacters;

        private int countOfCharacters;

        private int countWords;

        public void UpdateCountOfCharacters(int value)
        {
            countOfCharacters = value;
            UpdateMonitors.Invoke(countOfCharacters, countWords, requiredNumberOfCharacters);
        }

        public int PercentOfCharacters
        {
            get
            {
                return (countOfCharacters * 100) / requiredNumberOfCharacters;
            }
        }

        //public int RequiredNumberOfWords
        //{
        //    get
        //    {
        //        return requiredNumberOfCharacters / 200;
        //    }
        //}

        public int RequiredNumberOfCharacters
        {
            get
            {
                return requiredNumberOfCharacters;
            }

            set
            {
                if (value <= 0)
                {
                    requiredNumberOfCharacters = 5000;

                }
                requiredNumberOfCharacters = value;
                UpdateMonitors.Invoke(countOfCharacters, countWords, requiredNumberOfCharacters);
            }
        }

        public StatisticsMonitor()
        {
            requiredNumberOfCharacters = 5000;
            countWords = 0;
        }            

    }
}

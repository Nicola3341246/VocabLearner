using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletRipOff
{
    internal class VociWord
    {
        string _nonTranslated;
        string _translated;
        bool _known = false;

        public string NonTranslated { get { return _nonTranslated; } set { _nonTranslated = value; } }

        public string Translated { get { return _translated; } set { _translated = value; } }

        public bool Known { get { return _known; } set { _known = value; } }


        public VociWord(string nonTrans, string trans)
        {
            _nonTranslated = nonTrans;
            _translated = trans;
        }
    }
}

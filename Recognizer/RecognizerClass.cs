using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognizer {
    public class RecognizerClass {

        private string _input;
        private int pos = 0;
        private Stack<object> stack = new Stack<object>();

        public RecognizerClass(string input) {
            _input = input;
        }

        //Lexer
        private void SkipWhitespace() {
            if (pos != _input.Length) {
                while (_input.ElementAt(pos) == ' ' || _input.ElementAt(pos) == '\n')
                    ++pos;
            }
        }

        private bool ParseStringLit() {
            if(_input.ElementAt(pos) != '"') {
                return false;
            }
            int last = _input.Substring(pos + 1).IndexOf('"') - 1;
            if(last <0 ) { 
                return false; 
            }
            stack.Push(_input.Substring(pos + 1, last + 1));
            pos += last + 3;
            SkipWhitespace();
            return true;
        }

        private bool ParseNumber() {
            char[] numbers = new char[]{'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
            bool hasNumber = false;
            string finalNumber = "";
            for(int i = 0; i < numbers.Length; i++) {
                if(_input.ElementAt(pos) == numbers[i]) {
                    hasNumber = true;
                    finalNumber += _input.ElementAt(pos);
                    pos++;
                }
            }
            if(hasNumber ) {
                stack.Push(Convert.ToInt32(finalNumber));
            }
            return hasNumber;
        }

        private bool ParseChar(char c) {
            if(!(_input.ElementAt(pos) == c)) {
                return false;
            }
            pos++;
            SkipWhitespace();
            return true;
        }

        //Parser

        public bool ParseValue() {
            if (!ParseStringLit()) {
                if (!ParseNumber()) {
                    if(!ParseObject()) {
                        if (!ParseArray()) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool ParseObject() {
            int pos0 = pos;
            bool success = ParseChar('{') && ParsePairs() && ParseChar('}');
            if (!success) pos = pos0;
            return success;
        }

        private bool ParsePairs() {
            if (ParsePair()) {
                ParsePairTails();
            }
            return true;
        }

        private bool ParsePair() {
            int pos0 = pos;
            bool success = ParseStringLit() && ParseChar(':') && ParseValue();
            if (!success)
                pos = pos0;
            return success;
        }
        private bool ParsePairTails() {
            while (true) {
                int pos0 = pos;
                bool success = ParseChar(',') && ParsePair();
                if (!success) {
                    pos = pos0;
                    return true;
                }
            }
        }
        private bool ParseArray() {
            int pos0 = pos;
            bool success = ParseChar('[') && ParseValues() && ParseChar(']');
            if (!success)
                pos = pos0;
            return success;
        }
        private bool ParseValues() {
            if (ParseValue()) {
                ParseValueTails();
            }
            return true;
        }
        private bool ParseValueTails() {
            while (true) {
                int pos0 = pos;
                bool success = ParseChar(',') && ParseValue();
                if (!success) {
                    pos = pos0;
                    return true;
                }
            }
        }
    }
}

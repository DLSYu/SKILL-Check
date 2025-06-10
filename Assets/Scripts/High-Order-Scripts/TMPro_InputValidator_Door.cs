
using UnityEngine;
using System;


namespace TMPro
{
    [Serializable]
    [CreateAssetMenu(fileName = "InputValidator - Alphanumeric with Single Whitespace Separators", menuName = "TextMeshPro/Input Validators/Alphanumeric with Single Whitespace Separators", order = 100)]
    public class TMPro_InputValidator_Door : TMP_InputValidator
    {

        public override char Validate(ref string text, ref int pos, char ch)
        {

            if (char.IsLetterOrDigit(ch) || ch == '\'' || (char.IsWhiteSpace(ch) && !PreviousCharacterIsWhiteSpace(text, pos) && !NextCharacterIsWhiteSpace(text, pos)))
            {
#if UNITY_EDITOR_WIN
                text = text.Insert(pos, ch.ToString());
                pos++;
#endif
                return ch;
            }

            return '\0';
        }

        private bool PreviousCharacterIsWhiteSpace(string text, int pos)
        {
            return pos == 0 || pos > 0 && char.IsWhiteSpace(text[pos - 1]);
        }

        private bool NextCharacterIsWhiteSpace(string text, int pos)
        {
            return pos < text.Length && char.IsWhiteSpace(text[pos]);
        }
    }
}


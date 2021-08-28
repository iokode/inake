using System.Linq;
using IOKode.Inake.RandomGenerators;

namespace IOKode.Inake
{
    public class WordGenerator
    {
        private readonly char[] _Vowels = {'a', 'e', 'i', 'o', 'u'};

        private readonly char[] _IntransitiveConsonants =
            {'v', 'c', 'd', 'f', 'g', 'j', 'k', 'm', 'n', 'ñ', 'p', 's', 't', 'v', 'x'};

        private readonly char[] _TransitiveConsonants = {'l', 'r'};
        private readonly char[] _BeforeTransitiveConsonants = {'b', 'p', 'c', 'g'};
        private readonly char[] _EndConsonants = {'l', 'n', 'r', 's', 'z'};

        private readonly IRandomGenerator _RandomGenerator;

        public WordGenerator(IRandomGenerator randomGenerator)
        {
            _RandomGenerator = randomGenerator;
        }

        /// <summary>
        /// Generate random pronounceable word based on spanish rules.
        /// </summary>
        /// <param name="letters">Max number of letters. The generated word length could be lower than the supplied value.</param>
        /// <returns>The generated word.</returns>
        public string GenerateWord(int letters)
        {
            int vowelCounter = 0;
            int consonantCounter = 0;

            string word = string.Empty;

            for (int cursor = 0; cursor < letters - 2; cursor++)
            {
                switch (_Random(3))
                {
                    case 0:
                        if (vowelCounter >= 2)
                        {
                            cursor--;
                            break;
                        }

                        char vowel = _TakeLetter(_Vowels);

                        if (word.EndsWith(vowel))
                        {
                            cursor--;
                            break;
                        }

                        word += vowel;
                        vowelCounter++;
                        consonantCounter = 0;

                        break;

                    case 1:
                        if (consonantCounter != 0)
                        {
                            cursor--;
                            break;
                        }

                        word += _TakeLetter(_IntransitiveConsonants);
                        vowelCounter = 0;
                        consonantCounter++;

                        break;

                    case 2:
                        if (_BeforeTransitiveConsonants.Any(consonant => word.EndsWith(consonant)))
                        {
                            cursor--;
                            break;
                        }

                        word += _TakeLetter(_TransitiveConsonants);
                        vowelCounter = 0;
                        consonantCounter++;

                        break;

                    case 3:
                        switch (_Random(1))
                        {
                            case 0:
                                if (word.EndsWith('c'))
                                {
                                    cursor--;
                                    break;
                                }

                                word += 'h';
                                consonantCounter++;
                                vowelCounter = 0;

                                break;

                            case 1:
                                if (letters - word.Length < 3 || word.EndsWith("que") || word.EndsWith("qui"))
                                {
                                    cursor--;
                                    break;
                                }

                                word += "qu";
                                word += _TakeLetter("ei");

                                consonantCounter = 0;
                                vowelCounter = 2;
                                cursor += 2;

                                break;
                        }

                        break;
                }
            }

            if (consonantCounter > 0)
            {
                word = _AddEndLetters(word);
            }

            return word;
        }

        private string _AddEndLetters(string word)
        {
            word += _TakeLetter("ae");

            if (_Random(4) == 0)
            {
                word += _TakeLetter(_EndConsonants);
            }

            return word;
        }

        private int _Random(int min, int max) => _RandomGenerator.Next(min, max);
        private int _Random(int max) => _RandomGenerator.Next(max);

        private char _TakeLetter(params char[] letters) => letters[_Random(letters.Length - 1)];
        private char _TakeLetter(string letters) => _TakeLetter(letters.ToCharArray());
    }
}
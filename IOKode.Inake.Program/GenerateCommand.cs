using System;
using System.Collections.Generic;
using System.IO;
using ConsoleAppFramework;

namespace IOKode.Inake.Program
{
    internal class GenerateCommand : ConsoleAppBase
    {
        private readonly WordGenerator _Generator;

        public GenerateCommand(WordGenerator generator)
        {
            _Generator = generator;
        }

        public void Run(
            [Option("w", "Number of generated words.")]
            int words = 1,
            [Option("l", "Number of letters per word.")]
            int letters = 8,
            [Option("f", "If set, output to file, otherwise, output to console.")]
            string fileName = null)
        {
            var wordsList = _GenerateWords(words, letters);

            if (fileName is null)
            {
                _PrintToConsole(wordsList);
            }
            else
            {
                _WriteToFile(fileName, wordsList);
            }
        }

        private static void _WriteToFile(string fileName, List<string> wordsList)
        {
            try
            {
                File.WriteAllLines(fileName, wordsList);
            }
            catch (Exception ex) when (
                ex is DirectoryNotFoundException
                    or ArgumentException
                    or PathTooLongException
                    or NotSupportedException)
            {
                Console.Error.WriteLine(
                    "Path is invalid (for example, it is on an unmapped drive), is a zero-length string, " +
                    "contains only white space, or contains one or more invalid characters or exceeds the " +
                    "system-defined maximum length.");
            }
            catch (IOException)
            {
                Console.Error.WriteLine("An I/O error occurred while opening the file");
            }
            catch (UnauthorizedAccessException)
            {
                Console.Error.WriteLine("Path specified a file that is read-only," +
                                        " path specified a file that is hidden," +
                                        " the caller does not have the required permission" +
                                        " or path is a directory.");
            }
        }

        private static void _PrintToConsole(List<string> wordsList)
        {
            wordsList.ForEach(Console.WriteLine);
        }

        private List<string> _GenerateWords(int words, int letters)
        {
            List<string> wordsList = new();
            for (int i = 0; i < words; i++)
            {
                var word = _Generator.GenerateWord(letters);
                wordsList.Add(word);
            }

            return wordsList;
        }
    }
}
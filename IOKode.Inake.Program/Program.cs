using System;
using IOKode.Inake;
using IOKode.Inake.RandomGenerators;

var wordGenerator = new WordGenerator(new SystemRandomGenerator(new Random()));

for (int i = 0; i < 10; i++)
{
    var word = wordGenerator.GenerateWord(8);
    Console.WriteLine(word);
}


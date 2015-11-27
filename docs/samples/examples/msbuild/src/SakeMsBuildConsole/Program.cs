namespace SakeMsBuildConsole
{
    using System;
    using SakeMsBuildLibrary;

    class Program
    {
        static void Main(string[] args)
        {
            var deepThought = new DeepThought();
            
            var answer = deepThought?.AnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything();

            Console.WriteLine($"The answer to the ultimate question of life, the universe, and everything is... {answer}");
            Console.ReadLine();
        }
    }
}

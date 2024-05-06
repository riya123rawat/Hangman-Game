

using System;
using System.Text; // Added using directive for StringBuilder
using System.Linq;

class HangmanGame
{
    private string[] words = { "apple", "banana", "orange", "grape", "pineapple" };
    private string secretWord;
    private char[] correctLetters;
    private StringBuilder incorrectLetters = new StringBuilder();
    private int guessesLeft = 10;

    public void StartGame()
    {
        // Select a random word from the array
        Random random = new Random();
        secretWord = words[random.Next(words.Length)];

        // Initialize correctLetters array with underscores
        correctLetters = new char[secretWord.Length];
        for (int i = 0; i < correctLetters.Length; i++)
        {
            correctLetters[i] = '_';
        }

        Console.WriteLine("Welcome to Hangman!");
        Console.WriteLine("Guess the word:");

        while (guessesLeft > 0)
        {
            DisplayGameState();
            string guess = Console.ReadLine().ToLower();

            if (guess.Length == 1)
            {
                HandleLetterGuess(guess[0]);
            }
            else if (guess.Length == secretWord.Length)
            {
                HandleWordGuess(guess);
            }
            else
            {
                Console.WriteLine("Invalid guess. Please enter a single letter or the entire word.");
            }

            if (secretWord.SequenceEqual(correctLetters))
            {
                Console.WriteLine("Congratulations! You guessed the word: " + secretWord);
                return;
            }
        }

        Console.WriteLine("Sorry, you ran out of guesses. The word was: " + secretWord);
    }

    private void HandleLetterGuess(char letter)
    {
        if (secretWord.Contains(letter))
        {
            Console.WriteLine("Correct guess!");
            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == letter)
                {
                    correctLetters[i] = letter;
                }
            }
        }
        else
        {
            Console.WriteLine("Incorrect guess.");
            guessesLeft--;
            incorrectLetters.Append(letter + " ");
        }
    }

    private void HandleWordGuess(string word)
    {
        if (word == secretWord)
        {
            Array.Copy(secretWord.ToCharArray(), correctLetters, secretWord.Length);
        }
        else
        {
            Console.WriteLine("Incorrect guess.");
            guessesLeft--;
        }
    }

    private void DisplayGameState()
    {
        Console.WriteLine("Word: " + string.Join(" ", correctLetters));
        Console.WriteLine("Incorrect guesses: " + incorrectLetters);
        Console.WriteLine("Guesses left: " + guessesLeft);
        Console.WriteLine("Guess a letter or the entire word:");
    }
}

class Program
{
    static void Main(string[] args)
    {
        HangmanGame game = new HangmanGame();
        game.StartGame();
    }
}

﻿using KeyboardKata.Domain;
using KeyboardKata.Domain.Actions;
using KeyboardKata.Domain.InputProcessing;
using KeyboardKata.Domain.Sessions;

namespace KeyboardKata.Trainer.Cli
{
    internal class CliKeyboardKata : IKeyboardKata
    {
        public void Failure(KeyboardAction action, IEnumerable<Key> actual)
        {
            throw new NotImplementedException();
        }

        public void Finish(SessionResult result)
        {
            Console.WriteLine("All done!");
        }

        public void Progress(KeyboardAction action, IEnumerable<Key> remaining)
        {
            throw new NotImplementedException();
        }

        public void Prompt(KeyboardAction action)
        {
            Console.WriteLine(action.Prompt);
        }

        public void Success(KeyboardAction action)
        {
            Console.WriteLine("You did it!");
        }
    }
}

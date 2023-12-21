﻿using KeyboardKata.App.Commands;
using KeyboardKata.Domain.InputMatching;
using System;

namespace KeyboardKata.App.Shortcuts
{
    public record ShortcutCommand : Command
    {
        public ShortcutCommand(string identifier, Action action, IPattern shortcut) : base(identifier, action)
        {
            Shortcut = shortcut;
        }

        public IPattern Shortcut { get; }
    }
}

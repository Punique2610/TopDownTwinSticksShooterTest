using System;

namespace Ilumisoft.TwinStickShooterKit
{
    public class ManagerNotFoundException : Exception
    {
        public ManagerNotFoundException() { }

        public ManagerNotFoundException(string message) : base(message) { }

        public ManagerNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
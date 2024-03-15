using System;

namespace Exceptions;



public class PreconditionException : Exception
{
    public PreconditionException(string message) : base(message) {}
}
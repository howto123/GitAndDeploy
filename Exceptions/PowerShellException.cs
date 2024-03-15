



using System;

namespace PwShell;


class PowerShellException : Exception
{
    public PowerShellException(string msg) : base(msg) {}
}
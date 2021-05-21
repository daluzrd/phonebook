using System;

namespace DataServices
{
    [Serializable]
    public class NotFoundException : Exception 
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
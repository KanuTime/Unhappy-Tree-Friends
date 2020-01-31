using System;

namespace _Scripts.Utility
{
    public interface ISelfDestruction
    {
        void Destroy();
    }
    
    public class SelfDestruction : ISelfDestruction
    {
        private readonly Action _destroy;

        public SelfDestruction(Action destroy)
        {
            _destroy = destroy;
        } 
            
        public void Destroy()
        {
            _destroy();
        }
    }
}
using System;

namespace IoDemo
{
    public static class Extenders
    {
        public static Int32 Double(this Int32 @this)
        {
            return @this*2;
        }
    }
}
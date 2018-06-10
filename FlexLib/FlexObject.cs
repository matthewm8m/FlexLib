using System;

namespace FlexLib
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FlexObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual object Call(string method, params object[] args)
        {
            // Perform specific operations based on method and arguments
            switch (method)
            {
                // Equals checks if all parameter objects are equal to this object
                case "equals":
                    if (args.Length == 0)
                        throw new NotSupportedException($"{method} must have 1 or more objects to compare to.");

                    for (int i = 0; i < args.Length; i++)
                        if (!Equals(args[i]))
                            return false;
                    return true;

                // Representation simply converts this object into a string representation
                case "representation":
                    return ToString();
                
                // If method is not found, throw an exception
                default:
                    throw new InvalidOperationException($"{method} is not a valid method.");
            }
        }
    }
}

using System;

namespace FlexLib
{
    /// <summary>
    /// Implements a basic object that all FlexLib objects should inherit.
    /// </summary>
    public abstract class FlexObject
    {
        public static bool CallEquals(FlexObject that, params object[] args)
        {
            return that.Equals(args);
        }

        public static string CallToString(FlexObject that, params object[] args)
        {
            return that.ToString();
        }

        /// <summary>
        /// Calls a method of the object with specified arguments.
        /// Classes that override this method should call the base class to implement remaining functionality.
        /// </summary>
        /// <param name="method">The method to call.</param>
        /// <param name="args">The arguments to the method.</param>
        /// <returns>The results of calling the method.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a method does not exist.</exception>
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

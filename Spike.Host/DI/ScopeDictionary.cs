namespace App.Base.DI
{

    /// <summary>
    /// A Singleton
    /// Dictionary to permit the 
    /// <see cref="MyServiceBasedControllerActivator"/>
    /// find the right DI scope to build a 
    /// late loaded controller.
    /// </summary>
    public class ScopeDictionary : Dictionary<Type, ScopeDictionaryEntry>
    {
        static public ScopeDictionary Instance { get; private set; } = new ScopeDictionary();
    }
}

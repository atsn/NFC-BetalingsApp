namespace Tilbudsapp
{
    public interface IWebUri
    {
        /// <summary>
        /// Uri for Webservice ex. "Statue"
        /// </summary>
        string ResourceUri { get; }
        string VerboseName { get; }
    }
}

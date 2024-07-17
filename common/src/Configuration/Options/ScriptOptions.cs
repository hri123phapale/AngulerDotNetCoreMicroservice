namespace Blogposts.Common.Configuration.Options
{
    /// <summary>
    /// Class containing the strongly typed options necessary for the scripts
    /// </summary>
    public class ScriptOptions
    {
        /// <summary>
        /// Ghostscript is a suite of software based on an interpreter for Adobe Systems' PostScript and Portable Document Format (PDF) page description languages.
        /// The script is used to flatten the uploaded PDFs in order to remove any anotations that hold javascript actions.
        /// </summary>
        public GhostScript GhostScript { get; set; }
    }
}
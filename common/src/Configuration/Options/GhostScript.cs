namespace Blogposts.Common.Configuration.Options
{
    public class GhostScript
    {
        /// <summary>
        /// The executable that will trigger the ghostscript flattening of the pdf
        /// </summary>
        public string Executable { get; set; }

        /// <summary>
        /// The script arguments
        /// </summary>
        public string Args { get; set; }
    }
}
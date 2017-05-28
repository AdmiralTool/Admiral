namespace smartadmiral.console.Args
{
    internal class AdmiralArgs
    {
        public AdmiralArgs(string pathToShips, string pathToDirective)
        {
            PathToShips = pathToShips;
            PathToDirective = pathToDirective;
        }

        public string PathToShips { get; private set; }
        public string PathToDirective { get; private set; }
    }
}
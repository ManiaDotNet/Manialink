namespace ManiaNet.Manialink.Elements
{
    internal class ManialinkFileEntry : ManialinkEntry
    {
        public ManialinkFileEntry()
            : base(ManialinkElement.ElementType.FileEntry)
        {
            validFields.Add("folder");
        }
    }
}
namespace Hypermedia.AspNetCore.ApiExport
{
    internal interface IFileOutputLogic
    {
        void Save(string path, string content);
    }
}
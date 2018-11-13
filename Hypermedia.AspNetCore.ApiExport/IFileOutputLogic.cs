namespace Hypermedia.AspNetCore.ApiExport
{
    public interface IFileOutputLogic
    {
        void Save(string path, string content);
    }
}
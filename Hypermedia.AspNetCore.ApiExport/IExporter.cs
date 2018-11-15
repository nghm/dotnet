namespace Hypermedia.AspNetCore.ApiExport
{
    internal interface IExporter
    {
        void Export(ExportDefinition exportDefinition);
    }
}
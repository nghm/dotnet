namespace Hypermedia.AspNetCore.ApiExport
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ApplicationOptions
    {
        private Guid _tempDirGuid;

        public ApplicationOptions()
        {
            this._tempDirGuid = Guid.NewGuid();
        }

        public string Input { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public string Output { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public IEnumerable<string> ControllerAssemblies { get; set; }

        public string TempBuildDir => $"{Input}/{this._tempDirGuid}";

        public string ExportAssemblyName { get; set; }
    }
}
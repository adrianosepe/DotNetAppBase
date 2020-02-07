using System.Reflection;
using System.Resources;

[assembly: AssemblyVersion(version: "1.2.37")]
[assembly: AssemblyFileVersion(version: "1.2.37")]
[assembly: SatelliteContractVersion(version: "1.2.37")]

#if BETA
    [assembly: AssemblyInformationalVersion("BETA")]
#elif DEBUG
    [assembly: AssemblyInformationalVersion(informationalVersion: "DEBUG")]
#else
    [assembly: AssemblyInformationalVersion("RTM")]
#endif
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Vonage")]
[assembly: AssemblyDescription("Official C#/.NET wrapper for the Vonage API")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Vonage")]
[assembly: AssemblyProduct("Vonage")]
[assembly: AssemblyCopyright("© Vonage 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
#if(RELEASESIGNED)
[assembly: InternalsVisibleTo("Vonage.Test.Unit, PublicKey=dc6dad05b9ecb75a")]
#else
[assembly: InternalsVisibleTo("Vonage.Test.Unit")]
#endif

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("91ab32fe-85ff-4f90-87e9-aad072b21577")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("5.3.0.0")]
[assembly: AssemblyFileVersion("5.3.0.0")]


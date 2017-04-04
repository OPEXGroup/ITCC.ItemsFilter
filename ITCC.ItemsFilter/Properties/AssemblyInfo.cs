// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: Guid("958e5e4e-b421-449c-af2d-fd5ba246eecc")]
[assembly: AssemblyTitle("ITCC.ItemsFilter")]
[assembly: AssemblyDescription("WPF ItemsFilter Extension for any ItemsControl, such as DataGrid.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ITCC")]
[assembly: AssemblyProduct("ITCC.ItemsFilter")]
[assembly: AssemblyCopyright("Copyright (c) 2016-2017, Vladimir Tyrin, Vladislav Prishchepa")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
    )]

[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls", "ITCC.ItemsFilter")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter.View")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter.View.Converters")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter.ViewModel")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter.Initializer")]
[assembly: XmlnsDefinition("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "ITCC.ItemsFilter.Model")]

[assembly: XmlnsPrefix("http://schemas.itemsfilter.itcc.com/xaml/Controls/ItemsFilter", "itemsFilter")]
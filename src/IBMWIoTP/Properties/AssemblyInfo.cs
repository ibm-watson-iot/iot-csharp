#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IBMWIoTP")]
[assembly: AssemblyDescription("C# Library to simplify interactions with the IBM Watson IoT Platform")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("IBM")]
[assembly: AssemblyProduct("IBMWIoTP")]
[assembly: AssemblyCopyright("Copyright (c) 2017 IBM Corporation")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("0.2.3")]

// For log4net
[assembly: log4net.Config.XmlConfigurator(ConfigFile="IBMWIoTPLog.config",Watch=true)]

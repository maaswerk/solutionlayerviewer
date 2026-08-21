# Solution Layer Viewer for XrmToolBox

An [XrmToolBox](https://www.xrmtoolbox.com/) plugin for Microsoft Dataverse / Dynamics 365.

Pick a solution and the plugin lists all of its components. Select a component
and it shows the **solution layer stack** for that component: every solution
that also contains the same component, whether that layer is managed or
unmanaged, its version and its publisher.

This is the scaffold / first iteration of the plugin — the UI and data access
are functional, but the exact layer ordering shown by the Power Platform maker
portal's "Solution layers" dialog is not publicly documented by Microsoft.
The current ordering heuristic (unmanaged/"Active" layer on top, then managed
solutions ordered by version, newest first) is a reasonable approximation and
a good starting point, not a guaranteed match — see
[`PluginControl.LoadLayers`](src/SolutionLayerViewer/PluginControl.cs) for the
exact logic and its caveats.

## Project layout

```
SolutionLayerViewer.sln
src/SolutionLayerViewer/
  SolutionLayerViewer.csproj   # net48 WinForms class library
  PluginControl.cs             # main plugin UI + data access
  ComponentTypes.cs            # solutioncomponent componenttype -> friendly name
  Models/
    SolutionListItem.cs
    SolutionComponentItem.cs
    SolutionLayerItem.cs
```

## Building

Requirements: Visual Studio 2022 (or `dotnet build` with the .NET Framework
4.8 targeting pack) on Windows, since the project references WinForms and the
XrmToolBox plugin host.

```
dotnet restore
dotnet build -c Release
```

## Running inside XrmToolBox

1. Build the project.
2. Copy `SolutionLayerViewer.dll` (and any dependency that isn't already
   shipped with XrmToolBox, e.g. `Microsoft.Xrm.Sdk.dll`) into your
   XrmToolBox `Plugins` folder.
3. Start XrmToolBox, connect to an environment, and open **Solution Layer
   Viewer** from the plugin list.

Packaging the plugin as a NuGet package for the official XrmToolBox Plugin
Store (via `XrmToolBox.PluginPackager`) is a follow-up step, not included
here yet.

## Roadmap / ideas

- Verify the layer ordering heuristic against real environments and adjust it
  (or expose the raw, unordered layer list alongside the heuristic).
- Show component display names (not just object ids) by resolving metadata
  for entity/attribute/form/etc. component types.
- Export the layer stack of a component to CSV/Excel.
- Package as a proper XrmToolBox plugin NuGet package.

## License

[MIT](LICENSE)

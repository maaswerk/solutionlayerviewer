using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace SolutionLayerViewer
{
    // The plugin's MEF export + XrmToolBox tool-list metadata live on this small
    // class rather than on PluginControl itself, mirroring the pattern used by
    // maaswerk/SolutionOperationMonitor. Without SmallImageBase64/BigImageBase64,
    // XrmToolBox fails to render the tool tile and the plugin doesn't show up.
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Solution Layer Viewer"),
        ExportMetadata("Description", "Lists the components of a solution and shows the solution layer stack for the selected component."),
        // 32x32 px icon, base64
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAfklEQVR4nGNgGAUDDBjxylZcyaOJrR06k2BMJrpbjmY2bgfQCYw6YBA7ACmlUh0gmY07G1Zc2UIjy32QudhDgFaWYzF7EKeBUQcMqAPQUipVAZrZ2LNhxZX/NLIcwz7MEKCV5TjMHqRpYNQBA+oALCmVaoCWZo+CUTAKhiwAAPipGyjh9utaAAAAAElFTkSuQmCC"),
        // 80x80 px icon, base64
        ExportMetadata("BigImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAABF0lEQVR4nO3YwW2DMBiGYafzMEpm6ZkROHeWjsI+7akX1DTYn5MU8zzXBAu/soX0lwIAAFS7dF9xXt+7r/lKy/Tx18/9Ao4WbutGyLdnv8do+gQc/fSVcnOPTmBIwJCAIQFDAoYEDAkYEjAkYEjAUJ+AdyYWQzBMeAzzwHvOcLs4sD5XeF4/u6zzXyzTde9fs4CjhdvaEdJXONQecPTTV8quPTqBIQFDAoYEDAkYEjAkYEjAkIAhAUPtASsmFodlmPB45oG/OcPtYhDtV3hevzq+x+stU1OL+odGC7dVGdJXOFQXcPTTV0r1Hp3AkIAhAUMChgQMCRgSMCRgSMCQgKG6gI0Ti0MxTHgu88AfZ7hdAAAAAEDkG3brLV7ZfDeYAAAAAElFTkSuQmCC"),
        ExportMetadata("BackgroundColor", "#F0F7FF"),
        ExportMetadata("PrimaryFontColor", "#202020"),
        ExportMetadata("SecondaryFontColor", "#6E7480")]
    public class SolutionLayerViewerPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PluginControl();
        }
    }
}

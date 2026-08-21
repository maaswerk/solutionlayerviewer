using Microsoft.Xrm.Sdk;

namespace SolutionLayerViewer.Models
{
    /// <summary>
    /// One row of the layer stack for a component: a solution that also
    /// contains the same component (same objectid + componenttype).
    /// </summary>
    public class SolutionLayerItem
    {
        public SolutionLayerItem(Entity solutionComponent)
        {
            SolutionName = solutionComponent.GetAttributeValue<AliasedValue>("sol.friendlyname")?.Value as string;

            VersionText = solutionComponent.GetAttributeValue<AliasedValue>("sol.version")?.Value as string;
            System.Version.TryParse(VersionText, out var parsedVersion);
            Version = parsedVersion;

            IsManaged = solutionComponent.GetAttributeValue<AliasedValue>("sol.ismanaged")?.Value as bool? ?? false;

            var publisher = solutionComponent.GetAttributeValue<AliasedValue>("sol.publisherid")?.Value as EntityReference;
            Publisher = publisher?.Name;
        }

        /// <summary>1-based position in the (heuristically ordered) layer stack, top layer first.</summary>
        public int Order { get; set; }

        public string SolutionName { get; }

        public System.Version Version { get; }

        public string VersionText { get; }

        public bool IsManaged { get; }

        public string Publisher { get; }
    }
}

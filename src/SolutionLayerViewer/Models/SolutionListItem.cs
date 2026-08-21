using System;
using Microsoft.Xrm.Sdk;

namespace SolutionLayerViewer.Models
{
    public class SolutionListItem
    {
        public SolutionListItem(Entity solution)
        {
            SolutionId = solution.Id;
            FriendlyName = solution.GetAttributeValue<string>("friendlyname");
            UniqueName = solution.GetAttributeValue<string>("uniquename");
        }

        public Guid SolutionId { get; }

        public string FriendlyName { get; }

        public string UniqueName { get; }

        public override string ToString() => FriendlyName;
    }
}

using System;
using Microsoft.Xrm.Sdk;

namespace SolutionLayerViewer.Models
{
    public class SolutionComponentItem
    {
        public SolutionComponentItem(Entity solutionComponent)
        {
            ComponentTypeCode = solutionComponent.GetAttributeValue<OptionSetValue>("componenttype")?.Value ?? 0;
            ComponentType = SolutionLayerViewer.ComponentTypes.GetName(ComponentTypeCode);
            ObjectId = solutionComponent.GetAttributeValue<Guid>("objectid");
        }

        public int ComponentTypeCode { get; }

        public string ComponentType { get; }

        public Guid ObjectId { get; }
    }
}

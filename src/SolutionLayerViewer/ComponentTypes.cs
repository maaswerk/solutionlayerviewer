using System.Collections.Generic;

namespace SolutionLayerViewer
{
    /// <summary>
    /// Friendly names for the `solutioncomponent.componenttype` option set.
    /// Only the commonly encountered values are mapped; anything else falls
    /// back to "Type {code}".
    /// </summary>
    internal static class ComponentTypes
    {
        private static readonly Dictionary<int, string> Names = new Dictionary<int, string>
        {
            { 1, "Entity" },
            { 2, "Attribute" },
            { 3, "Relationship" },
            { 4, "Attribute Picklist Value" },
            { 5, "Attribute Lookup Value" },
            { 6, "View Attribute" },
            { 7, "Localized Label" },
            { 8, "Relationship Extra Condition" },
            { 9, "Option Set" },
            { 10, "Entity Relationship" },
            { 11, "Entity Relationship Role" },
            { 12, "Entity Relationship Relationships" },
            { 13, "Managed Property" },
            { 14, "Entity Key" },
            { 16, "Privilege" },
            { 17, "Privilege Object Type Code" },
            { 20, "Role" },
            { 21, "Role Privilege" },
            { 22, "Display String" },
            { 23, "Display String Map" },
            { 24, "Form" },
            { 25, "Organization" },
            { 26, "Saved Query" },
            { 29, "Workflow" },
            { 31, "Report" },
            { 32, "Report Entity" },
            { 33, "Report Category" },
            { 34, "Report Visibility" },
            { 35, "Attachment" },
            { 36, "Email Template" },
            { 37, "Contract Template" },
            { 38, "KB Article Template" },
            { 39, "Mail Merge Template" },
            { 44, "Duplicate Rule" },
            { 45, "Duplicate Rule Condition" },
            { 46, "Entity Map" },
            { 47, "Attribute Map" },
            { 48, "Ribbon Command" },
            { 49, "Ribbon Context Group" },
            { 50, "Ribbon Customization" },
            { 52, "Ribbon Rule" },
            { 53, "Ribbon Tab To Command Map" },
            { 55, "Ribbon Diff" },
            { 59, "Saved Query Visualization" },
            { 60, "System Form" },
            { 61, "Web Resource" },
            { 62, "Site Map" },
            { 63, "Connection Role" },
            { 65, "Field Security Profile" },
            { 66, "Field Permission" },
            { 68, "Plugin Type" },
            { 69, "Plugin Assembly" },
            { 70, "SDK Message Processing Step" },
            { 71, "SDK Message Processing Step Image" },
            { 72, "Service Endpoint" },
            { 80, "Routing Rule" },
            { 81, "Routing Rule Item" },
            { 82, "SLA" },
            { 83, "SLA Item" },
            { 90, "Mobile Offline Profile" },
            { 91, "Mobile Offline Profile Item" },
            { 92, "Similarity Rule" },
            { 95, "Data Source Mapping" },
            { 150, "Hierarchy Rule" },
            { 161, "Custom Control" },
            { 162, "Custom Control Default Config" },
        };

        public static string GetName(int componentType)
        {
            return Names.TryGetValue(componentType, out var name) ? name : $"Type {componentType}";
        }
    }
}

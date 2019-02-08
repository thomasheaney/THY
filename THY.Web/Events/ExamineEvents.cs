using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.businesslogic;
using UmbracoExamine;

namespace THY.Web
{
    public class ExamineEvents : ApplicationStartupHandler
    {
        // cpoied and amended from https://our.umbraco.org/forum/developers/api-questions/57958-Using-Examine-to-search-UmbracoTags
        public ExamineEvents()
        {
            ExamineManager.Instance.IndexProviderCollection["CategoryIndexer"].GatheringNodeData += ExamineEvents_GatheringNodeData;
        }

        private void ExamineEvents_GatheringNodeData(object sender, IndexingNodeDataEventArgs e)
        {
            if (e.IndexType != IndexTypes.Content) return;

            // Category values are stored as csv which will not be indexed properly 
            // We need to write the values back into the index without commas so they are indexed correctly

            var fields = e.Fields;
            var searchablefields = new Dictionary<string, string>();

            foreach (var field in fields)
            {
                switch (field.Key)
                {
                    case "category":

                        var searchableFieldKey = "categoryIndexed";
                        var searchableFieldValue = field.Value.Replace(',', ' ');

                        if (!string.IsNullOrEmpty(searchableFieldValue))
                        {
                            searchablefields.Add(searchableFieldKey, searchableFieldValue);
                        }
                        break;

                    case "tags":

                        var searchableTagsFieldKey = "tagsIndexed";
                        var searchableTagsFieldValue = field.Value.Replace(',', ' ');

                        if (!string.IsNullOrEmpty(searchableTagsFieldValue))
                        {
                            searchablefields.Add(searchableTagsFieldKey, searchableTagsFieldValue);
                        }
                        break;
                }
            }

            foreach (var fld in searchablefields)
            {
                e.Fields.Add(fld.Key, fld.Value);
            }
        }
    }
}
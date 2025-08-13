using System;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetDatastoreTypes
{
    public static class Hardcode
    {
        public static DataStoreTypeReturn GetDatastoreTypes(ConnectionInfo conn, System.Collections.Hashtable customparams, DataStoreInfo datastore)
        {
            return new DataStoreTypeReturn
            {
                error = false,
                errorMsg = "",
                datastoretypes = new[]
                {
                    new DataStoreType
                    {
                        id = "cm:content",
                        name = "Content",
                        typedata = new[]
                        {
                            new TypeData { name = "cm_name", type = "String", displayname = "Name", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "FoldersRelativePath", type = "String", displayname = "", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "PARENT_ID", type = "String", displayname = "", searchable = true, facetable = false, sortable = false }
                        }
                    },
                    new DataStoreType
                    {
                        id = "bpm:task",
                        name = "Task",
                        typedata = new[]
                        {
                            new TypeData { name = "bpm_status", type = "String", displayname = "Status", searchable = true, facetable = true, sortable = true },
                            new TypeData { name = "bpm_priority", type = "Integer", displayname = "Priority", searchable = true, facetable = true, sortable = true },
                            new TypeData { name = "cm_name", type = "String", displayname = "", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_percentComplete", type = "Integer", displayname = "Percent Complete", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_dueDate", type = "Date", displayname = "Due Date", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_startDate", type = "Date", displayname = "Start Date", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_completionDate", type = "Date", displayname = "Completion Date", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_comment", type = "String", displayname = "Comment", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "bpm_taskId", type = "Double", displayname = "Identifier", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "bpm_description", type = "String", displayname = "Description of what needs to be achieved", searchable = true, facetable = false, sortable = false }
                        }
                    },
                    new DataStoreType
                    {
                        id = "cm:savedquery",
                        name = "Saved Query",
                        typedata = new[]
                        {
                            new TypeData { name = "cm_name", type = "String", displayname = "Name", searchable = true, facetable = false, sortable = true }
                        }
                    },
                    new DataStoreType
                    {
                        id = "cm:thumbnail",
                        name = "Thumbnail",
                        typedata = new[]
                        {
                            new TypeData { name = "cm_contentPropertyName", type = "String", displayname = "", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "cm_name", type = "String", displayname = "Name", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "cm_thumbnailName", type = "String", displayname = "", searchable = true, facetable = false, sortable = false }
                        }
                    },
                    new DataStoreType
                    {
                        id = "cm:dictionaryModel",
                        name = "Dictionary Model",
                        typedata = new[]
                        {
                            new TypeData { name = "cm_name", type = "String", displayname = "Name", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "cm_modelName", type = "String", displayname = "", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "cm_modelAuthor", type = "String", displayname = "", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "cm_modelActive", type = "Bool", displayname = "", searchable = false, facetable = true, sortable = false },
                            new TypeData { name = "cm_modelPublishedDate", type = "Date", displayname = "", searchable = true, facetable = false, sortable = true },
                            new TypeData { name = "cm_modelDescription", type = "String", displayname = "", searchable = true, facetable = false, sortable = false },
                            new TypeData { name = "cm_modelVersion", type = "String", displayname = "", searchable = true, facetable = false, sortable = false }
                        }
                    }
                }
            };
        }


        private static bool GetDatastoreTypes_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new System.Collections.Hashtable();
            var datastore = new DataStoreInfo();
            var result = GetDatastoreTypes(conn, customparams, datastore);
            
            if (result.error) return false;
            if (result.datastoretypes.Length != 5) return false;
            if (result.datastoretypes[0].id != "cm:content") return false;
            if (result.datastoretypes[1].id != "bpm:task") return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}
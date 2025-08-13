using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoItemData
{
    public static class Hardcode
    {
        public static ItemReturn GetItemData(string id, bool getfile, bool getmetadata)
        {
            // Handle null or empty ID
            id = id ?? "";
            
            // Return different mock data based on the ID to simulate variety
            if (id == "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7")
            {
                return GetDocumentExample(getfile, getmetadata);
            }
            
            // Default: return link example (based on the JSON data)
            return GetLinkExample(id, getfile, getmetadata);
        }


        private static ItemReturn GetLinkExample(string id, bool getfile, bool getmetadata)
        {
            var item = new ItemReturn
            {
                id = id,
                subid = "",
                type = "lnk:link",
                title = "W3 Schools",
                author = "Mike Jackson",
                lastModified = DateTime.Parse("2011-02-15T21:43:14.377+00:00"),
                created = DateTime.Parse("2011-02-15T21:43:14.377+00:00"),
                contentType = "text/html",
                size = 1024,
                contentUrl = $"http://100.115.192.75:8080/share/page/site/swsdp/document-details?nodeRef=workspace://SpacesStore/{id}",
                allowedUsers = new[] { "admin", "mjackson" },
                deniedUsers = Array.Empty<string>(),
                allowedGroups = new[] { "EVERYONE", "site_swsdp_SiteContributor" },
                deniedGroups = Array.Empty<string>(),
                error = false,
                errorMsg = ""
            };

            if (getfile)
                item.content = System.Text.Encoding.UTF8.GetBytes("http://www.w3schools.com/");

            if (getmetadata)
            {
                item.metadata = new Hashtable
                {
                    ["lnk_url"] = "http://www.w3schools.com/",
                    ["cm_owner"] = "admin",
                    ["lnk_description"] = "The W3 Schools web site has some good guides (with interactive examples) on how to create websites",
                    ["lnk_title"] = "W3 Schools",
                    ["FoldersRelativePath"] = "/Company Home/Sites/swsdp/links",
                    ["PARENT_ID"] = "0e24b99c-41f0-43e1-a55e-fb9f50d73820",
                    ["CM_CREATED"] = DateTime.Parse("2011-02-15T21:43:14.377+00:00")
                };
            }

            return item;
        }


        private static ItemReturn GetDocumentExample(bool getfile, bool getmetadata)
        {
            var item = new ItemReturn
            {
                id = "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7",
                subid = "",
                type = "cm:content",
                title = "Sample Document",
                author = "Administrator",
                lastModified = DateTime.Parse("2011-02-15T21:35:26.467+00:00"),
                created = DateTime.Parse("2011-02-15T21:35:26.467+00:00"),
                contentType = "text/plain",
                size = 2048,
                contentUrl = "http://100.115.192.75:8080/alfresco/d/d/workspace/SpacesStore/5fa74ad3-9b5b-461b-9df5-de407f1f4fe7/sample.txt",
                allowedUsers = new[] { "admin" },
                deniedUsers = Array.Empty<string>(),
                allowedGroups = new[] { "EVERYONE" },
                deniedGroups = Array.Empty<string>(),
                error = false,
                errorMsg = ""
            };

            if (getfile)
                item.content = System.Text.Encoding.UTF8.GetBytes("This is the content of the sample document.");

            if (getmetadata)
            {
                item.metadata = new Hashtable
                {
                    ["cm_name"] = "sample.txt",
                    ["cm_description"] = "A sample text document",
                    ["cm_owner"] = "admin",
                    ["FoldersRelativePath"] = "/Company Home/Documents",
                    ["PARENT_ID"] = "document-library-root"
                };
            }

            return item;
        }


        private static bool GetItemData_Link_TEST_()
        {
            var result = GetItemData("a38308f8-6f30-4d8a-8576-eaf6703fb9d3", true, true);
            
            if (result.id != "a38308f8-6f30-4d8a-8576-eaf6703fb9d3") return false;
            if (result.type != "lnk:link") return false;
            if (result.title != "W3 Schools") return false;
            if (result.author != "Mike Jackson") return false;
            if (result.contentType != "text/html") return false;
            if (result.size != 1024) return false;
            if (result.content == null) return false;
            if (result.metadata == null) return false;
            if (!result.contentUrl.Contains("a38308f8-6f30-4d8a-8576-eaf6703fb9d3")) return false;
            if (result.allowedUsers.Length != 2) return false;
            if (result.allowedGroups.Length != 2) return false;
            if (result.error) return false;
            
            return true;
        }


        private static bool GetItemData_Document_TEST_()
        {
            var result = GetItemData("5fa74ad3-9b5b-461b-9df5-de407f1f4fe7", true, true);
            
            if (result.id != "5fa74ad3-9b5b-461b-9df5-de407f1f4fe7") return false;
            if (result.type != "cm:content") return false;
            if (result.title != "Sample Document") return false;
            if (result.author != "Administrator") return false;
            if (result.contentType != "text/plain") return false;
            if (result.size != 2048) return false;
            if (result.content == null) return false;
            if (result.metadata == null) return false;
            if (result.allowedUsers.Length != 1) return false;
            if (result.allowedGroups.Length != 1) return false;
            if (result.error) return false;
            
            return true;
        }


        private static bool GetItemData_No_File_Content_TEST_()
        {
            var result = GetItemData("test-id", false, true);
            
            if (result.content != null) return false;
            if (result.metadata == null) return false;
            
            return true;
        }


        private static bool GetItemData_No_Metadata_TEST_()
        {
            var result = GetItemData("test-id", true, false);
            
            if (result.content == null) return false;
            if (result.metadata != null) return false;
            
            return true;
        }


        private static bool GetItemData_Neither_Content_Nor_Metadata_TEST_()
        {
            var result = GetItemData("test-id", false, false);
            
            if (result.content != null) return false;
            if (result.metadata != null) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Hardcode)); }
    }
}
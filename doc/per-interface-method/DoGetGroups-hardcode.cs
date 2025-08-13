// Hardcoded implementation for iCustomConnector2.DoGetGroups() method
// Returns: SystemIdentityInfoReturn
// Interface method: public abstract SystemIdentityInfoReturn DoGetGroups(ConnectionInfo conn, Hashtable customparams, string additionalColumns);

var groups = new[]
{
    new SystemIdentityInfo
    {
        id = "GROUP_ALFRESCO_ADMINISTRATORS",
        name = "ALFRESCO_ADMINISTRATORS",
        displayName = "ALFRESCO_ADMINISTRATORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_ALFRESCO_MODEL_ADMINISTRATORS",
        name = "ALFRESCO_MODEL_ADMINISTRATORS",
        displayName = "ALFRESCO_MODEL_ADMINISTRATORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_ALFRESCO_SEARCH_ADMINISTRATORS",
        name = "ALFRESCO_SEARCH_ADMINISTRATORS",
        displayName = "ALFRESCO_SEARCH_ADMINISTRATORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_ALFRESCO_SYSTEM_ADMINISTRATORS",
        name = "ALFRESCO_SYSTEM_ADMINISTRATORS",
        displayName = "ALFRESCO_SYSTEM_ADMINISTRATORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_EMAIL_CONTRIBUTORS",
        name = "EMAIL_CONTRIBUTORS",
        displayName = "EMAIL_CONTRIBUTORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_SITE_ADMINISTRATORS",
        name = "SITE_ADMINISTRATORS",
        displayName = "SITE_ADMINISTRATORS",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_site_swsdp",
        name = "site_swsdp",
        displayName = "site_swsdp",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_site_swsdp_SiteCollaborator",
        name = "site_swsdp_SiteCollaborator",
        displayName = "site_swsdp_SiteCollaborator",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_site_swsdp_SiteConsumer",
        name = "site_swsdp_SiteConsumer",
        displayName = "site_swsdp_SiteConsumer",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_site_swsdp_SiteContributor",
        name = "site_swsdp_SiteContributor",
        displayName = "site_swsdp_SiteContributor",
        email = ""
    },
    new SystemIdentityInfo
    {
        id = "GROUP_site_swsdp_SiteManager",
        name = "site_swsdp_SiteManager",
        displayName = "site_swsdp_SiteManager",
        email = ""
    }
};

return new SystemIdentityInfoReturn
{
    identities = groups,
    error = false,
    errorMsg = ""
};
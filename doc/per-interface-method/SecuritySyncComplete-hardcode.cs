// Example hardcoded return values for SecuritySyncComplete method

// SecuritySyncComplete is a notification method that doesn't return data
// It simply acknowledges that the security sync has completed

return new SecuritySyncCompleteResponse
{
    success = true,
    message = "Security synchronization completed successfully"
};

// Note: This method is WSDL-only and has no corresponding interface method
// It's used to notify the system that security synchronization has finished
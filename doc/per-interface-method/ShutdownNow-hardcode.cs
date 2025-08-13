// Example hardcoded return values for ShutdownNow method

// ShutdownNow is an internal method for shutting down the server
// It's not part of the interface but exists for our own use

return new ShutdownResponse
{
    acknowledged = true,
    message = "Shutdown command received and will be processed"
};

// Note: This method has no corresponding interface method
// It's used internally to gracefully shutdown the service
// In actual implementation, this would trigger a shutdown sequence
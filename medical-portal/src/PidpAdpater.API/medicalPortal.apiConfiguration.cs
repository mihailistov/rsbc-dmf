﻿namespace pdipadapter.Infrastructure.Auth;
public class PdipadapterConfiguration
{
    public static bool IsProduction() => EnvironmentName == Environments.Production;
    public static bool IsDevelopment() => EnvironmentName == Environments.Development;
    private static readonly string? EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    public SettingsConfiguration Settings { get; set; } = new();

    public class SettingsConfiguration
    {
        public CorsConfiguration Cors { get; set; } = new();
    }

    public class CorsConfiguration
    {
        public string AllowedOrigins { get; set; }
    }

    public AddressAutocompleteClientConfiguration AddressAutocompleteClient { get; set; } = new();
    public ConnectionStringConfiguration ConnectionStrings { get; set; } = new();
    public ChesClientConfiguration ChesClient { get; set; } = new();
    public JustinParticipantClientConfiguration JustinParticipantClient { get; set; } = new();
    public KafkaClusterConfiguration KafkaCluster { get; set; } = new();
    public KeycloakConfiguration Keycloak { get; set; } = new();
    public MailServerConfiguration MailServer { get; set; } = new();

    public CMSConfiguration CMS { get; set; } = new();
    public PidpEndorsementAPIConfiguration PidpEndorsementAPI { get; set; } = new();

    // ------- Configuration Objects -------

    public class AddressAutocompleteClientConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
    public class PidpEndorsementAPIConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
    }
    public class ConnectionStringConfiguration
    {
        public string JumDatabase { get; set; } = string.Empty;
    }

    public class ChesClientConfiguration
    {
        public bool Enabled { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
    }
    public class KafkaClusterConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string BoostrapServers { get; set; } = string.Empty;
        public string TopicName { get; set; } = string.Empty;
    }
    public class JustinParticipantClientConfiguration
    {
        public string Url { get; set; } = string.Empty;
    }
    public class KeycloakConfiguration
    {
        public string RealmUrl { get; set; } = string.Empty;
        public string WellKnownConfig => KeycloakUrls.WellKnownConfig(this.RealmUrl);
        public string TokenUrl => KeycloakUrls.Token(this.RealmUrl);
        public string AdministrationUrl { get; set; } = string.Empty;
        public string AdministrationClientId { get; set; } = string.Empty;
        public string AdministrationClientSecret { get; set; } = string.Empty;
        public string HcimClientId { get; set; } = string.Empty;
    }
    public class MailServerConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public int Port { get; set; }
    }

    public class CMSConfiguration
    {
        public string SERVER_URL { get; set; } = string.Empty;
        public string CLIENTSECRET { get; set; } = string.Empty;
        public bool VALIDATESERVERCERTIFICATE { get; set; } = false;
    }
}

namespace CIR.Core.Entities.GlobalConfiguration;

public partial class GlobalConfigurationReason
{
    public long Id { get; set; }

    public short Type { get; set; }

    public bool Enabled { get; set; }

    public string Content { get; set; } = null!;


}

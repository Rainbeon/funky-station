using Content.Shared.CartridgeLoader.Cartridges;
using Content.Shared.Paper;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Server.CartridgeLoader.Cartridges;

[RegisterComponent, Access(typeof(FineIssuanceCartridgeSystem))]
[AutoGenerateComponentPause]
public sealed partial class FineIssuanceCartridgeComponent : Component
{
    /// <summary>
    /// The list of citations that were given
    /// </summary>
    [DataField("citations")]
    public List<string> Citations = new();

    /// <summary>
    /// Paper to spawn when printing citations.
    /// </summary>
    [DataField]
    public EntProtoId<PaperComponent> PaperPrototype = "CitationPaper";

    [DataField]
    public SoundSpecifier PrintSound = new SoundPathSpecifier("/Audio/Machines/diagnoser_printing.ogg");

    /// <summary>
    /// How long you have to wait before printing a citation again.
    /// </summary>
    [DataField]
    public TimeSpan PrintCooldown = TimeSpan.FromSeconds(5);

    /// <summary>
    /// When anyone is allowed to spawn another printout.
    /// </summary>
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer)), AutoPausedField]
    public TimeSpan NextPrintAllowed = TimeSpan.Zero;
}

using Robust.Shared.Serialization;

namespace Content.Shared.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class FineIssuanceUiState : BoundUserInterfaceState
{
    public List<string> Citations;

    public FineIssuanceUiState(List<string> citations)
    {
        Citations = citations;
    }
}

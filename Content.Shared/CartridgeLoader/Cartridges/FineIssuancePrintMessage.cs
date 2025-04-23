using Robust.Shared.Serialization;

namespace Content.Shared.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class FineIssuancePrintMessage : CartridgeMessageEvent
{
    public string Name;
    public uint Amount;
    public string Crime;

    public FineIssuancePrintMessage(string name, uint amount, string crime)
    {
        Name = name;
        Amount = amount;
        Crime = crime;
    }
}
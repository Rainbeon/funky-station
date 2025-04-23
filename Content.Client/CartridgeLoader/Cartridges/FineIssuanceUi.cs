using Content.Client.UserInterface.Fragments;
using Content.Shared.CartridgeLoader;
using Content.Shared.CartridgeLoader.Cartridges;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Client.CartridgeLoader.Cartridges;

public sealed partial class FineIssuanceUi : UIFragment
{
    private FineIssuanceUiFragment? _fragment;

    public override Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        _fragment = new FineIssuanceUiFragment();

        _fragment.OnPrintPressed += (name, amount, crime) =>
        {
            var ev = new FineIssuancePrintMessage(name, amount, crime);
            var message = new CartridgeUiMessage(ev);
            userInterface.SendMessage(message);
        };
    }

    public override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is not FineIssuanceUiState fineIssuanceState)
            return;

        _fragment?.UpdateState(fineIssuanceState.Citations);
    }

    private void PrintFine()
    {

    }
}

using Content.Shared.Audio;
using Content.Shared.CartridgeLoader;
using Content.Shared.CartridgeLoader.Cartridges;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Paper;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Timing;

namespace Content.Server.CartridgeLoader.Cartridges;

public sealed class FineIssuanceCartridgeSystem : EntitySystem
{
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedHandsSystem _hands = default!;
    [Dependency] private readonly PaperSystem _paper = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FineIssuanceCartridgeComponent, CartridgeMessageEvent>(OnMessage);
    }

    private void OnMessage(Entity<FineIssuanceCartridgeComponent> ent, ref CartridgeMessageEvent args)
    {
        if (args is FineIssuancePrintMessage cast)
            PrintCitation(ent, cast);
    }

    private void PrintCitation(Entity<FineIssuanceCartridgeComponent> ent, FineIssuancePrintMessage fineMessage)
    {
        if (_timing.CurTime < ent.Comp.NextPrintAllowed)
            return;

        var user = fineMessage.Actor;

        ent.Comp.NextPrintAllowed = _timing.CurTime + ent.Comp.PrintCooldown;

        var paper = Spawn(ent.Comp.PaperPrototype, _transform.GetMapCoordinates(user));

        _audio.PlayEntity(ent.Comp.PrintSound, user, paper);
        _hands.PickupOrDrop(user, paper, checkActionBlocker: false);

        var paperComp = Comp<PaperComponent>(paper);
        _paper.SetContent((paper, paperComp), Loc.GetString("fine-issuance-printout-text", ("name", fineMessage.Name), ("amount", fineMessage.Amount), ("crime", fineMessage.Crime)));
    }
}
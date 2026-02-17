namespace WibboEmulator.Games.Items.Interactors;

using GameClients;

public class InteractorTerminal : FurniInteractor
{
    public override void OnPlace(GameClient session, Item item)
    {

    }

    public override void OnRemove(GameClient session, Item item)
    {
    }

    public override void OnTrigger(GameClient session, Item item, int request, bool userHasRights, bool reverse)
    {
        if (session == null)
        {
            return;
        }

        if (item == null)
        {
            return;
        }

        //session.SendPacket(new InClientLinkComposer(item.ExtraData.Split("internalLink")));
    }

    public override void OnTick(Item item)
    {
    }
}

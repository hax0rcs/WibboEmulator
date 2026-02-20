namespace WibboEmulator.Games.Chats.Pets.Commands;

public struct PetCommand(int commandId, string commandInput)
{
    public int CommandId { get; set; } = commandId;

    public string CommandInput { get; set; } = commandInput;
}

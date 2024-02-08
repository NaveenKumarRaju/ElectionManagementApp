namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        // IUserRepository UserRepository { get; }
        // IMessageRepository MessageRepository { get; }
        // ILikesRepository LikesRepository { get; }
        IElectionManagementRepository ElectionManagementRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
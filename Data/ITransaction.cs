namespace opcode4.core.Data
{
    public interface ITransaction
    {
        void StartTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

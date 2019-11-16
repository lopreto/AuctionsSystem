namespace AuctionsSystem.Infrastructure.Enums
{
    public enum MessageType
    {
        #region Account
        ChangePasswordSuccess,
        ChangeEmailSuccess,
        ChangeImageSuccess,
        LoginError,
        Error,
        #endregion

        #region Auctions
        BestBid,
        WithoutBalance,
        Success
        #endregion
    }
}
using System.Collections.ObjectModel;
using IDEManager.Models;

namespace IDEManager.ViewModels
{
    public class AccountViewModel
    {
        public ObservableCollection<Account> Accounts { get; } = new();

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }
    }
}
